using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.Sound;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class UIController
    {
        private EventService eventService;
        private UIModel uiModel;
        private UIView uiView;

        public UIController(EventService _eventService, UIView _uiCanvas)
        {
            eventService = _eventService;

            uiModel = new UIModel();
            uiView = _uiCanvas.GetComponent<UIView>();

            // Adding Event Listeners
            eventService.OnCreateMenuButtonViewEvent.AddListener(CreateMenuButtonPrefab);
            eventService.OnCreateItemButtonViewEvent.AddListener(CreateItemButtonPrefab);
            eventService.OnBuySellButtonClickEvent.AddListener(GetItemTransactionStatus);
            eventService.OnPopupNotificationEvent.AddListener(PopupNotification);
            eventService.OnSetButtonInteractionEvent.AddListener(SetButtonInteractivity);
            eventService.OnInventoryUpdatedEvent.AddListener(UpdateInventoryUI);
            eventService.OnShopUpdatedEvent.AddListener(UpdateShopUI);
            eventService.OnItemClickEvent.AddListener(UpdateItemMenuUI);
        }
        ~UIController()
        {
            // Removing Event Listeners
            eventService.OnCreateMenuButtonViewEvent.RemoveListener(CreateMenuButtonPrefab);
            eventService.OnCreateItemButtonViewEvent.RemoveListener(CreateItemButtonPrefab);
            eventService.OnBuySellButtonClickEvent.RemoveListener(GetItemTransactionStatus);
            eventService.OnPopupNotificationEvent.RemoveListener(PopupNotification);
            eventService.OnSetButtonInteractionEvent.RemoveListener(SetButtonInteractivity);
            eventService.OnInventoryUpdatedEvent.RemoveListener(UpdateInventoryUI);
            eventService.OnShopUpdatedEvent.RemoveListener(UpdateShopUI);
            eventService.OnItemClickEvent.RemoveListener(UpdateItemMenuUI);
        }

        private GameObject CreateMenuButtonPrefab(UISection _uISection, string _menuButtonText)
        {
            return uiView.CreateMenuButtonPrefab(_uISection, _menuButtonText);
        }
        private GameObject CreateItemButtonPrefab(UISection _uISection)
        {
            return uiView.CreateItemButtonPrefab(_uISection);
        }
        private void GetItemTransactionStatus(ItemModel _itemModel, Action<int, bool> _callback)
        {
            // Setting the item quantity first
            SetItemQuanity(_itemModel, _quantity =>
            {
                // After getting the quantity, proceeding to get transaction confirmation
                GetTransactionConfirmation(_itemModel, _quantity, _isTransacted =>
                {
                    // Passing the result back through the callback
                    _callback.Invoke(_quantity, _isTransacted);
                });
            });
        }
        private void SetItemQuanity(ItemModel _itemModel, Action<int> _callback)
        {
            eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ConfirmationPopup);

            // Setting Initial Values
            int minQuantity = 1;
            int currentQuantity = minQuantity;
            int maxQuantity = _itemModel.Quantity;

            // Fetching buttons
            Button decrementbutton;
            Button incrementbutton;
            Button doneButton;

            (decrementbutton, incrementbutton, doneButton) = uiView.CreateItemQuanityButtons(_itemModel, currentQuantity);
            // Decrement Button Setup
            if (decrementbutton != null)
            {
                decrementbutton.onClick.RemoveAllListeners();
                decrementbutton.onClick.AddListener(() =>
                {
                    currentQuantity = uiView.OnQuantityDecrementButton(decrementbutton, incrementbutton,
                        minQuantity, currentQuantity);
                    eventService.OnPlaySoundEffectEvent.Invoke(SoundType.QuantitySelect);
                }
                );

                // Initial Button Status
                if (currentQuantity == minQuantity)
                {
                    decrementbutton.interactable = false;
                }
                else
                {
                    decrementbutton.interactable = true;
                }
            }

            // Increment Button Setup
            if (incrementbutton != null)
            {
                incrementbutton.onClick.RemoveAllListeners();
                incrementbutton.onClick.AddListener(() =>
                {
                    currentQuantity = uiView.OnQuantityIncrementButton(decrementbutton, incrementbutton,
                        maxQuantity, currentQuantity);
                    eventService.OnPlaySoundEffectEvent.Invoke(SoundType.QuantitySelect);
                }
                );

                // Initial Button Status
                if (currentQuantity == maxQuantity)
                {
                    incrementbutton.interactable = false;
                }
                else
                {
                    incrementbutton.interactable = true;
                }
            }

            // Done Button Setup
            if (doneButton != null)
            {
                doneButton.onClick.RemoveAllListeners();
                doneButton.onClick.AddListener(() =>
                {
                    uiView.GetQuantitySelectionPanel().SetActive(false);
                    _callback.Invoke(currentQuantity);
                }
                );
            }
        }
        private void GetTransactionConfirmation(ItemModel _itemModel, int _quantity, Action<bool> _callback)
        {
            eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ConfirmationPopup);

            // Fetching UI Elements
            Button yesButton;
            Button noButton;

            (yesButton, noButton) = uiView.CreateTransactionConfirmationButtons(_itemModel, _quantity);

            if (yesButton != null)
            {
                yesButton.onClick.RemoveAllListeners();
                yesButton.onClick.AddListener(() =>
                {
                    uiView.GetTransactionConfirmationPanel().SetActive(false);
                    eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ButtonClick);
                    _callback.Invoke(true);
                }
                );
            }

            if (noButton != null)
            {
                noButton.onClick.RemoveAllListeners();
                noButton.onClick.AddListener(() =>
                {
                    uiView.GetTransactionConfirmationPanel().SetActive(false);
                    eventService.OnPlaySoundEffectEvent.Invoke(SoundType.ButtonClick);
                    _callback.Invoke(false);
                }
                );
            }
        }
        private void PopupNotification(string _text)
        {
            uiView.PopupNotification(_text);
        }
        private void SetButtonInteractivity(Button _button, bool _isInteractable)
        {
            uiView.SetButtonInteractivity(_button, _isInteractable);
        }
        private void UpdateInventoryUI(int _itemCount, int _currency, float _currentWeight, float _totalWeight)
        {
            uiView.UpdateInventoryUI(_itemCount, _currency, _currentWeight, _totalWeight);
        }
        private void UpdateShopUI(ItemType _itemType, int _itemCount)
        {
            uiView.UpdateShopUI(_itemType, _itemCount);
        }
        private void UpdateItemMenuUI(ItemModel _itemModel, GameObject _menubutton)
        {
            uiView.UpdateItemMenuUI(_itemModel, _menubutton);
        }
    }
}