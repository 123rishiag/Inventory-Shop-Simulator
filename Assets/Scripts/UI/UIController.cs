using ServiceLocator.Event;

namespace ServiceLocator.UI
{
    public class UIController
    {
        private UIModel uiModel;
        private UIView uiView;

        public UIController(EventService _eventService, UIView _uiCanvas)
        {
            uiModel = new UIModel();
            uiView = _uiCanvas.GetComponent<UIView>();

            uiView.InitializeVariables(_eventService);
        }
    }
}