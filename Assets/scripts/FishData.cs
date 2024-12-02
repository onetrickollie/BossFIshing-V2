using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Fishing/Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;
    public Sprite fishSprite;
    public string fishDescription;
    public int price; // New field for the fish price
}
