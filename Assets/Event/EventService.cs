using ServiceLocator.Item;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Event
{
    public class EventService
    {
        public void Init() { }

        public EventController<Action<string>> OnPopupNotificationEvent { get; private set; }
        public EventController<Action<Button, bool>> OnSetButtonInteractionEvent { get; private set; }
        public EventController<Func<ItemScriptableObject, UISection, ItemController>> OnCreateItemEvent { get; private set; }
        public EventController<Func<UISection, GameObject>> OnCreateItemButtonViewEvent { get; private set; }
        public EventController<Func<UISection, string, GameObject>> OnCreateMenuButtonViewEvent { get; private set; }
        public EventController<Action<UISection, ItemType>> OnShowItemEvent { get; private set; }
        public EventController<Action<UISection, int>> OnDestroyItemEvent { get; private set; }
        public EventController<Action<ItemType, int>> OnShopUpdatedEvent { get; private set; }
        public EventController<Action<int, int, float, float>> OnInventoryUpdatedEvent { get; private set; }

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
        }
    }
}