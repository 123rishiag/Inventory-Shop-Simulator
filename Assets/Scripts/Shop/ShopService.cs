using ServiceLocator.Event;
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

        private UIService uiService;
        private InventoryService inventoryService;
        private EventService eventService;

        private ShopController shopController;

        public ShopService(ShopScriptableObject _scriptableObject)
        {
            shopScriptableObject = _scriptableObject;
        }

        public void Init(UIService _uiService, InventoryService _inventoryService, EventService _eventService)
        {
            uiService = _uiService;
            inventoryService = _inventoryService;
            eventService = _eventService;

            InitializeVariables();
        }

        private void InitializeVariables()
        {
            if (!ValidateReferences(shopScriptableObject.itemDatabase.allItems, shopScriptableObject.itemPrefab,
                uiService.GetShopButtonPanel(), "Shop"))
                return;

            // Initializing ShopController
            shopController = new ShopController(shopScriptableObject, uiService, eventService);

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

        public void BuyItems(ItemController _itemController, int _quantity)
        {
            string transactionMessage;
            bool isTransacted = inventoryService.GetInventoryController().AddOrIncrementItems(_itemController,
                out transactionMessage, _quantity);
            if (isTransacted)
            {
                transactionMessage = $"{_quantity}x {_itemController.GetModel().Name} bought worth " +
                    $"{_itemController.GetModel().BuyingPrice * _quantity}A!!!!";
                shopController.RemoveOrDecrementItems(_itemController, _quantity);
                eventService.OnPopupNotificationEvent.Invoke(transactionMessage);
            }
            else
            {
                eventService.OnPopupNotificationEvent.Invoke(transactionMessage);
            }
        }

        public ShopController GetShopController() => shopController;
    }
}