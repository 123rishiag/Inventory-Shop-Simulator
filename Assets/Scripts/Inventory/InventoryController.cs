using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        inventoryModel.Currency = 0;
        inventoryModel.CurrentWeight = 1;

        // Initially UI values
        UpdateInventoryTexts();
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
