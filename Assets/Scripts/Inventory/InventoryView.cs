using ServiceLocator.Event;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ServiceLocator.Inventory
{
    public class InventoryView
    {
        private InventoryController inventoryController;

        private List<Button> inventoryButtons;

        public InventoryView(InventoryController _inventoryController)
        {
            inventoryController = _inventoryController;
            inventoryButtons = new List<Button>();
        }

        public void AddButtonToView(Button _button)
        {
            inventoryButtons.Add(_button);
        }

        public void SetButtonInteractivity(EventService _eventService, bool _isInteractable)
        {
            // Enabling or Disabling Buttons
            if (inventoryButtons != null)
            {
                foreach (var button in inventoryButtons)
                {
                    _eventService.OnSetButtonInteractionEvent.Invoke(button, _isInteractable);
                }
            }
        }

        public void UpdateUI(EventService _eventService)
        {
            _eventService.OnShowItemEvent.Invoke(inventoryController.GetModel().UISection,
                inventoryController.GetModel().SelectedItemType);

            _eventService.OnInventoryUpdatedEvent.Invoke(inventoryController.GetItems().Count,
                inventoryController.GetModel().Currency, inventoryController.GetModel().CurrentWeight,
                inventoryController.GetModel().MaxWeight);

            // Enabling or Disabling Buttons
            if (inventoryController.CheckGatherResources(out _))
            {
                SetButtonInteractivity(_eventService, true);
            }
        }
    }
}