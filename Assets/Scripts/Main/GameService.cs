using ServiceLocator.Inventory;
using ServiceLocator.Item;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : MonoBehaviour
    {

        // Services
        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        // Scriptable Objects
        [SerializeField] private ItemDatabase inventoryItemDatabase;
        [SerializeField] private ItemDatabase shopItemDatabase;
        [SerializeField] private InventoryConfigScriptableObject inventoryConfig;

        private void Start()
        {
            // Initializaing Services
            uiService.Init();
        }
    }
}
