using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<ItemScriptableObject> inventoryItems;
    private List<ItemScriptableObject> shopItems;

    [Header("Database")]
    [SerializeField] private ItemDatabase inventoryItemDatabase;
    [SerializeField] private ItemDatabase shopItemDatabase;

    [Header("Prefabs")]
    [SerializeField] private GameObject itemPrefab;

    [Header("Inventory Config")]
    [SerializeField] private InventoryConfigScriptableObject inventoryConfig;

    private void Awake()
    {
        inventoryItems = LoadItemsFromDatabase(inventoryItemDatabase, "Inventory");
        shopItems = LoadItemsFromDatabase(shopItemDatabase, "Shop");
    }
    private void Start()
    {
        if (!ValidateItemPrefab()) 
            return;
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

    public void PopulateInventory(Transform _parentPanel)
    {
        foreach (var item in inventoryItems)
        {
            new ItemController(item, _parentPanel, itemPrefab);
        }
    }

    public void PopulateShop(Transform _parentPanel)
    {
        foreach (var item in shopItems)
        {
            new ItemController(item, _parentPanel, itemPrefab);
        }
    }

    private bool ValidateItemPrefab()
    {
        if (itemPrefab == null)
        {
            Debug.LogError("ItemPrefab is not assigned in the inspector!");
            return false;
        }
        return true;
    }
}
