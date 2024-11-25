using TMPro;
using UnityEngine;

public class InventoryView
{
    private Transform inventoryGrid;
    private TMP_Text inventoryEmptyText;

    public InventoryView(Transform _inventoryGrid, TMP_Text _inventoryEmptyText)
    {
        inventoryGrid = _inventoryGrid;
        inventoryEmptyText = _inventoryEmptyText;
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
}
