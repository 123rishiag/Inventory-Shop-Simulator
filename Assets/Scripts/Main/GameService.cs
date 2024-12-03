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
        private UIService uiService;
        private ItemService itemService;
        private InventoryService inventoryService;
        private ShopService shopService;

        // Scriptable Objects
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private InventoryScriptableObject inventoryScriptableObject;
        [SerializeField] private ShopScriptableObject shopScriptableObject;

        // Audio Sources:
        [SerializeField] private AudioSource SFXSource;
        [SerializeField] private AudioSource BGSource;

        // UI Canvas
        [SerializeField] private UIView uiCanvas;

        private void Start()
        {
            CreateServices();
        }

        private void CreateServices()
        {
            eventService = new EventService();
            soundService = new SoundService(eventService, soundScriptableObject, SFXSource, BGSource);
            uiService = new UIService(eventService, uiCanvas);
            itemService = new ItemService(eventService);
            inventoryService = new InventoryService(eventService, inventoryScriptableObject);
            shopService = new ShopService(eventService, shopScriptableObject);
        }
    }
}