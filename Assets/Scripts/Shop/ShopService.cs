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

        private ShopController shopController;

        public ShopService(ShopScriptableObject _scriptableObject)
        {
            shopScriptableObject = _scriptableObject;
        }

        public void Init(UIService _uiService)
        {
            uiService = _uiService;
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            if (!ValidateReferences(shopScriptableObject.allItems, shopScriptableObject.itemPrefab, uiService.GetShopButtonPanel(),
                "Shop"))
                return;

            // Initializing ShopController
            shopController = new ShopController(shopScriptableObject, uiService);

            // Adding buttons dynamically
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                shopController.AddButtonToPanel(itemType);
            }

            // Populating Shop
            foreach (var itemData in shopScriptableObject.allItems)
            {
                // Creating ItemControllers
                var itemController = new ItemController(itemData, uiService.GetShopGrid(), shopScriptableObject.itemPrefab);
                shopController.AddOrIncrementItems(itemController);
            }
        }

        private bool ValidateReferences(List<ItemScriptableObject> _itemDatabase, GameObject _itemPrefab, Transform _buttonPanel,
            string _type)
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

        public void SellItems()
        {
            
        }
    }
}