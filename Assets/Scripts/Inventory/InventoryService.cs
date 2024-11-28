using ServiceLocator.Item;
using ServiceLocator.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Inventory
{
    public class InventoryService
    {
        private InventoryScriptableObject inventoryScriptableObject;

        private ItemService itemService;
        private UIService uiService;

        private InventoryController inventoryController;

        public InventoryService(InventoryScriptableObject _scriptableObject)
        {
            inventoryScriptableObject = _scriptableObject;
        }

        public void Init(ItemService _itemService, UIService _uiService)
        {
            itemService = _itemService;
            uiService = _uiService;
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            if (!ValidateReferences(inventoryScriptableObject.allItems, inventoryScriptableObject.itemPrefab,
                uiService.GetInventoryButtonPanel(), "Inventory"))
                return;

            // Initializing InventoryController
            inventoryController = new InventoryController(inventoryScriptableObject, uiService);

            // Adding buttons dynamically
            inventoryController.AddButtonToPanel("Gather Resources");

            // Populating Inventory
            foreach (var itemData in inventoryScriptableObject.allItems)
            {
                // Creating ItemControllers
                var itemController = itemService.CreateItem(itemData, uiService.GetInventoryGrid(), 
                    inventoryScriptableObject.itemPrefab);
                inventoryController.AddOrIncrementItems(itemController);
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

        public void BuyItems()
        {

        }
    }
}
