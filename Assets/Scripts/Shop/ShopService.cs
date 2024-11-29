using ServiceLocator.Inventory;
using ServiceLocator.Item;
using ServiceLocator.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Shop
{
    public class ShopService
    {
        private ShopScriptableObject shopScriptableObject;

        private ItemService itemService;
        private UIService uiService;
        private InventoryService inventoryService;

        private ShopController shopController;

        public ShopService(ShopScriptableObject _scriptableObject)
        {
            shopScriptableObject = _scriptableObject;
        }

        public void Init(ItemService _itemService, UIService _uiService, InventoryService _inventoryService)
        {
            itemService = _itemService;
            uiService = _uiService;
            inventoryService = _inventoryService;

            InitializeVariables();
        }

        private void InitializeVariables()
        {
            if (!ValidateReferences(shopScriptableObject.allItems, shopScriptableObject.itemPrefab, 
                uiService.GetShopButtonPanel(), "Shop"))
                return;

            // Initializing ShopController
            shopController = new ShopController(shopScriptableObject, itemService, uiService);

            // Adding buttons dynamically
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                shopController.AddButtonToPanel(itemType);
            }

            // Populating Shop
            foreach (var itemData in shopScriptableObject.allItems)
            {
                // Adding ItemControllers
                shopController.AddNewItem(itemData);
            }
        }

        private bool ValidateReferences(List<ItemScriptableObject> _itemDatabase, GameObject _itemPrefab, 
            Transform _buttonPanel, string _type)
        {
            if (_itemDatabase == null)
            {
                Debug.LogError($"{_type} Item Database is not assigned!");
                return false;
            }

            if (_itemPrefab == null)
            {
                Debug.LogError($"{_type} Item Prefab is not assigned!");
                return false;
            }

            if (_buttonPanel == null)
            {
                Debug.LogError($"{_type} Button Panel is not assigned!");
                return false;
            }

            return true;
        }

        public void BuyItems(ItemController _itemController)
        {
            string transactionFailReason;
            bool isTransacted = inventoryService.GetInventoryController().AddOrIncrementItems(_itemController, out transactionFailReason, 2);
            if (isTransacted)
            {
                shopController.RemoveOrDecrementItems(_itemController, 2);
            }
            else
            {
                Debug.Log(transactionFailReason);
            }
        }

        public ShopController GetShopController() => shopController;
    }
}