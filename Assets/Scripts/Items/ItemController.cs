using ServiceLocator.Inventory;
using ServiceLocator.Shop;
using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemController
    {
        private InventoryService inventoryService;
        private ShopService shopService;

        private ItemModel itemModel;
        private ItemView itemView;

        public ItemController(InventoryService _inventoryService, ShopService _shopService, 
            ItemScriptableObject _itemScriptableObject, Transform _parentGrid, GameObject _itemPrefab)
        {
            // Setting the services
            inventoryService = _inventoryService;
            shopService = _shopService;

            // Creating the Model
            itemModel = new ItemModel(_itemScriptableObject);

            // Creating the View
            itemView = ItemView.CreateView(_parentGrid, this, _itemPrefab);
        }

        public void OnItemClick()
        {
            Debug.Log($"Item {itemModel.Id} clicked! Performing actions.");
            // Add your click handling logic here
        }

        // Getters
        public ItemModel GetModel() => itemModel;
        public ItemView GetView() => itemView;

        // Setters
        public void UpdateItemQuantity(int _delta)
        {
            itemModel.UpdateQuantity(_delta);
            itemView.UpdateQuantity();
        }
    }
}