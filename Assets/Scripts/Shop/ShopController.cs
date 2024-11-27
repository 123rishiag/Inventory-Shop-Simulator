using ServiceLocator.Item;
using ServiceLocator.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Shop
{
    public class ShopController
    {
        private UIService uiService;

        private ShopModel shopModel;
        private ShopView shopView;
        private List<ItemController> itemControllers;
        private List<ItemController> filteredItemControllers;

        public ShopController(UIService _uiService)
        {
            uiService = _uiService;

            // Instantiating Model
            shopModel = new ShopModel();

            // Instantiating View
            shopView = new ShopView(this);

            // Controllers List
            itemControllers = new List<ItemController>();
            filteredItemControllers = new List<ItemController>();

            // Selected Item Type Filter
            shopModel.SelectedItemType = ItemType.All;

            // Show Filtered Items
            ShowFilteredItems();
        }

        public void AddButtonToPanel(GameObject _menuButtonPrefab, Transform _menuButtonPanel, ItemType _itemType)
        {
            // Instantiating the button
            GameObject newButton = shopView.CreateButton(_menuButtonPrefab, _menuButtonPanel, _itemType);

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
            ShowFilteredItems();
        }

        public void AddItem(ItemScriptableObject _itemData, GameObject _itemPrefab)
        {
            // Create ItemController
            var itemController = new ItemController(_itemData, uiService.GetShopGrid(), _itemPrefab);
            itemControllers.Add(itemController);

            // Add to Model
            shopModel.AddItem(itemController.GetModel());

            // Show New Filtered Items
            ShowFilteredItems();
        }

        public void RemoveItem(ItemController _itemController)
        {
            if (itemControllers.Contains(_itemController))
            {
                // Remove from Model
                shopModel.RemoveItem(_itemController.GetModel());

                // Remove from Controller List
                itemControllers.Remove(_itemController);

                // Destroy the Item View
                _itemController.DestroyView();

                // Show New Filtered Items
                ShowFilteredItems();
            }
        }

        private void ShowFilteredItems()
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
        public ShopModel GetShopModel() => shopModel;
    }
}