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

        private ItemService itemService;
        private UIService uiService;
        private ShopService shopService;
        private EventService eventService;

        private InventoryController inventoryController;

        public InventoryService(InventoryScriptableObject _scriptableObject)
        {
            inventoryScriptableObject = _scriptableObject;
        }

        public void Init(ItemService _itemService, UIService _uiService, ShopService _shopService, EventService _eventService)
        {
            itemService = _itemService;
            uiService = _uiService;
            shopService = _shopService;
            eventService = _eventService;

            InitializeVariables();
        }

        private void InitializeVariables()
        {
            if (!ValidateReferences(inventoryScriptableObject.itemDatabase.allItems, inventoryScriptableObject.itemPrefab,
                uiService.GetInventoryButtonPanel(), "Inventory"))
                return;

            // Initializing InventoryController
            inventoryController = new InventoryController(inventoryScriptableObject, itemService, uiService, eventService);

            // Adding buttons dynamically
            inventoryController.AddButtonToPanel("Gather Resources");

            // Updating UI
            inventoryController.UpdateUI();
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

        public void SellItems(ItemController _itemController, int _quantity)
        {
            string transactionMessage;
            bool isTransacted = shopService.GetShopController().AddOrIncrementItems(_itemController,
                out transactionMessage, _quantity);
            if (isTransacted)
            {
                transactionMessage = $"{_quantity}x {_itemController.GetModel().Name} sold worth " +
                    $"{_itemController.GetModel().SellingPrice * _quantity} coins!!!!";
                inventoryController.RemoveOrDecrementItems(_itemController, _quantity);
                eventService.OnPopupNotificationEvent.Invoke(transactionMessage);
            }
            else
            {
                eventService.OnPopupNotificationEvent.Invoke(transactionMessage);
            }
        }

        public InventoryController GetInventoryController() => inventoryController;

        public GameObject GetButtonItemPrefab() => inventoryScriptableObject.menuButtonPrefab;
    }
}
