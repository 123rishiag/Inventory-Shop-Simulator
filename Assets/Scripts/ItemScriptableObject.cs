using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public Sprite icon; // Item's icon
    [TextArea] public string description; // Detailed description
    public int buyingPrice; // Cost to buy
    public int sellingPrice; // Price when sold
    public float weight; // Item weight
    public ItemType itemType; // Type of the item
    public Rarity rarity; // Rarity of the item
    public int quantity; // Initial quantity
}

public enum ItemType
{
    Materials,
    Weapons,
    Consumables,
    Treasure
}

public enum Rarity
{
    VeryCommon,
    Common,
    Rare,
    Epic,
    Legendary
}