using ServiceLocator.Inventory;
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
        }

        private void InjectDependencies()
        {
            shopService.Init(uiService);
            inventoryService.Init(uiService);
            uiService.Init();
        }
    }
}
