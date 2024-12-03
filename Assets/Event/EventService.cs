using ServiceLocator.Item;
using ServiceLocator.Sound;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Event
{
    public class EventService
    {
        public EventController<Action<string>> OnPopupNotificationEvent { get; private set; }
        public EventController<Action<Button, bool>> OnSetButtonInteractionEvent { get; private set; }
        public EventController<Func<ItemScriptableObject, UISection, ItemController>> OnCreateItemEvent { get; private set; }
        public EventController<Func<UISection, GameObject>> OnCreateItemButtonViewEvent { get; private set; }
        public EventController<Func<UISection, string, GameObject>> OnCreateMenuButtonViewEvent { get; private set; }
        public EventController<Action<UISection, ItemType>> OnShowItemEvent { get; private set; }
        public EventController<Action<UISection, int>> OnDestroyItemEvent { get; private set; }
        public EventController<Action<ItemType, int>> OnShopUpdatedEvent { get; private set; }
        public EventController<Action<int, int, float, float>> OnInventoryUpdatedEvent { get; private set; }
        public EventController<Func<ItemModel, int, bool>> OnShopAddItemEvent { get; private set; }
        public EventController<Func<ItemModel, int, bool>> OnInventoryAddItemEvent { get; private set; }
        public EventController<Action<ItemModel, int>> OnSellItemEvent { get; private set; }
        public EventController<Action<ItemModel, int>> OnBuyItemEvent {  get; private set; }
        public EventController<Action<ItemModel, GameObject>> OnItemClickEvent { get; private set; }
        public EventController<Action<ItemModel, Action<int, bool>>> OnBuySellButtonClickEvent { get; private set; }
        public EventController<Action<SoundType, bool>> OnPlaySoundEffectEvent { get; private set; }

        public EventService()
        {
            OnPopupNotificationEvent = new EventController<Action<string>>();
            OnSetButtonInteractionEvent = new EventController<Action<Button, bool>>();
            OnCreateItemEvent = new EventController<Func<ItemScriptableObject, UISection, ItemController>>();
            OnCreateItemButtonViewEvent = new EventController<Func<UISection, GameObject>>();
            OnCreateMenuButtonViewEvent = new EventController<Func<UISection, string, GameObject>>();
            OnShowItemEvent = new EventController<Action<UISection, ItemType>>();
            OnDestroyItemEvent = new EventController<Action<UISection, int>>();
            OnShopUpdatedEvent = new EventController<Action<ItemType, int>>();
            OnInventoryUpdatedEvent = new EventController<Action<int, int, float, float>>();
            OnShopAddItemEvent = new EventController<Func<ItemModel, int, bool>>();
            OnInventoryAddItemEvent = new EventController<Func<ItemModel, int, bool>>();
            OnSellItemEvent = new EventController<Action<ItemModel, int>>();
            OnBuyItemEvent = new EventController<Action<ItemModel, int>>();
            OnItemClickEvent = new EventController<Action<ItemModel, GameObject>>();
            OnBuySellButtonClickEvent = new EventController<Action<ItemModel, Action<int, bool>>>();
            OnPlaySoundEffectEvent = new EventController<Action<SoundType, bool>>();
        }
    }
}