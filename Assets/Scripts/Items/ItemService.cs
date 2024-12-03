using ServiceLocator.Event;
using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemService
    {
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

        public void Init()
        {
            AddButtonToPanel("");
        }

        private void AddButtonToPanel(string _menuButtonText)
        {
            // Instantiating the button
            itemMenuButton = eventService.OnCreateMenuButtonViewEvent.Invoke<GameObject>(UISection.Item, _menuButtonText);
        }
        private ItemController CreateItem(ItemScriptableObject _itemData, UISection _uiSection)
        {
            return new ItemController(eventService, itemMenuButton, _itemData, _uiSection);
        }
    }
}
