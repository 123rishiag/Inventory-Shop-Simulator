using UnityEngine;

namespace ServiceLocator.Item
{
    [CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/ItemScriptableObject")]
    public class ItemScriptableObject : ScriptableObject
    {
        [Header("Unique Identifier (Auto-Generated)")]
        [SerializeField, ReadOnly] private int id;

        [Header("General Info")]
        public Sprite icon;
        [TextArea] public string description;

        [Header("Attributes")]
        public int buyingPrice;
        public int sellingPrice;
        public float weight;
        public ItemType type;
        public Rarity rarity;
        public int quantity;

        // Logic to auto update identity and make it readonly
        [HideInInspector] public int Id => id; // Public getter for the ID

        // Tracks the next ID to use (Editor-only, not saved)
        private static int nextID = 1;

        private void OnValidate()
        {
            // Assign an ID only if it hasn't been set
            if (id == 0)
            {
                AssignUniqueID();
            }
        }

        private void AssignUniqueID()
        {
            // Ensure we avoid overlapping IDs in the current session
            id = GetNextAvailableID();
        }

        private static int GetNextAvailableID()
        {
            // Start with the highest used ID + 1
            var allItems = Resources.FindObjectsOfTypeAll<ItemScriptableObject>();
            foreach (var item in allItems)
            {
                if (item.id >= nextID)
                {
                    nextID = item.id + 1;
                }
            }

            return nextID++;
        }

        public class ReadOnlyAttribute : PropertyAttribute { /* Just a marker */ }
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
        VeryCommon = 1,
        Common,
        Rare,
        Epic,
        Legendary
    }

    public enum UISection
    {
        Inventory,
        Shop
    }
}