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

    private void Start()
    {
        UpdateCurrency(0);
        
        for (int i = 0; i < 20; i++)
        {
            AddInventoryItem(itemPrefab);
        }

        for (int i = 0; i < 20; i++)
        {
            AddShopItem(itemPrefab);
        }

    }

    public void UpdateCurrency(int currency)
    {
        currencyText.text = $"Gold: {currency}";
    }

    public void AddInventoryItem(GameObject item)
    {
        Instantiate(item, inventoryPanel);
    }

    public void AddShopItem(GameObject item)
    {
        Instantiate(item, shopPanel);
    }
}
