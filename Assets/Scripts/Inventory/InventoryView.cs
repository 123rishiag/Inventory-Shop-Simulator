using ServiceLocator.Event;
using ServiceLocator.UI;
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

        public void ShowItems()
        {
            // Update visibility
            foreach (var itemController in inventoryController.GetItems())
            {
                itemController.GetView().ShowView();
            }
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

        public void UpdateUI(UIService _uiService, EventService _eventService, InventoryScriptableObject _scriptableObject)
        {
            // Update UI texts based on items
            _uiService.UpdateInventoryEmptyText(inventoryController.GetItems().Count == 0);
            _uiService.UpdateInventoryCurrency(inventoryController.GetModel().Currency);
            _uiService.UpdateInventoryWeight(inventoryController.GetModel().CurrentWeight,
                inventoryController.GetModel().MaxWeight);

            // Enabling or Disabling Buttons
            if (inventoryController.CheckGatherResources(out _))
            {
                SetButtonInteractivity(_eventService, true);
            }

            _uiService.HideItemPanel();
        }
    }
}