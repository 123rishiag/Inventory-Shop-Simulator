using ServiceLocator.Item;
using ServiceLocator.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

        public GameObject CreateButton(GameObject _menuButtonPrefab, Transform _menuButtonPanel, string _menuButtonText)
        {
            // Checking if prefab and panel are valid
            if (_menuButtonPrefab == null || _menuButtonPanel == null)
            {
                Debug.LogError("Menu Button prefab or panel is null!");
                return null;
            }

            // Instantiating the button
            GameObject newButton = Object.Instantiate(_menuButtonPrefab, _menuButtonPanel);

            // Fetching TMP_Text component in the button and setting its text
            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = _menuButtonText;
            }
            else
            {
                Debug.LogWarning("Text component not found in button prefab.");
            }

            return newButton;
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

        public void SetButtonInteractivity(UIService _uiService, bool _isInteractable)
        {
            // Enabling or Disabling Buttons
            if (inventoryButtons != null)
            {
                foreach (var button in inventoryButtons)
                {
                    _uiService.SetButtonInteractivity(button, _isInteractable);
                }
            }
        }

        public void UpdateUI(UIService _uiService, InventoryScriptableObject _scriptableObject)
        {
            // Update UI texts based on items
            _uiService.UpdateInventoryEmptyText(inventoryController.GetItems().Count == 0);
            _uiService.UpdateInventoryCurrency(inventoryController.GetModel().Currency);
            _uiService.UpdateInventoryWeight(inventoryController.GetModel().CurrentWeight,
                inventoryController.GetModel().MaxWeight);

            // Enabling or Disabling Buttons
            if (inventoryController.CheckGatherResources(out _))
            {
                SetButtonInteractivity(_uiService, true);
            }
        }
    }
}