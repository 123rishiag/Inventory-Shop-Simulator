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
        private EventService eventService;
        private ShopController shopController;

        public ShopService(ShopScriptableObject _scriptableObject)
        {
            shopScriptableObject = _scriptableObject;
        }

        public void Init(EventService _eventService)
        {
            eventService = _eventService;
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            if (!ValidateReferences(shopScriptableObject.itemDatabase.allItems, "Shop"))
                return;

            // Initializing ShopController
            shopController = new ShopController(eventService);

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

        private bool ValidateReferences(List<ItemScriptableObject> _itemDatabase, string _type)
        {
            if (_itemDatabase == null)
            {
                Debug.LogError($"{_type} Item Database is not assigned!");
                return false;
            }

            return true;
        }

        public void BuyItems(ItemModel _itemModel, int _quantity)
        {
            bool isTransacted = eventService.OnBuyItemEvent.Invoke<bool>(_itemModel, _quantity);
            if (isTransacted)
            {
                shopController.RemoveOrDecrementItems(_itemModel, _quantity);
            }
        }
    }
}