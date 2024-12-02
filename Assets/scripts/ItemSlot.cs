using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private InventoryManager inventoryManager;
    public Image icon;
    public TMP_Text itemNameText;
    public TMP_Text quantityText;
    public bool isFull;
    public string itemDescription;

    public GameObject selectedShader;
    public bool thisItemSelected;

    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemDescriptionNameText;

    private Item currentItem; // Reference to the current item in this slot

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public void AddItem(Item item)
    {
        icon.sprite = item.itemSprite;
        itemNameText.text = item.itemName;
        quantityText.text = $"x{item.quantity}";
        isFull = true;
        currentItem = item; // Store the reference for later use
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        itemNameText.text = "";
        quantityText.text = "";
        isFull = false;
        currentItem = null; // Clear the reference
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)  
        {
            OnLeftClick();    
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        inventoryManager.DisplayItemDetails(currentItem); // Assuming currentItem is assigned correctly
        if (itemDescriptionText == null)
    {
    Debug.LogWarning("ItemSlot: itemDescriptionText is not assigned. Please check the Inspector.");
    }

    }
}
