using ServiceLocator.Item;
using ServiceLocator.Shop;
using ServiceLocator.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Inventory
{
    public class InventoryController
    {
        private InventoryScriptableObject inventoryScriptableObject;
        private UIService uiService;

        private InventoryModel inventoryModel;
        private InventoryView inventoryView;
        private List<ItemController> itemControllers;

        public InventoryController(InventoryScriptableObject _scriptableObject, UIService _uiService)
        {
            inventoryScriptableObject = _scriptableObject;
            uiService = _uiService;

            // Instantiating Model
            inventoryModel = new InventoryModel(_scriptableObject);

            // Instantiating View
            inventoryView = new InventoryView(this);

            // Controller List
            itemControllers = new List<ItemController>();

            // Update UI
            UpdateUI();
        }

        public void AddButtonToPanel(string _menuButtonText)
        {
            // Instantiating the button
            GameObject newButton = inventoryView.CreateButton(inventoryScriptableObject.menuButtonPrefab, 
                uiService.GetInventoryButtonPanel(), _menuButtonText);

            // Setting up button logic (e.g., click events)
            Button button = newButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnButtonClicked(_menuButtonText));
            }
            else
            {
                Debug.LogWarning("Button component not found in button prefab.");
            }
        }

        private void OnButtonClicked(string _menuButtonText)
        {
            // Handle button-specific actions here
            Debug.Log($"Button '{_menuButtonText}' clicked!");
            UpdateUI();
        }

        public void AddOrIncrementItems(ItemController _itemController)
        {
            // Check if an item with the same ID already exists
            var existingController = itemControllers.Find(c => c.GetModel().Id == _itemController.GetModel().Id);

            if (existingController != null)
            {
                // If the item exists, update its quantity
                int quantityToRemove = _itemController.GetModel().Quantity;
                existingController.UpdateItemQuantity(quantityToRemove);
            }
            else
            {
                // If the item doesn't exist, add it to the list
                itemControllers.Add(_itemController);
                // Setting EntityType of Item
                _itemController.GetModel().UISection = GetModel().UISection;

                // Add to Model
                inventoryModel.AddItem(_itemController.GetModel());
            }

            // Update UI
            UpdateUI();
        }

        public void RemoveOrDecrementItems(ItemController _itemController)
        {
            // Check if an item with the same ID already exists
            var existingController = itemControllers.Find(c => c.GetModel().Id == _itemController.GetModel().Id);

            if (existingController != null)
            {
                // Reduce the quantity of the existing item
                int quantityToRemove = _itemController.GetModel().Quantity;
                existingController.UpdateItemQuantity(-quantityToRemove);

                // If quantity becomes zero or less, remove the item completely
                if (existingController.GetModel().Quantity <= 0)
                {
                    // Remove from the item list and inventory model
                    inventoryModel.RemoveItem(existingController.GetModel());
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
            // Update UI
            inventoryView.UpdateUI(uiService);
        }

        // Getters
        public List<ItemController> GetItems() => itemControllers;
        public InventoryModel GetModel() => inventoryModel;
    }
}