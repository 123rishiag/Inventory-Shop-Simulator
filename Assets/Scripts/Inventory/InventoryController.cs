using ServiceLocator.Item;
using ServiceLocator.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Inventory
{
    public class InventoryController
    {
        private InventoryScriptableObject inventoryScriptableObject;
        private ItemService itemService;
        private UIService uiService;

        private InventoryModel inventoryModel;
        private InventoryView inventoryView;
        private List<ItemController> itemControllers;

        public InventoryController(InventoryScriptableObject _scriptableObject, ItemService _itemService, UIService _uiService)
        {
            inventoryScriptableObject = _scriptableObject;
            itemService = _itemService;
            uiService = _uiService;

            // Instantiating Model
            inventoryModel = new InventoryModel(_scriptableObject);

            // Instantiating View
            inventoryView = new InventoryView(this);

            // Controller List
            itemControllers = new List<ItemController>();
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
        }

        public void AddNewItem(ItemScriptableObject _itemScriptableObject, int _quantity = -1)
        {
            var itemController = itemService.CreateItem(_itemScriptableObject, uiService.GetInventoryGrid(),
                inventoryScriptableObject.itemPrefab);

            itemControllers.Add(itemController);
            // Setting EntityType of Item
            itemController.GetModel().UISection = GetModel().UISection;

            // Add to Model
            inventoryModel.AddItem(itemController.GetModel());

            // If the item does not exists,
            // happens when no transaction but only item addition happens during initialization or gathering resources
            // then only update weight, not currency
            if (_quantity == -1)
            {
                itemController.UpdateItemQuantity(itemController.GetModel().Quantity, false);
                // Updating Inventory Metrics
                inventoryModel.UpdateUI(0,
                    itemController.GetModel().Weight * itemController.GetModel().Quantity);
            }
            else
            {
                itemController.UpdateItemQuantity(_quantity, false);
                // Updating Inventory Metrics
                inventoryModel.UpdateUI(itemController.GetModel().BuyingPrice * (-1 * _quantity),
                    itemController.GetModel().Weight * _quantity);
            }

            UpdateUI();
        }

        public bool AddOrIncrementItems(ItemController _itemController, out string _transactionFailReason, int _quantity = 1)
        {
            if(!CheckMetricConditions(_itemController, out _transactionFailReason, _quantity))
            {
                return false;
            }
     
            // Check if an item with the same ID already exists
            var existingItemController = itemControllers.Find(c => c.GetModel().Id == _itemController.GetModel().Id);

            if (existingItemController != null)
            {
                // If the item exists, update its quantity
                existingItemController.UpdateItemQuantity(_quantity, true);

                // Updating Inventory Metrics
                inventoryModel.UpdateUI(existingItemController.GetModel().BuyingPrice * (-1 * _quantity),
                    existingItemController.GetModel().Weight * _quantity);
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

                // Updating Inventory Metrics
                inventoryModel.UpdateUI(existingItemController.GetModel().SellingPrice * _quantity,
                    existingItemController.GetModel().Weight * (-1 * _quantity));

                // If quantity becomes zero or less, remove the item completely
                if (existingItemController.GetModel().Quantity <= 0)
                {
                    // Remove from the item list and inventory model
                    inventoryModel.RemoveItem(existingItemController.GetModel());
                    itemControllers.Remove(existingItemController);

                    // Remove Item
                    existingItemController.GetView().DestroyView();
                }
            }

            // Update UI
            UpdateUI();
        }

        private bool CheckMetricConditions(ItemController _itemController, out string _transactionFailReason, int _quantity)
        {
            _transactionFailReason = string.Empty;
            // If Item does not have that much quantity in other UISection, return false
            if (_itemController.GetModel().Quantity < _quantity)
            {
                _transactionFailReason = "Shop doesn't have that many items.";
                return false;
            }

            // If Item Buying Price is greater than the current currency, return false
            if (_itemController.GetModel().BuyingPrice * _quantity > inventoryModel.Currency)
            {
                _transactionFailReason = "Not Enough Money!!!!.";
                return false;
            }

            // If the inventory does not have that much space left in Inventory, return false
            if (_itemController.GetModel().Weight * _quantity > (inventoryModel.MaxWeight - inventoryModel.CurrentWeight))
            {
                _transactionFailReason = "Not Enough Space!!!!.";
                return false;
            }

            return true;
        }

        private void UpdateUI()
        {
            // Show Items
            inventoryView.ShowItems();

            // Update UI
            inventoryView.UpdateUI(uiService);
        }        

        // Getters
        public List<ItemController> GetItems() => itemControllers;
        public InventoryModel GetModel() => inventoryModel;
    }
}