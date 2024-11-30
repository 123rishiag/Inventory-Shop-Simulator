using ServiceLocator.Inventory;
using ServiceLocator.Shop;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemService
    {
        private InventoryService inventoryService;
        private ShopService shopService;
        private UIService uiService;
        public ItemService() { }

        public void Init(InventoryService _inventoryService, ShopService _shopService, UIService _uIService)
        {
            inventoryService = _inventoryService;
            shopService = _shopService;
            uiService = _uIService;
        }

        public ItemController CreateItem(ItemScriptableObject _itemScriptableObject, Transform _parentGrid, GameObject _itemPrefab)
        {
            return new ItemController(inventoryService, shopService, uiService, _itemScriptableObject, _parentGrid, _itemPrefab);
        }
    }
}
