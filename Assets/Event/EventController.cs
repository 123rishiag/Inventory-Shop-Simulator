using System;

namespace ServiceLocator.Event
{
    public class EventController<TDelegate> where TDelegate : Delegate
    {
        private TDelegate baseEvent;

        // Adding a listener to the event
        public void AddListener(TDelegate listener)
        {
            baseEvent = (TDelegate)Delegate.Combine(baseEvent, listener);
        }

        // Removing a listener from the event
        public void RemoveListener(TDelegate listener)
        {
            baseEvent = (TDelegate)Delegate.Remove(baseEvent, listener);
        }

        // Invoking the event
        public void Invoke(params object[] args)
        {
            if (baseEvent == null) return;

            foreach (var handler in baseEvent.GetInvocationList())
            {
                handler?.DynamicInvoke(args);
            }
        }
    }
}