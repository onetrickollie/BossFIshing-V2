using UnityEngine;
using UnityEngine.UI;

public class WelcomeSceneManager : MonoBehaviour
{
    [SerializeField] private Button startButton; 
    [SerializeField] private GameObject instructionPanel;
    private void Start()
    {
        if (startButton != null)
        {
            // Add the desired listener
            
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Debug.LogWarning("Start button reference is missing.");
        }
    }

    public void OnStartButtonClicked()
    {
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(true);
        }
    }
        public void OnQuitButtonClicked()
    {
        Debug.Log("Quit button clicked. Exiting the game...");
        Application.Quit();
    }
}