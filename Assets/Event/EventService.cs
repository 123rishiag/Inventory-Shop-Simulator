using System;

namespace ServiceLocator.Event
{
    public class EventService
    {
        private static EventService instance;
        public void Init() { }

        public EventController<Action> OnNoParametersEvent { get; private set; }
        public EventController<Action<int>> OnOneParametersEvent { get; private set; }
        public EventController<Action<int, int>> OnTwoParametersEvent { get; private set; }

        public EventService()
        {
            OnNoParametersEvent = new EventController<Action>();
            OnOneParametersEvent = new EventController<Action<int>>();
            OnTwoParametersEvent = new EventController<Action<int, int>>();
        }
    }
}