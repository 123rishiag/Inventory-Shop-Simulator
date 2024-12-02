using ServiceLocator.Event;
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
        private EventService eventService;
        public ItemService() { }

        public void Init(InventoryService _inventoryService, ShopService _shopService, UIService _uIService, EventService _eventService)
        {
            inventoryService = _inventoryService;
            shopService = _shopService;
            uiService = _uIService;
            eventService = _eventService;
        }

        public ItemController CreateItem(ItemScriptableObject _itemScriptableObject, UISection _uiSection)
        {
            return new ItemController(inventoryService, shopService, uiService, eventService, _itemScriptableObject, _uiSection);
        }
    }
}
