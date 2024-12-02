using ServiceLocator.Item;
using System;
using UnityEngine;

namespace ServiceLocator.Event
{
    public class EventService
    {
        private static EventService instance;
        public void Init() { }
        public EventController<Action<string>> OnPopupNotificationEvent { get; private set; }

        public EventController<Func<UISection, string, GameObject>> OnCreateMenuButtonEvent { get; private set; }

        public EventService()
        {
            OnPopupNotificationEvent = new EventController<Action<string>>();
            OnCreateMenuButtonEvent = new EventController<Func<UISection, string, GameObject>>();
        }
    }
}