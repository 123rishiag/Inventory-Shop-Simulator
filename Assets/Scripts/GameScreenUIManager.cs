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

    [Header("Item Icons")]
    public List<Sprite> itemIcons;

    private void Start()
    {
        if (!ValidateItemPrefab()) return;

        UpdateCurrency(0);
        PopulateDummy();

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

    private void PopulateDummy()
    {
        for (int i = 0; i < 20; i++)
        {
            AddItem(inventoryPanel, GetRandomIcon(), Random.Range(1, 100).ToString());
            AddItem(shopPanel, GetRandomIcon(), Random.Range(10, 500).ToString());
        }
    }

    private Sprite GetRandomIcon()
    {
        if (itemIcons == null || itemIcons.Count == 0)
        {
            Debug.LogWarning("ItemIcons list is empty! Add some sprites in the inspector.");
            return null; // Placeholder or default sprite
        }

        int randomIndex = Random.Range(0, itemIcons.Count);
        return itemIcons[randomIndex];
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
}
