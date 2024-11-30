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
            if (!ValidateReferences(shopScriptableObject.itemDatabase.allItems, shopScriptableObject.itemPrefab, 
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
            foreach (var itemData in shopScriptableObject.itemDatabase.allItems)
            {
                // Adding ItemControllers
                shopController.AddNewItem(itemData);
            }

            // Updating UI
            shopController.UpdateUI();
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
            int quantity = 2;
            string transactionMessage;
            bool isTransacted = inventoryService.GetInventoryController().AddOrIncrementItems(_itemController, out transactionMessage, quantity);
            if (isTransacted)
            {
                transactionMessage = $"{quantity}x {_itemController.GetModel().Name} bought!!!!";
                uiService.PopupNotification(transactionMessage);
                shopController.RemoveOrDecrementItems(_itemController, quantity);
            }
            else
            {
                uiService.PopupNotification(transactionMessage);
            }
        }

        public ShopController GetShopController() => shopController;
    }
}