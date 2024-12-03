using ServiceLocator.Event;
using ServiceLocator.Item;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Inventory
{
    public class InventoryService
    {
        private InventoryScriptableObject inventoryScriptableObject;
        private EventService eventService;
        private InventoryController inventoryController;

        public InventoryService(EventService _eventService, InventoryScriptableObject _scriptableObject)
        {
            eventService = _eventService;
            inventoryScriptableObject = _scriptableObject;
            InitializeVariables();

            // Adding Event Listeners
            eventService.OnSellItemEvent.AddListener(SellItems);
        }

        ~InventoryService()
        {
            // Removing Event Listeners
            eventService.OnSellItemEvent.RemoveListener(SellItems);
        }

        private void InitializeVariables()
        {
            if (!ValidateReferences(inventoryScriptableObject.itemDatabase.allItems, "Inventory"))
                return;

            // Initializing InventoryController
            inventoryController = new InventoryController(eventService, inventoryScriptableObject);

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

        private void SellItems(ItemModel _itemModel, int _quantity)
        {
            bool isTransacted = eventService.OnShopAddItemEvent.Invoke<bool>(_itemModel, _quantity);
            if (isTransacted)
            {
                inventoryController.RemoveOrDecrementItems(_itemModel, _quantity);
            }
        }
    }
}
