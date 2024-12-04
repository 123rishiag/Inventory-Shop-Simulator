using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.Sound;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Shop
{
    public class ShopController
    {
        private EventService eventService;

        private ShopModel shopModel;
        private ShopView shopView;
        private List<ItemController> itemControllers;

        public ShopController(EventService _eventService)
        {
            eventService = _eventService;

            // Instantiating Model
            shopModel = new ShopModel();

            // Instantiating View
            shopView = new ShopView(this);

            // Controllers List
            itemControllers = new List<ItemController>();

            // Adding Event Listeners
            eventService.OnBuyItemEvent.AddListener(BuyItems);
            eventService.OnShopAddItemEvent.AddListener(AddOrIncrementItems);
        }
        ~ShopController()
        {
            // Removing Event Listeners
            eventService.OnBuyItemEvent.RemoveListener(BuyItems);
            eventService.OnShopAddItemEvent.RemoveListener(AddOrIncrementItems);
        }

        public void AddButtonToPanel(ItemType _itemType)
        {
            // Instantiating the button
            GameObject newButton = eventService.OnCreateMenuButtonViewEvent.Invoke<GameObject>(shopModel.UISection, _itemType.ToString());

            // Setting up button logic (e.g., click events)
            Button button = newButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnButtonClicked(_itemType));
            }
            else
            {
                Debug.LogWarning("Button component not found in button prefab.");
            }
        }

        private void OnButtonClicked(ItemType _selectedItemType)
        {
            shopModel.SelectedItemType = _selectedItemType;
            eventService.OnPlaySoundEffectEvent.Invoke(SoundType.TabSwitch);
            UpdateUI();
        }

        private void BuyItems(ItemModel _itemModel, int _quantity)
        {
            bool isTransacted = eventService.OnInventoryAddItemEvent.Invoke<bool>(_itemModel, _quantity);
            if (isTransacted)
            {
                eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ItemBought);
                RemoveOrDecrementItems(_itemModel, _quantity);
            }
        }

        public void AddNewItem(ItemScriptableObject _itemData, int _quantity = -1)
        {
            var itemController = eventService.OnCreateItemEvent.Invoke<ItemController>(_itemData, shopModel.UISection);

            itemControllers.Add(itemController);
            // Setting EntityType of Item
            itemController.GetModel().UISection = GetModel().UISection;

            // Add to Model
            shopModel.AddItem(itemController.GetModel());

            // If the item does not exists,
            // happens when no transaction but only item addition happens during initialization
            // then update weight with quantity given in Scriptable Objects
            if (_quantity == -1)
            {
                itemController.UpdateItemQuantity(itemController.GetModel().Quantity, false);
            }
            else
            {
                itemController.UpdateItemQuantity(_quantity, false);
            }
        }

        private bool AddOrIncrementItems(ItemModel _itemModel, int _quantity = 1)
        {
            string transactionMessage = string.Empty;
            if (!CheckMetricConditions(_itemModel, out transactionMessage, _quantity))
            {
                // Displaying Unsuccessful Transaction Popup
                eventService.OnPopupNotificationEvent.Invoke(transactionMessage);
                eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ErrorFeedback);
                return false;
            }

            // Check if an item with the same ID already exists
            var existingItemController = itemControllers.Find(c => c.GetModel().Id == _itemModel.Id);

            if (existingItemController != null)
            {
                // If the item exists, update its quantity
                existingItemController.UpdateItemQuantity(_quantity, true);
            }
            else
            {
                // Add New Item
                AddNewItem(_itemModel.ItemData, _quantity);
            }

            // Update UI
            UpdateUI();

            // Displaying Successful Transaction Popup
            transactionMessage = $"{_quantity}x {_itemModel.Name} sold worth " +
                    $"{_itemModel.SellingPrice * _quantity}A!!!!";
            eventService.OnPopupNotificationEvent.Invoke(transactionMessage);

            return true;
        }

        private void RemoveOrDecrementItems(ItemModel _itemModel, int _quantity = 1)
        {
            // Check if an item with the same ID already exists
            var existingItemController = itemControllers.Find(c => c.GetModel().Id == _itemModel.Id);

            if (existingItemController != null)
            {
                // Reduce the quantity of the existing item
                existingItemController.UpdateItemQuantity((-1 * _quantity), true);

                // If quantity becomes zero or less, remove the item completely
                if (existingItemController.GetModel().Quantity <= 0)
                {
                    // Remove from the item list and shop model
                    shopModel.RemoveItem(existingItemController.GetModel());
                    itemControllers.Remove(existingItemController);

                    // Remove Item
                    eventService.OnDestroyItemEvent.Invoke(shopModel.UISection, existingItemController.GetModel().Id);
                }
            }

            // Update UI
            UpdateUI();
        }

        private bool CheckMetricConditions(ItemModel _itemModel, out string _transactionMessage, int _quantity)
        {
            _transactionMessage = string.Empty;
            // If Item does not have that much quantity in other UISection, return false
            if (_itemModel.Quantity < _quantity)
            {
                _transactionMessage = "Inventory doesn't have that many items.";
                return false;
            }

            return true;
        }

        public void UpdateUI()
        {
            // Update UI
            shopView.UpdateUI(eventService);
        }

        // Getters
        public List<ItemController> GetItems() => itemControllers;
        public int GetFilteredItemsCount() => itemControllers.Count(
            item => (item.GetModel().Type == shopModel.SelectedItemType || shopModel.SelectedItemType == ItemType.All)
            );

        public ShopModel GetModel() => shopModel;
    }
}