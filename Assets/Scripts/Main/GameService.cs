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

        // Scriptable Objects
        [SerializeField] private InventoryScriptableObject inventoryScriptableObject;
        [SerializeField] private ShopScriptableObject shopScriptableObject;

        private void Start()
        {
            CreateServices();
        }

        private void CreateServices()
        {
            eventService = new EventService();
            uiService.Init(eventService);
            itemService = new ItemService(eventService);
            inventoryService = new InventoryService(eventService, inventoryScriptableObject);
            shopService = new ShopService(eventService, shopScriptableObject);
        }
    }
}