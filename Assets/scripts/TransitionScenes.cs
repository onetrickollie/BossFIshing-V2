using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public Transform marketSpawnPoint; // Set this in the Inspector for your market scene spawn point

    public void EnterMarketScene()
    {
        if (GameManager.Instance != null)
        {
            // Set the custom spawn point for the Market Scene
            GameManager.Instance.SetSpawnPointForScene("MarketScene", marketSpawnPoint.position);
            GameManager.Instance.SetCurrentScene("MarketScene");
        }

        // Load the market scene
        SceneManager.LoadScene("MarketScene");
    }

    public void EnterMainScene(Transform mainSceneSpawnPoint)
    {
        if (GameManager.Instance != null)
        {
            // Set the custom spawn point for the Main Scene
            GameManager.Instance.SetSpawnPointForScene("MainScene", mainSceneSpawnPoint.position);
            GameManager.Instance.SetCurrentScene("MainScene");
        }

        // Load the main scene
        SceneManager.LoadScene("MainScene");
    }
}
