using ServiceLocator.Event;
using ServiceLocator.Item;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Inventory
{
    public class InventoryService
    {
        private InventoryScriptableObject inventoryScriptableObject;
        private InventoryController inventoryController;

        public InventoryService(EventService _eventService, InventoryScriptableObject _scriptableObject)
        {
            inventoryScriptableObject = _scriptableObject;
            InitializeVariables(_eventService);
        }

        private void InitializeVariables(EventService _eventService)
        {
            if (!ValidateReferences(inventoryScriptableObject.itemDatabase.itemList, "Inventory"))
                return;

            // Initializing InventoryController
            inventoryController = new InventoryController(_eventService, inventoryScriptableObject);

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
    }
}
