using ServiceLocator.Event;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemService
    {
        private UIService uiService;
        private EventService eventService;

        private GameObject itemMenuButton;

        public ItemService(EventService _eventService)
        {
            eventService = _eventService;
            eventService.OnCreateItemEvent.AddListener(CreateItem);
        }
        ~ItemService()
        {
            eventService.OnCreateItemEvent.RemoveListener(CreateItem);
        }

        public void Init(UIService _uIService)
        {
            uiService = _uIService;

            AddButtonToPanel("");
        }

        private void AddButtonToPanel(string _menuButtonText)
        {
            // Instantiating the button
            itemMenuButton = eventService.OnCreateMenuButtonViewEvent.Invoke<GameObject>(UISection.Item, _menuButtonText);
        }
        private ItemController CreateItem(ItemScriptableObject _itemData, UISection _uiSection)
        {
            return new ItemController(uiService, eventService, itemMenuButton, _itemData, _uiSection);
        }
    }
}
