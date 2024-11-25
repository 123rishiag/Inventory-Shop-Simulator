using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public Sprite icon;
    [TextArea] public string description;
    public int buyingPrice;
    public int sellingPrice;
    public float weight;
    public ItemType type;
    public Rarity rarity;
    public int quantity;
}

public enum ItemType
{
    Materials,
    Weapons,
    Consumables,
    Treasure,
    All
}

public enum Rarity
{
    VeryCommon,
    Common,
    Rare,
    Epic,
    Legendary
}