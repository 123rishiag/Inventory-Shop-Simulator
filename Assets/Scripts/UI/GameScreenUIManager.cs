using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class GameScreenUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private Transform shopPanel;

    [Header("Text Fields")]
    [SerializeField] private TMP_Text currencyText;

    [Header("Managers")]
    [SerializeField] private ItemManager itemManager;

    private void Start()
    {
        if (!ValidateItemManager()) 
            return;

        UpdateCurrency(0);
        itemManager.PopulateShop(shopPanel);
    }

    public void UpdateCurrency(int currency)
    {
        currencyText.text = $"Currency: {currency}";
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
