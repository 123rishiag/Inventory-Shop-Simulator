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
            InitializeVariables();

            // Adding Event Listeners
            eventService.OnCreateItemEvent.AddListener(CreateItem);
        }
        ~ItemService()
        {
            // Removing Event Listeners
            eventService.OnCreateItemEvent.RemoveListener(CreateItem);
        }

        private void InitializeVariables()
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
