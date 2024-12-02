using ServiceLocator.Event;
using ServiceLocator.Inventory;
using ServiceLocator.Shop;
using ServiceLocator.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Item
{
    public class ItemController
    {
        private InventoryService inventoryService;
        private ShopService shopService;
        private UIService uiService;
        private EventService eventService;

        private ItemModel itemModel;
        private ItemView itemView;

        public ItemController(InventoryService _inventoryService, ShopService _shopService, UIService _uIService, EventService _eventService,
            ItemScriptableObject _itemScriptableObject, UISection _uiSection)
        {
            // Setting the services
            inventoryService = _inventoryService;
            shopService = _shopService;
            uiService = _uIService;
            eventService = _eventService;

            // Creating the Model
            itemModel = new ItemModel(_itemScriptableObject);

            // Creating the View
            itemView = ItemView.CreateView(this, eventService, _uiSection);
        }

        public void ProcessItem()
        {
            uiService.UpdateItemMenuUI(itemModel);
            GameObject itemMenuButton = uiService.GetItemMenuButton();

            Button button = itemMenuButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => uiService.OnSetItemQuanity(itemModel, quantity =>
                {
                    uiService.OnTransactionConfirmation(itemModel, quantity, isTransacted =>
                    {
                        if (isTransacted)
                        {
                            switch (itemModel.UISection)
                            {
                                case UISection.Inventory:
                                    inventoryService.SellItems(this, quantity);
                                    break;
                                case UISection.Shop:
                                    shopService.BuyItems(this, quantity);
                                    break;
                                default:
                                    break;
                            }
                        }
                    });
                    uiService.HideItemPanel();
                }));
            }
            else
            {
                Debug.LogWarning("Button component not found in button prefab.");
            }
        }

        // Getters
        public ItemModel GetModel() => itemModel;
        public ItemView GetView() => itemView;

        // Setters
        public void UpdateItemQuantity(int _quantity, bool _isDelta)
        {
            itemModel.UpdateQuantity(_quantity, _isDelta);
            itemView.UpdateQuantity();
        }
    }
}