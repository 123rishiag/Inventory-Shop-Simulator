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
        private UIService uiService;

        private ShopModel shopModel;
        private ShopView shopView;
        private List<ItemController> itemControllers;
        private List<ItemController> filteredItemControllers;

        public ShopController(ShopScriptableObject _scriptableObject, UIService _uiService)
        {
            shopScriptableObject = _scriptableObject;
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

        public void AddOrIncrementItems(ItemController itemController)
        {
            // Check if an item with the same ID already exists
            var existingController = itemControllers.Find(c => c.GetModel().Id == itemController.GetModel().Id);

            if (existingController != null)
            {
                // If the item exists, update its quantity
                int quantityToRemove = itemController.GetModel().Quantity;
                existingController.UpdateItemQuantity(quantityToRemove);
            }
            else
            {
                // If the item doesn't exist, add it to the list
                itemControllers.Add(itemController);

                // Add to Model
                shopModel.AddItem(itemController.GetModel());
            }

            // Update UI
            UpdateUI();
        }

        public void RemoveOrDecrementItems(ItemController itemController)
        {
            // Check if an item with the same ID already exists
            var existingController = itemControllers.Find(c => c.GetModel().Id == itemController.GetModel().Id);

            if (existingController != null)
            {
                // Reduce the quantity of the existing item
                int quantityToRemove = itemController.GetModel().Quantity;
                existingController.UpdateItemQuantity(-quantityToRemove);

                // If quantity becomes zero or less, remove the item completely
                if (existingController.GetModel().Quantity <= 0)
                {
                    // Remove from the item list and shop model
                    shopModel.RemoveItem(existingController.GetModel());
                    itemControllers.Remove(existingController);

                    // Destroy the item
                    existingController.GetView().DestroyView();
                }
            }

            // Update UI
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Clear filtered list
            filteredItemControllers.Clear();

            // Show Filtered Items
            shopView.ShowFilteredItems();

            // Update UI
            shopView.UpdateUI(uiService);
        }

        // Getters
        public List<ItemController> GetItems() => itemControllers;
        public List<ItemController> GetFilteredItems() => filteredItemControllers;
        public ShopModel GetModel() => shopModel;
    }
}