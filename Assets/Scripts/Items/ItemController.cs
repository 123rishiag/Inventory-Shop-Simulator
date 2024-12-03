using ServiceLocator.Event;
using ServiceLocator.Sound;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Item
{
    public class ItemController
    {
        private EventService eventService;

        private ItemModel itemModel;
        private ItemView itemView;

        public ItemController(EventService _eventService, GameObject _menubutton, ItemScriptableObject _itemData, UISection _uiSection)
        {
            // Setting the services
            eventService = _eventService;

            // Creating the Model
            itemModel = new ItemModel(_itemData);

            // Creating the View
            itemView = _eventService.OnCreateItemButtonViewEvent.Invoke<GameObject>(_uiSection).GetComponent<ItemView>();
            SetListener(_menubutton);

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

        private void SetListener(GameObject _menubutton)
        {
            if (itemView == null)
            {
                Debug.LogError("ItemView: Prefab does not have an ItemView component!");
                return;
            }

            itemView.SetView(this, _menubutton);

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
            GameObject itemMenuButton = itemView.GetMenuButton();
            eventService.OnItemClickEvent.Invoke(itemModel, itemMenuButton);
            eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ButtonClick);

            Button button = itemMenuButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                GetItemTransactionStatus((_quantity, _isTransacted) =>
                ProcessTransaction(_quantity, _isTransacted)
                )
                );
            }
            else
            {
                Debug.LogWarning("Button component not found in button prefab.");
            }
        }

        private void GetItemTransactionStatus(Action<int, bool> _callback)
        {
            eventService.OnBuySellButtonClickEvent.Invoke(itemModel, _callback);
        }
        private void ProcessTransaction(int _quantity, bool _isTransacted)
        {
            if (_isTransacted)
            {
                switch (itemModel.UISection)
                {
                    case UISection.Inventory:
                        eventService.OnSellItemEvent.Invoke(itemModel, _quantity);
                        break;
                    case UISection.Shop:
                        eventService.OnBuyItemEvent.Invoke(itemModel, _quantity);
                        break;
                    default:
                        break;
                }
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