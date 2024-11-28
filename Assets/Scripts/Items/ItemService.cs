using ServiceLocator.Inventory;
using ServiceLocator.Shop;
using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemService
    {
        private InventoryService inventoryService;
        private ShopService shopService;
        public ItemService() { }

        public void Init(InventoryService _inventoryService, ShopService _shopService)
        {
            inventoryService = _inventoryService;
            shopService = _shopService;
        }

        public ItemController CreateItem(ItemScriptableObject _itemScriptableObject, Transform _parentGrid, GameObject _itemPrefab)
        {
            return new ItemController(inventoryService, shopService, _itemScriptableObject, _parentGrid, _itemPrefab);
        }
    }
}
