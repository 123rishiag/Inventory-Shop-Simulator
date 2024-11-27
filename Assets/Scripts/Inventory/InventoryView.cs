using ServiceLocator.UI;
using TMPro;
using UnityEngine;

namespace ServiceLocator.Inventory
{
    public class InventoryView
    {
        private InventoryController inventoryController;
        public InventoryView(InventoryController _inventoryController)
        {
            inventoryController = _inventoryController;
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

        public void UpdateUI(UIService _uiService)
        {
            // Update UI texts based on items
            _uiService.UpdateInventoryEmptyText(inventoryController.GetItems().Count == 0);
            _uiService.UpdateInventoryCurrency(inventoryController.GetModel().Currency);
            _uiService.UpdateInventoryWeight(inventoryController.GetModel().CurrentWeight,
                inventoryController.GetModel().MaxWeight);
        }
    }
}