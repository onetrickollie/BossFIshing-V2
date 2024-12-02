using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
using TMPro;
public class CarInteraction : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel; // Assign the menu UI Panel in Inspector
    public TMP_Text messageText; // Text component for displaying messages
    private bool isMoving = false;

    public float carSpeed = 1.0f;
    private void Start()
    {
        if (menuPanel != null)
            menuPanel.SetActive(false); // Ensure the menu is initially hidden
    }

    private void OnMouseDown()
    {
        if (isMoving) return; // Prevent interaction during the movement animation

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "main")
        {
            // Show the menu for confirmation
            if (menuPanel != null)
                menuPanel.SetActive(true);
        }
        else if (currentScene == "River")
        {
            // Directly transition back to Main without charging gold
            StartCoroutine(MoveCarAndLoadScene("main"));
        }
    }

    public void ConfirmTravel()
    {
        // Called when player confirms travel in the menu
        if (GameManager.Instance.playerGold >= 100)
        {
            GameManager.Instance.DeductGold(100);
            Debug.Log("100 gold deducted. Traveling to the River.");
            StartCoroutine(MoveCarAndLoadScene("River"));
        }
        else
        {
            messageText.text = "Not enough gold to travel.";
            Debug.Log("Not enough gold to travel.");
        }

        // Hide the menu after confirming
        if (menuPanel != null)
            menuPanel.SetActive(false);
    }

    public void CancelTravel()
    {
        // Called when player cancels travel in the menu
        if (menuPanel != null)
            menuPanel.SetActive(false);
    }

private IEnumerator MoveCarAndLoadScene(string sceneName)
{
    isMoving = true;
    float moveDuration = 2f; // Duration of the car moving animation
    float elapsedTime = 0f;

    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false; // Hide the player
        }

        Collider2D[] colliders = player.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false; // Disable collisions
        }
    }

    // Car movement animation
    Vector3 startPos = transform.position;
    Vector3 endPos = new Vector3(transform.position.x + carSpeed, transform.position.y, transform.position.z);

    while (elapsedTime < moveDuration)
    {
        transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Load the new scene
    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

    // Re-enable the player's visibility after the scene loads
    SceneManager.sceneLoaded += (scene, mode) =>
    {
        if (player != null)
        {
            // Use spawn point or saved position
            player.transform.position = GameManager.Instance.GetSpawnPointOrSavedPosition();

            Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = true;
            }

            Collider2D[] colliders = player.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = true;
            }
        }

        SceneManager.sceneLoaded -= (scene, mode) => { }; // Unsubscribe after handling
    };
}



}
