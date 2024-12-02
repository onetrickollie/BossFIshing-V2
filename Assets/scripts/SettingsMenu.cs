using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject settingsPanel; // Reference to the settings/pause menu panel

    private bool isPaused = false; // Track whether the game is paused
    private AudioSource bgmAudioSource; // Reference to the main BGM AudioSource

    private void Start()
    {
        // Find the BGM GameObject in the scene (it should be persistent from the first scene)
        GameObject bgmObject = GameObject.Find("BGM");
        if (bgmObject != null)
        {
            bgmAudioSource = bgmObject.GetComponent<AudioSource>();
        }

        // Initialize slider value to match the BGM audio source volume
        if (bgmAudioSource != null && volumeSlider != null)
        {
            volumeSlider.value = bgmAudioSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume); // Attach listener to slider
        }

        // Conditionally hide settings panel based on the active scene
        if (settingsPanel != null && SceneManager.GetActiveScene().name != "welcome")
        {
            settingsPanel.SetActive(false);
        }
    }

    // Method to set volume based on slider value
    public void SetVolume(float value)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = value;
        }
    }

    // Method to handle "Back to Main Menu" button click
    public void BackToMainMenu()
    {
        // Resume game time before switching scenes
        Time.timeScale = 1f;
        SceneManager.LoadScene("welcome"); // Replace "welcome" with the actual name of your main menu scene
    }

    // Method to toggle the pause menu
    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(isPaused);
        }

        // Pause or resume the game time
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // Method to quit/resume the pause menu
    public void QuitPauseMenu()
    {
        isPaused = false;
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // Resume game time
        Time.timeScale = 1f;
    }
}
