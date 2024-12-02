using ServiceLocator.Event;
using ServiceLocator.Inventory;
using ServiceLocator.Shop;
using ServiceLocator.UI;

namespace ServiceLocator.Item
{
    public class ItemService
    {
        private InventoryService inventoryService;
        private ShopService shopService;
        private UIService uiService;
        private EventService eventService;
        public ItemService(EventService _eventService) 
        {
            eventService = _eventService;
            eventService.OnCreateItemEvent.AddListener(CreateItem);
        }
        ~ItemService()
        {
            eventService.OnCreateItemEvent.RemoveListener(CreateItem);
        }

        public void Init(InventoryService _inventoryService, ShopService _shopService, UIService _uIService)
        {
            inventoryService = _inventoryService;
            shopService = _shopService;
            uiService = _uIService;
        }

        private ItemController CreateItem(ItemScriptableObject _itemData, UISection _uiSection)
        {
            return new ItemController(inventoryService, shopService, uiService, eventService, _itemData, _uiSection);
        }
    }
}
