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

        private ItemService itemService;
        private UIService uiService;
        private ShopService shopService;

        private InventoryController inventoryController;

        public InventoryService(InventoryScriptableObject _scriptableObject)
        {
            inventoryScriptableObject = _scriptableObject;
        }

        public void Init(ItemService _itemService, UIService _uiService, ShopService _shopService)
        {
            itemService = _itemService;
            uiService = _uiService;
            shopService = _shopService;

            InitializeVariables();
        }

        private void InitializeVariables()
        {
            if (!ValidateReferences(inventoryScriptableObject.itemDatabase.allItems, inventoryScriptableObject.itemPrefab,
                uiService.GetInventoryButtonPanel(), "Inventory"))
                return;

            // Initializing InventoryController
            inventoryController = new InventoryController(inventoryScriptableObject, itemService, uiService);

            // Adding buttons dynamically
            inventoryController.AddButtonToPanel("Gather Resources");
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

        public void SellItems(ItemController _itemController)
        {
            string transactionFailReason;
            bool isTransacted = shopService.GetShopController().AddOrIncrementItems(_itemController, out transactionFailReason, 2);
            if (isTransacted)
            {
                inventoryController.RemoveOrDecrementItems(_itemController, 2);
            }
            else
            {
                Debug.Log(transactionFailReason);
            }
        }

        public InventoryController GetInventoryController() => inventoryController;
    }
}
