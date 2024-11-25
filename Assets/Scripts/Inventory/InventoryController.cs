using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController
{
    private InventoryModel inventoryModel;
    private InventoryView inventoryView;
    private List<ItemController> itemControllers;

    public InventoryController(Transform _inventoryGrid, TMP_Text _inventoryEmptyText,
        TMP_Text _inventoryCurrencyText, TMP_Text _inventoryWeightText, float _maxWeight)
    {
        // Instantiating Model
        inventoryModel = new InventoryModel(_maxWeight);

        // Instantiating View
        inventoryView = new InventoryView(_inventoryGrid, _inventoryEmptyText, _inventoryCurrencyText, _inventoryWeightText);

        // Controller List
        itemControllers = new List<ItemController>();

        // Test Values Initialization
        inventoryModel.Currency = 10;
        inventoryModel.CurrentWeight = 1;

        // Initial UI values
        UpdateInventoryTexts();
    }

    public void AddButtonToPanel(GameObject _inventoryMenuButtonPrefab, Transform _inventoryMenuButtonPanel, string _inventoryMenuButtonText)
    {
        // Checking if prefab and panel are valid
        if (_inventoryMenuButtonPrefab == null || _inventoryMenuButtonPanel == null)
        {
            Debug.LogError("Menu Button prefab or panel is null!");
            return;
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

        // Setting up button logic (e.g., click events)
        Button button = newButton.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnButtonClicked(_inventoryMenuButtonText));
        }
        else
        {
            Debug.LogWarning("Button component not found in button prefab.");
        }
    }

    private void OnButtonClicked(string _inventoryMenuButtonText)
    {
        Debug.Log($"Button '{_inventoryMenuButtonText}' clicked!");
        // Handle button-specific actions here
    }

    public void AddItem(ItemScriptableObject _itemData, GameObject _itemPrefab)
    {
        // Create ItemController
        var itemController = new ItemController(_itemData, inventoryView.GetInventoryGrid(), _itemPrefab);
        itemControllers.Add(itemController);

        // Add to Model
        inventoryModel.AddItem(itemController.GetModel());

        // Update UI Metrics
        UpdateInventoryTexts();
    }

    public void RemoveItem(ItemController _itemController)
    {
        if (itemControllers.Contains(_itemController))
        {
            // Remove from Model
            inventoryModel.RemoveItem(_itemController.GetModel());

            // Remove from Controller List
            itemControllers.Remove(_itemController);

            // Destroy the Item View
            _itemController.DestroyView();

            // Update UI Metrics
            UpdateInventoryTexts();
        }
    }

    private void UpdateInventoryTexts()
    {
        inventoryView.UpdateInventoryEmptyText(itemControllers.Count == 0);
        inventoryView.UpdateInventoryCurrency(inventoryModel.Currency);
        inventoryView.UpdateInventoryWeight(inventoryModel.CurrentWeight, inventoryModel.MaxWeight);
    }
}
