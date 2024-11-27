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

        [Header("Databases")]
        [SerializeField] private ItemDatabase inventoryItemDatabase;

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
        [SerializeField] private GameObject inventoryItemPrefab;

        [Header("Inventory Config")]
        [SerializeField] private InventoryConfigScriptableObject inventoryConfigData;

        public void Init()
        {
            PopulateInventory();
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

        public Transform GetShopGrid() => shopGrid;

        public Transform GetShopButtonPanel() => shopMenuButtonPanel;

        public void UpdateShopEmptyText(bool _isEmpty)
        {
            if (shopEmptyText != null)
            {
                shopEmptyText.gameObject.SetActive(_isEmpty);
            }
        }
        public void UpdateShopItemTypeText(string _text)
        {
            shopItemTypeText.text = $"Item Type: {_text}";
        }

        public void UpdateShopItemCount(int _itemCount)
        {
            shopItemsCountText.text = $"Items Count: {_itemCount}";
        }
    }
}