using ServiceLocator.Item;
using ServiceLocator.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Shop
{
    public class ShopController
    {
        private ShopScriptableObject shopScriptableObject;
        private ItemService itemService;
        private UIService uiService;

        private ShopModel shopModel;
        private ShopView shopView;
        private List<ItemController> itemControllers;
        private List<ItemController> filteredItemControllers;

        public ShopController(ShopScriptableObject _scriptableObject, ItemService _itemService, UIService _uiService)
        {
            shopScriptableObject = _scriptableObject;
            itemService = _itemService;
            uiService = _uiService;

            // Instantiating Model
            shopModel = new ShopModel();

            // Instantiating View
            shopView = new ShopView(this);

            // Controllers List
            itemControllers = new List<ItemController>();
            filteredItemControllers = new List<ItemController>();

            // Update UI
            UpdateUI();
        }

        public void AddButtonToPanel(ItemType _itemType)
        {
            // Instantiating the button
            GameObject newButton = shopView.CreateButton(shopScriptableObject.menuButtonPrefab,
                uiService.GetShopButtonPanel(), _itemType);

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
            var itemController = itemService.CreateItem(_itemScriptableObject, uiService.GetShopGrid(),
                shopScriptableObject.itemPrefab);

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

        public bool AddOrIncrementItems(ItemController _itemController, int _quanitity = 1)
        {
            // If Item does not have that much quantity in other UISection, return false
            if (_itemController.GetModel().Quantity < _quanitity)
            {
                return false;
            }

            // Check if an item with the same ID already exists
            var existingItemController = itemControllers.Find(c => c.GetModel().Id == _itemController.GetModel().Id);

            if (existingItemController != null)
            {
                // If the item exists, update its quantity
                existingItemController.UpdateItemQuantity(_quanitity, true);
            }
            else
            {
                // Add New Item
                AddNewItem(_itemController.GetModel().ItemData, _quanitity);
            }

            // Update UI
            UpdateUI();

            return true;
        }

        public void RemoveOrDecrementItems(ItemController _itemController, int _quanitity = 1)
        {
            // Check if an item with the same ID already exists
            var existingItemController = itemControllers.Find(c => c.GetModel().Id == _itemController.GetModel().Id);

            if (existingItemController != null)
            {
                // Reduce the quantity of the existing item
                existingItemController.UpdateItemQuantity(-_quanitity, true);

                // If quantity becomes zero or less, remove the item completely
                if (existingItemController.GetModel().Quantity <= 0)
                {
                    // Remove from the item list and shop model
                    shopModel.RemoveItem(existingItemController.GetModel());
                    itemControllers.Remove(existingItemController);

                    // Remove Item
                    existingItemController.GetView().DestroyView();
                }
            }

            // Update UI
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Clear filtered list
            filteredItemControllers.Clear();

            // Show Items
            shopView.ShowItems();

            // Update UI
            shopView.UpdateUI(uiService);
        }

        // Getters
        public List<ItemController> GetItems() => itemControllers;
        public List<ItemController> GetFilteredItems() => filteredItemControllers;
        public ShopModel GetModel() => shopModel;
    }
}