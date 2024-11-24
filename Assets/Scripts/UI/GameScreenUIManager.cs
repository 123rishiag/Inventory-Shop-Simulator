using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class GameScreenUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private Transform shopPanel;

    [Header("Prefabs")]
    [SerializeField] private GameObject itemPrefab;

    [Header("Text Fields")]
    [SerializeField] private TMP_Text currencyText;

    [Header("Managers")]
    [SerializeField] private ItemManager itemManager;

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
