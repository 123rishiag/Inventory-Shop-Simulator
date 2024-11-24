using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class GameScreenUIManager : MonoBehaviour
{
    [Header("Panels")]
    public Transform inventoryPanel;
    public Transform shopPanel;

    public GameObject itemPrefab;

    [Header("Currency")]
    public TMP_Text currencyText;

    [Header("Managers")]
    public ItemManager itemManager;

    private void Start()
    {
        if (!ValidateItemPrefab() || !ValidateItemManager()) return;

        UpdateCurrency(0);
        PopulateInventory();
        PopulateShop();

    }

    public void UpdateCurrency(int currency)
    {
        currencyText.text = $"Currency: {currency}";
    }

    public void AddItem(Transform parentPanel, Sprite icon, string labelText)
    {
        if (!ValidateItemPrefab()) return;

        GameObject item = Instantiate(itemPrefab, parentPanel);
        item.GetComponentInChildren<Image>().sprite = icon;
        item.GetComponentInChildren<TMP_Text>().text = labelText;
    }

    private void PopulateInventory()
    {
        foreach (var item in itemManager.GetInventoryItems())
        {
            AddItem(inventoryPanel, item.icon, $"{item.quantity}x");
        }
    }

    private void PopulateShop()
    {
        foreach (var item in itemManager.GetShopItems())
        {
            AddItem(shopPanel, item.icon, $"{item.quantity}x");
        }
    }

    private bool ValidateItemPrefab()
    {
        if (itemPrefab == null)
        {
            Debug.LogError("ItemPrefab is not assigned in the inspector!");
            return false;
        }
        return true;
    }

    private bool ValidateItemManager()
    {
        if (itemManager == null)
        {
            Debug.LogError("ItemManager is not assigned in the inspector!");
            return false;
        }
        return true;
    }
}
