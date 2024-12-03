using ServiceLocator.Event;
using ServiceLocator.Inventory;
using ServiceLocator.Item;
using ServiceLocator.Shop;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        // Services
        private EventService eventService;
        private SoundService soundService;
        private InventoryService inventoryService;
        private ShopService shopService;
        private ItemService itemService;
        [SerializeField] private UIService uiService;

        // Scriptable Objects
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private InventoryScriptableObject inventoryScriptableObject;
        [SerializeField] private ShopScriptableObject shopScriptableObject;

        // Audio Sources:
        [SerializeField] private AudioSource SFXSource;
        [SerializeField] private AudioSource BGSource;

        private void Start()
        {
            CreateServices();
        }

        private void CreateServices()
        {
            eventService = new EventService();
            soundService = new SoundService(eventService, soundScriptableObject, SFXSource, BGSource);
            uiService.Init(eventService);
            itemService = new ItemService(eventService);
            inventoryService = new InventoryService(eventService, inventoryScriptableObject);
            shopService = new ShopService(eventService, shopScriptableObject);
        }
    }
}