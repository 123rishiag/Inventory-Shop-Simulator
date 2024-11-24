using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<ItemScriptableObject> inventoryItems;
    private List<ItemScriptableObject> shopItems;

    [Header("Database")]
    [SerializeField] private ItemDatabase inventoryItemDatabase;
    [SerializeField] private ItemDatabase shopItemDatabase;

    [Header("Inventory Config")]
    [SerializeField] private InventoryConfigScriptableObject inventoryConfig;

    private void Awake()
    {
        inventoryItems = LoadItemsFromDatabase(inventoryItemDatabase, "Inventory");
        shopItems = LoadItemsFromDatabase(shopItemDatabase, "Shop");
    }

    private List<ItemScriptableObject> LoadItemsFromDatabase(ItemDatabase database, string type)
    {
        if (database != null)
        {
            return new List<ItemScriptableObject>(database.allItems);
        }
        else
        {
            Debug.LogWarning($"{type}ItemDatabase is not assigned, starting with an empty {type.ToLower()}.");
            return new List<ItemScriptableObject>();
        }
    }

    public IReadOnlyList<ItemScriptableObject> GetInventoryItems()
    {
        return inventoryItems.AsReadOnly();
    }

    public IReadOnlyList<ItemScriptableObject> GetShopItems()
    {
        return shopItems.AsReadOnly();
    }
}
