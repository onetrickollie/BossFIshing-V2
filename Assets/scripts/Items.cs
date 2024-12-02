using UnityEngine;

public class Item
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public int price;
    [TextArea] [SerializeField] private string itemDescription;

    public FishData linkedFishData; // Reference to FishData, if applicable
    public bool isFish; // Property to indicate if this item is a fish

    // Constructor that takes 5 parameters
    public Item(string name, int qty, Sprite sprite, string description, int itemPrice, bool fishStatus = false)
    {
        itemName = name;
        quantity = qty;
        itemSprite = sprite;
        itemDescription = description;
        price = itemPrice;
        isFish = fishStatus;
    }

    public string Description => linkedFishData != null ? linkedFishData.fishDescription : itemDescription;
}
