using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    public string sceneName; // Name of the scene to load
    public Vector3 returnPosition; // Position to place player on return

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Save the player's position in the GameManager
            GameManager.Instance.SavePlayerPosition(returnPosition);

            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }
}
