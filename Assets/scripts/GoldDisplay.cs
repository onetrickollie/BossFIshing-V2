using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GoldDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText; // Reference to the UI Text component

    private void Start()
    {
        if (goldText == null)
        {
            Debug.LogWarning("Gold Text UI reference is not set.");
            return;
        }

        // Update the initial display
        UpdateGoldDisplay(GameManager.Instance.playerGold);

        // Subscribe to gold change updates (if needed for events)
        GameManager.Instance.OnGoldChanged += UpdateGoldDisplay;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            // Unsubscribe from gold change updates when this object is destroyed
            GameManager.Instance.OnGoldChanged -= UpdateGoldDisplay;
        }
    }

    // Method to update the UI text when gold changes
    private void UpdateGoldDisplay(int newGoldAmount)
    {
        if (goldText != null)
        {
            goldText.text = $"Gold: {newGoldAmount}";
        }
        else{
            Debug.LogWarning("Gold Text UI reference is not set.");
        }
    }
}
