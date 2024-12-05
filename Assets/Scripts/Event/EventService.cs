using ServiceLocator.Item;
using ServiceLocator.Sound;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Event
{
    public class EventService
    {
        // To Create and Return an Item Controller - Item Service
        public EventController<Func<ItemScriptableObject, UISection, ItemController>> OnCreateItemEvent { get; private set; }

        // To Show a particular type of items in a UI Section(Shop/Inventory) - Item Controller
        public EventController<Action<UISection, ItemType>> OnShowItemEvent { get; private set; }

        // To Destroy an item in a UI Section(Shop/Inventory) - Item Controller
        public EventController<Action<UISection, int>> OnDestroyItemEvent { get; private set; }

        // To Add an Item in Shop and return its success status when Selling from Inventory - Shop Controller
        public EventController<Func<ItemModel, int, bool>> OnShopAddItemEvent { get; private set; }

        // To Add an Item in Inventory and return its success status when Buying from Shop - Inventory Controller
        public EventController<Func<ItemModel, int, bool>> OnInventoryAddItemEvent { get; private set; }

        // To Trigger Sell Item - Inventory Controller
        public EventController<Action<ItemModel, int>> OnSellItemEvent { get; private set; }

        // To Trigger Buy Item - Shop Controller
        public EventController<Action<ItemModel, int>> OnBuyItemEvent { get; private set; }

        // To Peform their respective operations when Buy/Sell Button is clicked in Item Panel - UI Controller
        public EventController<Action<ItemModel, Action<int, bool>>> OnBuySellButtonClickEvent { get; private set; }

        // To Update Shop UI whenever Shop is updated - UI Controller
        public EventController<Action<ItemType, int>> OnShopUpdatedEvent { get; private set; }

        // To Update Inventory UI when Inventory is updated - UI Controller
        public EventController<Action<int, int, float, float>> OnInventoryUpdatedEvent { get; private set; }

        // To Show an item's UI in Item Panel - UI Controller
        public EventController<Action<ItemModel, GameObject>> OnItemClickEvent { get; private set; }

        // To Popup Notifications - UI Controller
        public EventController<Action<string>> OnPopupNotificationEvent { get; private set; }

        // To Set Button Interactibility On/Off - UI Controller
        public EventController<Action<Button, bool>> OnSetButtonInteractionEvent { get; private set; }

        // To Instantiate and Return an Item Button Prefab - UI Controller
        public EventController<Func<UISection, GameObject>> OnCreateItemButtonViewEvent { get; private set; }

        // To Instantiate and Return a Menu Button Prefab - UI Controller
        public EventController<Func<UISection, string, GameObject>> OnCreateMenuButtonViewEvent { get; private set; }

        // To Play a particular Sound Effect - Sound Service
        public EventController<Action<SoundType>> OnPlaySoundEffectEvent { get; private set; }

        public EventService()
        {
            OnCreateItemEvent = new EventController<Func<ItemScriptableObject, UISection, ItemController>>();
            OnShowItemEvent = new EventController<Action<UISection, ItemType>>();
            OnDestroyItemEvent = new EventController<Action<UISection, int>>();
            OnShopAddItemEvent = new EventController<Func<ItemModel, int, bool>>();
            OnInventoryAddItemEvent = new EventController<Func<ItemModel, int, bool>>();
            OnSellItemEvent = new EventController<Action<ItemModel, int>>();
            OnBuyItemEvent = new EventController<Action<ItemModel, int>>();
            OnBuySellButtonClickEvent = new EventController<Action<ItemModel, Action<int, bool>>>();
            OnShopUpdatedEvent = new EventController<Action<ItemType, int>>();
            OnInventoryUpdatedEvent = new EventController<Action<int, int, float, float>>();
            OnItemClickEvent = new EventController<Action<ItemModel, GameObject>>();
            OnPopupNotificationEvent = new EventController<Action<string>>();
            OnSetButtonInteractionEvent = new EventController<Action<Button, bool>>();
            OnCreateItemButtonViewEvent = new EventController<Func<UISection, GameObject>>();
            OnCreateMenuButtonViewEvent = new EventController<Func<UISection, string, GameObject>>();
            OnPlaySoundEffectEvent = new EventController<Action<SoundType>>();
        }
    }
}