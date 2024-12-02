using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isFishing = false;
    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (GameManager.Instance != null)
        {
            // Use scene-specific spawn point if available
            Vector3 spawnPoint = GameManager.Instance.GetSpawnPointForScene(GameManager.Instance.currentScene);
            if (spawnPoint != Vector3.zero)
            {
                transform.position = spawnPoint;
            }
            else if (GameManager.Instance.playerPosition != Vector3.zero)
            {
                transform.position = GameManager.Instance.playerPosition;
            }
        }
    }

    private void Update()
    {
         if (isFishing) return;
        // Capture movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set animator parameters for animations
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Handle flipping based on horizontal movement
        if (movement.x > 0)
        {
            spriteRenderer.flipX = true; // Face right
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = false; // Face left
        }
    }

    private void FixedUpdate()
    {
        // Move the player using Rigidbody for consistent movement speed
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnDisable()
    {
        // Save player position when the GameObject is disabled or destroyed (e.g., on scene transition)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SavePlayerPosition(transform.position);
        }
    }
    public void StartFishing()
    {
        isFishing = true;
        animator.SetTrigger("StartFishing"); // Assume this trigger starts the fishing animation
    }

    public void EndFishing()
    {
        isFishing = false;
    }
}
