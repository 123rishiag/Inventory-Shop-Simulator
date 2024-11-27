using ServiceLocator.Item;
using ServiceLocator.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Inventory
{
    public class InventoryController
    {
        private UIService uiService;

        private InventoryModel inventoryModel;
        private InventoryView inventoryView;
        private List<ItemController> itemControllers;

        public InventoryController(UIService _uiService, InventoryScriptableObject _scriptableObject)
        {
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

        public void AddButtonToPanel(GameObject _menuButtonPrefab, Transform _menuButtonPanel, string _menuButtonText)
        {
            // Instantiating the button
            GameObject newButton = inventoryView.CreateButton(_menuButtonPrefab, _menuButtonPanel, _menuButtonText);

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

        public void AddItem(ItemScriptableObject _itemData, GameObject _itemPrefab)
        {
            // Create ItemController
            var itemController = new ItemController(_itemData, uiService.GetInventoryGrid(), _itemPrefab);
            itemControllers.Add(itemController);

            // Add to Model
            inventoryModel.AddItem(itemController.GetModel());

            // Update UI
            UpdateUI();
        }

        public void RemoveItem(ItemController _itemController)
        {
            if (itemControllers.Contains(_itemController))
            {
                // Remove from Model
                inventoryModel.RemoveItem(_itemController.GetModel());

                // Remove from Controller List
                itemControllers.Remove(_itemController);

                // Destroy the Item View
                _itemController.GetView().DestroyView();

                // Update UI
                UpdateUI();
            }
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