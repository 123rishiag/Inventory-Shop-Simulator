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
            inventoryService = new InventoryService(inventoryScriptableObject);
            shopService = new ShopService(shopScriptableObject);
            itemService = new ItemService();
        }

        private void InjectDependencies()
        {
            inventoryService.Init(itemService, uiService);
            shopService.Init(itemService, uiService);
            itemService.Init(inventoryService, shopService);
            uiService.Init();
        }
    }
}
