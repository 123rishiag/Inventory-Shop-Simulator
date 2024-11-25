using System.Collections.Generic;
using UnityEngine;

public class InventoryController
{
    private InventoryModel inventoryModel;
    private InventoryView inventoryView;
    private List<ItemController> itemControllers;

    public InventoryController(Transform _inventoryGrid)
    {
        // Instantiating Model
        inventoryModel = new InventoryModel();

        // Instantiating View
        inventoryView = new InventoryView(_inventoryGrid);

        // Controller List
        itemControllers = new List<ItemController>();
    }

    public void AddItem(ItemScriptableObject _itemData, GameObject _itemPrefab)
    {
        // Create ItemController
        var itemController = new ItemController(_itemData, inventoryView.GetInventoryGrid(), _itemPrefab);
        itemControllers.Add(itemController);

        // Add to Model
        inventoryModel.AddItem(itemController.GetModel());
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
        }
    }
}
