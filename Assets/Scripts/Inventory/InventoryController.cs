using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ServiceLocator.Inventory
{
    public class InventoryController
    {
        private InventoryScriptableObject inventoryScriptableObject;
        private EventService eventService;

        private InventoryModel inventoryModel;
        private InventoryView inventoryView;
        private List<ItemController> itemControllers;

        public InventoryController(EventService _eventService, InventoryScriptableObject _scriptableObject)
        {
            inventoryScriptableObject = _scriptableObject;
            eventService = _eventService;

            // Instantiating Model
            inventoryModel = new InventoryModel(_scriptableObject);

            // Instantiating View
            inventoryView = new InventoryView(this);

            // Controller List
            itemControllers = new List<ItemController>();

            // Adding Event Listeners
            eventService.OnSellItemEvent.AddListener(SellItems);
            eventService.OnInventoryAddItemEvent.AddListener(AddOrIncrementItems);
        }
        ~InventoryController()
        {
            // Removing Event Listeners
            eventService.OnSellItemEvent.RemoveListener(SellItems);
            eventService.OnInventoryAddItemEvent.RemoveListener(AddOrIncrementItems);
        }

        public void AddButtonToPanel(string _menuButtonText)
        {
            // Instantiating the button
            GameObject newButton = eventService.OnCreateMenuButtonViewEvent.Invoke<GameObject>(inventoryModel.UISection, _menuButtonText);

            // Setting up button logic (e.g., click events)
            Button button = newButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnButtonClicked());
                inventoryView.AddButtonToView(button);
            }
            else
            {
                Debug.LogWarning("Button component not found in button prefab.");
            }
        }

        private void OnButtonClicked()
        {
            GatherResources();
        }

        private void SellItems(ItemModel _itemModel, int _quantity)
        {
            bool isTransacted = eventService.OnShopAddItemEvent.Invoke<bool>(_itemModel, _quantity);
            if (isTransacted)
            {
                eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ItemSold);
                RemoveOrDecrementItems(_itemModel, _quantity);
            }
        }

        public void AddNewItem(ItemScriptableObject _itemData, int _quantity = -1)
        {
            var itemController = eventService.OnCreateItemEvent.Invoke<ItemController>(_itemData, inventoryModel.UISection);

            itemControllers.Add(itemController);
            // Setting EntityType of Item
            itemController.GetModel().UISection = GetModel().UISection;

            // Add to Model
            inventoryModel.AddItem(itemController.GetModel());

            // If the item does not exists,
            // happens when no transaction but only item addition happens during initialization or gathering resources
            // then only update weight, not currency with quantity given in Scriptable Objects for initialization
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

                // Updating Inventory Metrics
                inventoryModel.UpdateUI(existingItemController.GetModel().BuyingPrice * (-1 * _quantity),
                    existingItemController.GetModel().Weight * _quantity);
            }
            else
            {
                // Add New Item
                AddNewItem(_itemModel.ItemData, _quantity);
            }

            // Update UI
            UpdateUI();

            // Displaying Successful Transaction Popup
            transactionMessage = $"{_quantity}x {_itemModel.Name} bought worth " +
                    $"{_itemModel.BuyingPrice * _quantity}A!!!!";
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
                    eventService.OnDestroyItemEvent.Invoke(inventoryModel.UISection, existingItemController.GetModel().Id);
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
                _transactionMessage = "Shop doesn't have that many items.";
                return false;
            }

            // If Item Buying Price is greater than the current currency, return false
            if (_itemModel.BuyingPrice * _quantity > inventoryModel.Currency)
            {
                _transactionMessage = "Not Enough Money!!!!";
                return false;
            }

            // If the inventory does not have that much space left in Inventory, return false
            if (_itemModel.Weight * _quantity > (inventoryModel.MaxWeight - inventoryModel.CurrentWeight))
            {
                _transactionMessage = "Not Enough Space!!!!";
                return false;
            }

            return true;
        }

        public bool CheckGatherResources(out string _transactionMessage)
        {
            _transactionMessage = string.Empty;

            // Ensuring the item pool exists
            if (inventoryScriptableObject.itemDatabase.itemList == null ||
                inventoryScriptableObject.itemDatabase.itemList.Count == 0)
            {
                _transactionMessage = "No items available in the database to gather. \n\nGather Resource Disabled";
                return false;
            }

            // Ensuring inventory has space
            var items = inventoryScriptableObject.itemDatabase.itemList;
            var minWeightOfItems = items.Min(item => (float)item.weight);

            if ((inventoryModel.MaxWeight == inventoryModel.CurrentWeight))
            {
                _transactionMessage = "No Space Left!!!! \n\nGather Resource Disabled";
                return false;
            }

            if ((inventoryModel.MaxWeight - inventoryModel.CurrentWeight)
                < minWeightOfItems)
            {
                _transactionMessage = "Not Enough Space for Lightest item to Gather!!!! \n\nGather Resource Disabled";
                return false;
            }

            // Selecting a random item from the global pool based on rarity
            var itemData = GetRandomItemDataBasedOnRarity();
            if (itemData == null)
            {
                _transactionMessage = "No suitable items found!!!! \n\nGather Resource Disabled";
                return false;
            }

            return true;
        }

        private void GatherResources()
        {
            eventService.OnPlaySoundEffectEvent.Invoke(SoundType.GatherResource);

            string transactionMessage;
            if (!CheckGatherResources(out transactionMessage))
            {
                eventService.OnPopupNotificationEvent.Invoke(transactionMessage);
                eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ErrorFeedback);
                inventoryView.SetButtonInteractivity(eventService, false);
                return;
            }

            var itemData = GetRandomItemDataBasedOnRarity();

            // Setting quantity to 1  as whenever gather resources is clicked only one quantity is to be added
            int quantity = 1;

            // Check if an item with the same ID already exists
            var existingItemController = itemControllers.Find(c => c.GetModel().Id == itemData.Id);

            if (existingItemController != null)
            {
                // If the item exists, update its quantity
                existingItemController.UpdateItemQuantity(quantity, true);

                // Updating Inventory Metrics
                inventoryModel.UpdateUI(0,
                    existingItemController.GetModel().Weight * quantity);
            }
            else
            {
                // Add New Item
                AddNewItem(itemData, quantity);
            }

            // Update UI
            UpdateUI();
        }

        private ItemScriptableObject GetRandomItemDataBasedOnRarity()
        {
            // Fetching all items
            var items = inventoryScriptableObject.itemDatabase.itemList;

            // Inverting rarity weights: Higher enum value -> Lower weight
            var maxRarityValue = Enum.GetValues(typeof(ItemRarity)).Cast<int>().Max() + 1;
            var rarityWeights = items.ToDictionary(item => item,
                                                   item => maxRarityValue - (int)item.rarity);

            // Calculating total rarity weight based on inverted weights
            var totalRarityWeight = rarityWeights.Sum(rw => rw.Value);

            int randomValue = Random.Range(0, totalRarityWeight);
            int cumulativeWeight = 0;

            // Selecting item based on cumulative weight
            foreach (var item in items)
            {
                cumulativeWeight += rarityWeights[item];

                // Skipping items that would exceed the max weight
                float potentialWeight = inventoryModel.CurrentWeight + item.weight;
                if (potentialWeight > inventoryModel.MaxWeight)
                {
                    continue;
                }

                if (randomValue < cumulativeWeight)
                {
                    return item;
                }
            }

            return null;
        }

        public void UpdateUI()
        {
            // Update UI
            inventoryView.UpdateUI(eventService);
        }

        // Getters
        public List<ItemController> GetItems() => itemControllers;
        public InventoryModel GetModel() => inventoryModel;
    }
}