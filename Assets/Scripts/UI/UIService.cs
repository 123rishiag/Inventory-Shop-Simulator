using ServiceLocator.Inventory;
using ServiceLocator.Item;
using ServiceLocator.Shop;
using System;
using TMPro;
using UnityEngine;

namespace ServiceLocator.UI
{
    public class UIService : MonoBehaviour
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
        [SerializeField] private TMP_Text shopItemTypeText;
        [SerializeField] private TMP_Text shopItemsCountText;

        [Header("Prefabs")]
        [SerializeField] private GameObject inventoryMenuButtonPrefab;
        [SerializeField] private GameObject shopMenuButtonPrefab;
        [SerializeField] private GameObject inventoryItemPrefab;
        [SerializeField] private GameObject shopItemPrefab;

        [Header("Inventory Config")]
        [SerializeField] private InventoryConfigScriptableObject inventoryConfigData;

        public void Init()
        {
            PopulateInventory();
            PopulateShop();
        }

        public void PopulateInventory()
        {
            if (!ValidateReferences(inventoryItemDatabase, inventoryItemPrefab, inventoryGrid, "Inventory"))
                return;

            // Initializing InventoryController
            inventoryController = new InventoryController(inventoryGrid, inventoryEmptyText,
                inventoryCurrencyText, inventoryWeightText, inventoryConfigData.maxWeight);

            // Adding buttons dynamically
            inventoryController.AddButtonToPanel(inventoryMenuButtonPrefab, inventoryMenuButtonPanel, "Gather Resources");

            // Populating Inventory
            foreach (var itemData in inventoryItemDatabase.allItems)
            {
                inventoryController.AddItem(itemData, inventoryItemPrefab);
            }
        }

        public void PopulateShop()
        {
            if (!ValidateReferences(shopItemDatabase, shopItemPrefab, shopGrid, "Shop"))
                return;

            // Initializing ShopController
            shopController = new ShopController(shopGrid, shopEmptyText, shopItemTypeText, shopItemsCountText);

            // Adding buttons dynamically
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                shopController.AddButtonToPanel(shopMenuButtonPrefab, shopMenuButtonPanel, itemType);
            }

            // Populating Shop
            foreach (var itemData in shopItemDatabase.allItems)
            {
                shopController.AddItem(itemData, shopItemPrefab);
            }
        }

        private bool ValidateReferences(ItemDatabase _database, GameObject _itemPrefab, Transform _parentPanel, string type)
        {
            if (_database == null)
            {
                Debug.LogError($"{type} Item Database is not assigned!");
                return false;
            }

            if (_itemPrefab == null)
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
}