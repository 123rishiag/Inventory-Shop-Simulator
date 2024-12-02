using ServiceLocator.Event;
using ServiceLocator.Inventory;
using ServiceLocator.Item;
using ServiceLocator.Shop;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        // Services
        private EventService eventService;
        private InventoryService inventoryService;
        private ShopService shopService;
        private ItemService itemService;

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        // Scriptable Objects
        [SerializeField] private InventoryScriptableObject inventoryScriptableObject;
        [SerializeField] private ShopScriptableObject shopScriptableObject;

        private void Start()
        {
            CreateServices();
            InjectDependencies();
        }

        private void CreateServices()
        {
            eventService = new EventService();
            // UI Service is self instantitated
            itemService = new ItemService(eventService);
            inventoryService = new InventoryService(inventoryScriptableObject);
            shopService = new ShopService(shopScriptableObject);
        }

        private void InjectDependencies()
        {
            eventService.Init();
            uiService.Init(eventService);
            itemService.Init(inventoryService, shopService, uiService);
            inventoryService.Init(shopService, eventService);
            shopService.Init(inventoryService, eventService);
        }
    }
}