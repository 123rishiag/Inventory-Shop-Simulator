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
            ItemScriptableObject _itemData, UISection _uiSection)
        {
            // Setting the services
            inventoryService = _inventoryService;
            shopService = _shopService;
            uiService = _uIService;
            eventService = _eventService;

            // Creating the Model
            itemModel = new ItemModel(_itemData);

            // Creating the View
            itemView = _eventService.OnCreateItemButtonViewEvent.Invoke<GameObject>(_uiSection).GetComponent<ItemView>();
            SetListener();

            // Adding Event Listeners
            eventService.OnShowItemEvent.AddListener(ShowItem);
            eventService.OnDestroyItemEvent.AddListener(DestroyItem);
        }
        ~ItemController()
        {
            // Removing Event Listeners
            eventService.OnShowItemEvent.RemoveListener(ShowItem);
            eventService.OnDestroyItemEvent.RemoveListener(DestroyItem);
        }

        private void SetListener()
        {
            if (itemView == null)
            {
                Debug.LogError("ItemView: Prefab does not have an ItemView component!");
                return;
            }

            itemView.SetController(this);

            // Add OnClick listener
            Button button = itemView.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => ProcessItem());
            }
            else
            {
                Debug.LogError("ItemView: Prefab does not have a Button component!");
            }
        }

        private void ProcessItem()
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
                                    inventoryService.SellItems(itemModel, quantity);
                                    break;
                                case UISection.Shop:
                                    shopService.BuyItems(itemModel, quantity);
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

        private void ShowItem(UISection _uiSection, ItemType _itemType)
        {
            if (itemModel.UISection == _uiSection)
            {
                itemView.HideView();
                if (_itemType == ItemType.All || itemModel.Type == _itemType)
                {
                    itemView.ShowView();
                }
            }
        }

        private void DestroyItem(UISection _uiSection, int _itemId)
        {
            if (itemModel.UISection == _uiSection && itemModel.Id == _itemId)
            {
                itemView.DestroyView();
            }
        }

        // Getters
        public ItemModel GetModel() => itemModel;

        // Setters
        public void UpdateItemQuantity(int _quantity, bool _isDelta)
        {
            itemModel.UpdateQuantity(_quantity, _isDelta);
            itemView.UpdateQuantity();
        }
    }
}