using ServiceLocator.Item;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Event
{
    public class EventService
    {
        private static EventService instance;
        public void Init() { }

        public EventController<Action<string>> OnPopupNotificationEvent { get; private set; }
        public EventController<Action<Button, bool>> OnSetButtonInteractionEvent { get; private set; }
        public EventController<Func<ItemScriptableObject, UISection, ItemController>> OnCreateItemEvent { get; private set; }
        public EventController<Func<UISection, GameObject>> OnCreateItemButtonViewEvent { get; private set; }
        public EventController<Func<UISection, string, GameObject>> OnCreateMenuButtonViewEvent { get; private set; }

        public EventService()
        {
            OnPopupNotificationEvent = new EventController<Action<string>>();
            OnSetButtonInteractionEvent = new EventController<Action<Button, bool>>();
            OnCreateItemEvent = new EventController<Func<ItemScriptableObject, UISection, ItemController>>();
            OnCreateItemButtonViewEvent = new EventController<Func<UISection, GameObject>>();
            OnCreateMenuButtonViewEvent = new EventController<Func<UISection, string, GameObject>>();
        }
    }
}