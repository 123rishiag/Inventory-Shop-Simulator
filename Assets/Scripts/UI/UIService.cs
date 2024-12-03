using ServiceLocator.Event;

namespace ServiceLocator.UI
{
    public class UIService
    {
        private UIController uiController;
        public UIService(EventService _eventService, UIView _uiCanvas)
        {
            uiController = new UIController(_eventService, _uiCanvas);
        }
    }
}