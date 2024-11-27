using TMPro;
using UnityEngine;
using ServiceLocator.Item;

namespace ServiceLocator.Inventory
{
    public class InventoryView
    {
        private Transform inventoryGrid;
        private TMP_Text inventoryEmptyText;
        private TMP_Text inventoryCurrencyText;
        private TMP_Text inventoryWeightText;

        public InventoryView(Transform _inventoryGrid, TMP_Text _inventoryEmptyText,
            TMP_Text _inventoryCurrencyText, TMP_Text _inventoryWeightText)
        {
            inventoryGrid = _inventoryGrid;
            inventoryEmptyText = _inventoryEmptyText;
            inventoryCurrencyText = _inventoryCurrencyText;
            inventoryWeightText = _inventoryWeightText;
        }

        public GameObject CreateButton(GameObject _inventoryMenuButtonPrefab, Transform _inventoryMenuButtonPanel,
            string _inventoryMenuButtonText)
        {
            // Checking if prefab and panel are valid
            if (_inventoryMenuButtonPrefab == null || _inventoryMenuButtonPanel == null)
            {
                Debug.LogError("Menu Button prefab or panel is null!");
                return null;
            }

            // Instantiating the button
            GameObject newButton = Object.Instantiate(_inventoryMenuButtonPrefab, _inventoryMenuButtonPanel);

            // Fetching TMP_Text component in the button and setting its text
            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = _inventoryMenuButtonText;
            }
            else
            {
                Debug.LogWarning("Text component not found in button prefab.");
            }

            return newButton;
        }

        public Transform GetInventoryGrid()
        {
            return inventoryGrid;
        }

        public void UpdateInventoryEmptyText(bool _isEmpty)
        {
            if (inventoryEmptyText != null)
            {
                inventoryEmptyText.gameObject.SetActive(_isEmpty);
            }
        }

        public void UpdateInventoryCurrency(int _currency)
        {
            inventoryCurrencyText.text = $"Currency: {_currency}";
        }

        public void UpdateInventoryWeight(float _currentWeight, float _maxWeight)
        {
            inventoryWeightText.text = $"Weight: {_currentWeight}/{_maxWeight}";
        }
    }
}