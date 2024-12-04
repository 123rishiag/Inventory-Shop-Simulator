using ServiceLocator.Event;
using ServiceLocator.Item;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Shop
{
    public class ShopService
    {
        private ShopScriptableObject shopScriptableObject;
        private ShopController shopController;

        public ShopService(EventService _eventService, ShopScriptableObject _scriptableObject)
        {
            shopScriptableObject = _scriptableObject;
            InitializeVariables(_eventService);
        }

        private void InitializeVariables(EventService _eventService)
        {
            if (!ValidateReferences(shopScriptableObject.itemDatabase.itemList, "Shop"))
                return;

            // Initializing ShopController
            shopController = new ShopController(_eventService);

            // Adding buttons dynamically
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                shopController.AddButtonToPanel(itemType);
            }

            // Populating Shop
            foreach (var itemData in shopScriptableObject.itemDatabase.itemList)
            {
                // Adding ItemControllers
                shopController.AddNewItem(itemData);
            }

            // Updating UI
            shopController.UpdateUI();
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