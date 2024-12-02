using UnityEngine;
using UnityEngine.UI;
public class VendingMachine : MonoBehaviour
{
    [SerializeField] private GameObject vendingMachinePanel; // The UI panel for the vending machine
    [SerializeField] private int chuckieColaCost = 50; // Cost of Chuckie Cola
    public Button exitButton; // Button to close the panel
        
    private void Start()
    {
        if (exitButton != null) {
            exitButton.onClick.AddListener(CloseVendingMachinePanel);
        }
    }
    private void OnMouseDown()
    {
        if (vendingMachinePanel != null)
        {
            vendingMachinePanel.SetActive(true); // Show the vending machine panel
        }
    }

    public void PurchaseChuckieCola()
    {
        if (GameManager.Instance.playerGold >= chuckieColaCost)
        {
            GameManager.Instance.DeductGold(chuckieColaCost);
            Debug.Log("Chuckie Cola purchased! Fishing ability increased!");
            GameManager.Instance.ActivateFishingBoost(2f, 120f); // Boost fishing by 2x for 2 minutes
        }
        else
        {
            Debug.Log("Not enough gold to purchase Chuckie Cola.");
        }

        if (vendingMachinePanel != null)
        {
            vendingMachinePanel.SetActive(false); // Hide the panel after purchase
        }
    }

    public void CloseVendingMachinePanel()
    {
        if (vendingMachinePanel != null)
        {
            vendingMachinePanel.SetActive(false); // Hide the panel without purchasing
        }
    }
}
