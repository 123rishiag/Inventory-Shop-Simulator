using ServiceLocator.Inventory;
using ServiceLocator.Shop;
using ServiceLocator.Item;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {
        // Services
        private ShopService shopService;

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        // Scriptable Objects
        [SerializeField] private ItemDatabase inventoryItemDatabase;
        [SerializeField] private InventoryConfigScriptableObject inventoryConfig;

        [SerializeField] private ShopScriptableObject shopScriptableObject;

        private void Start()
        {
            CreateServices();
            InjectDependencies();
        }

        private void CreateServices()
        {
            shopService = new ShopService(shopScriptableObject);
        }

        private void InjectDependencies()
        {
            shopService.Init(uiService);
            uiService.Init();
        }
    }
}
