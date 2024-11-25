using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController
{
    private InventoryModel inventoryModel;
    private InventoryView inventoryView;
    private List<ItemController> itemControllers;

    public InventoryController(Transform _inventoryGrid, TMP_Text _inventoryEmptyText)
    {
        // Instantiating Model
        inventoryModel = new InventoryModel();

        // Instantiating View
        inventoryView = new InventoryView(_inventoryGrid, _inventoryEmptyText);

        // Controller List
        itemControllers = new List<ItemController>();

        // Initially check if the inventory is empty
        UpdateInventoryEmptyText();
    }

    public void AddItem(ItemScriptableObject _itemData, GameObject _itemPrefab)
    {
        // Create ItemController
        var itemController = new ItemController(_itemData, inventoryView.GetInventoryGrid(), _itemPrefab);
        itemControllers.Add(itemController);

        // Add to Model
        inventoryModel.AddItem(itemController.GetModel());

        // Update empty text
        UpdateInventoryEmptyText();
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

            // Update empty text
            UpdateInventoryEmptyText();
        }
    }

    private void UpdateInventoryEmptyText()
    {
        inventoryView.UpdateInventoryEmptyText(itemControllers.Count == 0);
    }
}
