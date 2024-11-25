using UnityEngine;

public class InventoryView
{
    private Transform inventoryGrid;

    public InventoryView(Transform _inventoryGrid)
    {
        inventoryGrid = _inventoryGrid;
    }

    public Transform GetInventoryGrid()
    {
        return inventoryGrid;
    }
}
