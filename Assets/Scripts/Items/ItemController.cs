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
            itemView = ItemView.CreateView(this, _parentGrid, _itemPrefab);
        }

        public void ProcessItem()
        {
           switch(itemModel.UISection)
            {
                case UISection.Inventory:
                    inventoryService.SellItems(this);
                    break;
                case UISection.Shop:
                    shopService.BuyItems(this);
                    break;
                default:
                    break;
            }
        }

        // Getters
        public ItemModel GetModel() => itemModel;
        public ItemView GetView() => itemView;

        // Setters
        public void UpdateItemQuantity(int _quantity, bool _isDelta)
        {
            itemModel.UpdateQuantity(_quantity, _isDelta);
            itemView.UpdateQuantity();
        }
    }
}