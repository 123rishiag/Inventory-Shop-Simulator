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
            TestEventService();
        }

        private void CreateServices()
        {
            eventService = new EventService();
            inventoryService = new InventoryService(inventoryScriptableObject);
            shopService = new ShopService(shopScriptableObject);
            itemService = new ItemService();
        }

        private void InjectDependencies()
        {
            eventService.Init();
            uiService.Init();
            itemService.Init(inventoryService, shopService, uiService);
            shopService.Init(itemService, uiService, inventoryService);
            inventoryService.Init(itemService, uiService, shopService);
        }

        private void TestEventService()
        {
            eventService.OnNoParametersEvent.AddListener(TestNoParameter);
            eventService.OnOneParametersEvent.AddListener(TestOneParameter);
            eventService.OnTwoParametersEvent.AddListener(TestTwoParameter);

            eventService.OnNoParametersEvent.Invoke();
            eventService.OnOneParametersEvent.Invoke(1);
            eventService.OnTwoParametersEvent.Invoke(1, 2);

            eventService.OnNoParametersEvent.RemoveListener(TestNoParameter);
            eventService.OnOneParametersEvent.RemoveListener(TestOneParameter);
            eventService.OnTwoParametersEvent.RemoveListener(TestTwoParameter);
        }

        private void TestNoParameter()
        {
            Debug.Log($"Invoked EventService with No Parameters!!");
        }

        private void TestOneParameter(int _param)
        {
            Debug.Log($"Invoked EventService with One Parameters [{_param}]!!");
        }

        private void TestTwoParameter(int _param1, int _param2)
        {
            Debug.Log($"Invoked EventService with No Parameters [{_param1}, {_param2}]!!");
        }

    }
}