using UnityEngine;

namespace ServiceLocator.Item
{
    [CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/ItemScriptableObject")]
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
        All,
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
}