using ServiceLocator.Event;
using ServiceLocator.Item;
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
            UpdateUI();
        }

        public void AddNewItem(ItemScriptableObject _itemScriptableObject, int _quantity = -1)
        {
            var itemController = eventService.OnCreateItemEvent.Invoke<ItemController>(_itemScriptableObject, shopModel.UISection);

            itemControllers.Add(itemController);
            // Setting EntityType of Item
            itemController.GetModel().UISection = GetModel().UISection;

            // Add to Model
            shopModel.AddItem(itemController.GetModel());

            // If the item does not exists, update new item's quantity
            if (_quantity == -1)
            {
                itemController.UpdateItemQuantity(itemController.GetModel().Quantity, false);
            }
            else
            {
                itemController.UpdateItemQuantity(_quantity, false);
            }
        }

        public bool AddOrIncrementItems(ItemController _itemController, out string _transactionMessage, int _quantity = 1)
        {
            if (!CheckMetricConditions(_itemController, out _transactionMessage, _quantity))
            {
                return false;
            }

            // Check if an item with the same ID already exists
            var existingItemController = itemControllers.Find(c => c.GetModel().Id == _itemController.GetModel().Id);

            if (existingItemController != null)
            {
                // If the item exists, update its quantity
                existingItemController.UpdateItemQuantity(_quantity, true);
            }
            else
            {
                // Add New Item
                AddNewItem(_itemController.GetModel().ItemData, _quantity);
            }

            // Update UI
            UpdateUI();

            return true;
        }

        public void RemoveOrDecrementItems(ItemController _itemController, int _quantity = 1)
        {
            // Check if an item with the same ID already exists
            var existingItemController = itemControllers.Find(c => c.GetModel().Id == _itemController.GetModel().Id);

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

        private bool CheckMetricConditions(ItemController _itemController, out string _transactionMessage, int _quantity)
        {
            _transactionMessage = string.Empty;
            // If Item does not have that much quantity in other UISection, return false
            if (_itemController.GetModel().Quantity < _quantity)
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