using TMPro;
using UnityEngine;

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
