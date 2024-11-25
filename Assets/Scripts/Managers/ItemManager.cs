using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Controllers
    private InventoryController inventoryController;
    private ShopController shopController;

    [Header("Databases")]
    [SerializeField] private ItemDatabase inventoryItemDatabase;
    [SerializeField] private ItemDatabase shopItemDatabase;

    [Header("Panels")]
    [SerializeField] private Transform inventoryGrid; // Assign Inventory Content Game Object inside Inventory Panel
    [SerializeField] private Transform inventoryMenuButtonPanel;
    [SerializeField] private Transform shopGrid; // Assign Shop Content Game Object inside Shop Panel
    [SerializeField] private Transform shopMenuButtonPanel;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text inventoryEmptyText;
    [SerializeField] private TMP_Text inventoryCurrencyText;
    [SerializeField] private TMP_Text inventoryWeightText;
    [SerializeField] private TMP_Text shopEmptyText;
    [SerializeField] private TMP_Text shopItemsCountText;

    [Header("Prefabs")]
    [SerializeField] private GameObject menuButtonPrefab;
    [SerializeField] private GameObject itemPrefab;

    [Header("Inventory Config")]
    [SerializeField] private InventoryConfigScriptableObject inventoryConfigData;

    private void Start()
    {
        PopulateInventory();
        PopulateShop();
    }

    public void PopulateInventory()
    {
        if (!ValidateReferences(inventoryItemDatabase, inventoryGrid, "Inventory"))
            return;

        // Initializing InventoryController
        inventoryController = new InventoryController(inventoryGrid, inventoryEmptyText, 
            inventoryCurrencyText, inventoryWeightText, inventoryConfigData.maxWeight);

        // Adding buttons dynamically
        inventoryController.AddButtonToPanel(menuButtonPrefab, inventoryMenuButtonPanel, "Gather Resources");

        // Populating Inventory
        foreach (var itemData in inventoryItemDatabase.allItems)
        {
            inventoryController.AddItem(itemData, itemPrefab);
        }
    }

    public void PopulateShop()
    {
        if (!ValidateReferences(shopItemDatabase, shopGrid, "Shop"))
            return;

        // Initializing ShopController
        shopController = new ShopController(shopGrid, shopEmptyText, shopItemsCountText);

        // Adding buttons dynamically
        shopController.AddButtonToPanel(menuButtonPrefab, shopMenuButtonPanel, "Materials");
        shopController.AddButtonToPanel(menuButtonPrefab, shopMenuButtonPanel, "Weapons");
        shopController.AddButtonToPanel(menuButtonPrefab, shopMenuButtonPanel, "Consumables");
        shopController.AddButtonToPanel(menuButtonPrefab, shopMenuButtonPanel, "Treasure");

        // Populating Shop
        foreach (var itemData in shopItemDatabase.allItems)
        {
            shopController.AddItem(itemData, itemPrefab);
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
