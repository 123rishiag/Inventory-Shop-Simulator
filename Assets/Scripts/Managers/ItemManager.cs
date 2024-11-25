using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private InventoryController inventoryController;
    private List<ItemScriptableObject> shopItems;

    [Header("Database")]
    [SerializeField] private ItemDatabase inventoryItemDatabase;
    [SerializeField] private ItemDatabase shopItemDatabase;

    [Header("Panels")]
    [SerializeField] private Transform inventoryGrid; // Assign Inventory Content Game Object inside Inventory Panel 

    [Header("UI Elements")]
    [SerializeField] private TMP_Text inventoryEmptyText;

    [Header("Prefabs")]
    [SerializeField] private GameObject itemPrefab;

    [Header("Inventory Config")]
    [SerializeField] private InventoryConfigScriptableObject inventoryConfig;

    private void Awake()
    {
        shopItems = LoadItemsFromDatabase(shopItemDatabase, "Shop");
    }

    private void Start()
    {
        PopulateInventory();
    }

    public void PopulateInventory()
    {
        if (!ValidateReferences(inventoryItemDatabase, inventoryGrid, "Inventory"))
            return;

        // Initialize InventoryController
        inventoryController = new InventoryController(inventoryGrid, inventoryEmptyText);

        // Populate Inventory
        foreach (var itemData in inventoryItemDatabase.allItems)
        {
            inventoryController.AddItem(itemData, itemPrefab);
        }
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

    public void PopulateShop(Transform _parentPanel)
    {
        foreach (var item in shopItems)
        {
            new ItemController(item, _parentPanel, itemPrefab);
        }
    }

    private bool ValidateReferences(ItemDatabase _database, Transform _parentPanel, string type)
    {
        if (_database == null)
        {
            Debug.LogError($"{type} Item Database is not assigned!");
            return false;
        }

        if (itemPrefab == null)
        {
            Debug.LogError("Item Prefab is not assigned!");
            return false;
        }

        if (_parentPanel == null)
        {
            Debug.LogError($"{type} Panel is not assigned!");
            return false;
        }

        return true;
    }
}
