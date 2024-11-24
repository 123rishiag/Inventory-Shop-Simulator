using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<ItemScriptableObject> inventoryItems = new List<ItemScriptableObject>();
    private List<ItemScriptableObject> shopItems = new List<ItemScriptableObject>();

    [Header("Database")]
    public ItemDatabase inventoryItemDatabase;
    public ItemDatabase shopItemDatabase;

    [Header("Inventory Config")]
    public InventoryConfigScriptableObject inventoryConfig;

    private void Start()
    {
        inventoryItems = LoadItemsFromDatabase(inventoryItemDatabase, "Inventory");
        shopItems = LoadItemsFromDatabase(shopItemDatabase, "Shop");
    }

    private List<ItemScriptableObject> LoadItemsFromDatabase(ItemDatabase database, string type)
    {
        if (database != null)
        {
            Debug.Log($"{type} items loaded successfully.");
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
