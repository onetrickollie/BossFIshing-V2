using UnityEngine;
using TMPro;
using System.Collections;

public class CatchMessageUI : MonoBehaviour
{
    public TextMeshProUGUI catchMessageText; // Reference to the CatchMessage TextMeshPro UI element
    public float displayDuration = 2f; // Duration for which the message is displayed

    private Coroutine messageCoroutine;

    // Method to display the caught fish message
    public void DisplayCatchMessage(string fishName)
    {
        // If a message is already displaying, stop it
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        // Start a new coroutine to display the message
        messageCoroutine = StartCoroutine(DisplayMessageCoroutine(fishName));
    }

    private IEnumerator DisplayMessageCoroutine(string fishName)
    {
        // Set the text and make it visible
        catchMessageText.text = "Caught a " + fishName + "!";
        catchMessageText.gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // Hide the message after the duration
        catchMessageText.text = "";
        catchMessageText.gameObject.SetActive(false);
        
    }
}
