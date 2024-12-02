using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.Shop;
using ServiceLocator.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Inventory
{
    public class InventoryService
    {
        private InventoryScriptableObject inventoryScriptableObject;

        private ShopService shopService;
        private EventService eventService;

        private InventoryController inventoryController;

        public InventoryService(InventoryScriptableObject _scriptableObject)
        {
            inventoryScriptableObject = _scriptableObject;
        }

        public void Init(ShopService _shopService, EventService _eventService)
        {
            shopService = _shopService;
            eventService = _eventService;

            InitializeVariables();
        }

        private void InitializeVariables()
        {
            if (!ValidateReferences(inventoryScriptableObject.itemDatabase.allItems, "Inventory"))
                return;

            // Initializing InventoryController
            inventoryController = new InventoryController(inventoryScriptableObject, eventService);

            // Adding buttons dynamically
            inventoryController.AddButtonToPanel("Gather Resources");

            // Updating UI
            inventoryController.UpdateUI();
        }

        private bool ValidateReferences(List<ItemScriptableObject> _itemDatabase, string _type)
        {
            if (_itemDatabase == null)
            {
                Debug.LogError($"{_type} Item Database is not assigned!");
                return false;
            }

            return true;
        }

        public void SellItems(ItemScriptableObject _itemData, int _quantity)
        {
            string transactionMessage;
            bool isTransacted = shopService.GetShopController().AddOrIncrementItems(_itemData,
                out transactionMessage, _quantity);
            if (isTransacted)
            {
                transactionMessage = $"{_quantity}x {_itemData.name} sold worth " +
                    $"{_itemData.sellingPrice * _quantity}A!!!!";
                inventoryController.RemoveOrDecrementItems(_itemData, _quantity);
                eventService.OnPopupNotificationEvent.Invoke(transactionMessage);
            }
            else
            {
                eventService.OnPopupNotificationEvent.Invoke(transactionMessage);
            }
        }

        public InventoryController GetInventoryController() => inventoryController;
    }
}
