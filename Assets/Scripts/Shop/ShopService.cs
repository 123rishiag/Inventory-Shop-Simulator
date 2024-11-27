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

        public ShopService(ShopScriptableObject _shopScriptableObject) 
        {
            shopScriptableObject = _shopScriptableObject;
        }

        public void Init(UIService _uiService)
        {
            uiService = _uiService;
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            if (!ValidateReferences(shopScriptableObject.allItems, shopScriptableObject.itemPrefab, uiService.GetShopButtonPanel(), "Inventory"))
                return;

            // Initializing ShopController
            shopController = new ShopController(uiService);

            // Adding buttons dynamically
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                shopController.AddButtonToPanel(shopScriptableObject.menuButtonPrefab, uiService.GetShopButtonPanel(), itemType);
            }

            // Populating Shop
            foreach (var itemData in shopScriptableObject.allItems)
            {
                shopController.AddItem(itemData, shopScriptableObject.itemPrefab);
            }
        }


        private bool ValidateReferences(List<ItemScriptableObject> _itemDatabase, GameObject _itemPrefab, Transform _buttonPanel, string type)
        {
            if (_itemDatabase == null)
            {
                Debug.LogError($"{type} Item Database is not assigned!");
                return false;
            }

            if (_itemPrefab == null)
            {
                Debug.LogError($"{type} Item Prefab is not assigned!");
                return false;
            }

            if (_buttonPanel == null)
            {
                Debug.LogError($"{type} Button Panel is not assigned!");
                return false;
            }

            return true;
        }
    }
}