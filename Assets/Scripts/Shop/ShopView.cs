using TMPro;
using UnityEngine;

public class ShopView
{
    private Transform shopGrid;
    private TMP_Text shopEmptyText;
    private TMP_Text shopItemTypeText;
    private TMP_Text shopItemCountText;

    public ShopView(Transform _shopGrid, TMP_Text _shopEmptyText, TMP_Text _shopItemTypeText, TMP_Text _shopItemCountText)
    {
        shopGrid = _shopGrid;
        shopEmptyText = _shopEmptyText;
        shopItemTypeText = _shopItemTypeText;
        shopItemCountText = _shopItemCountText;
    }

    public GameObject CreateButton(GameObject _shopMenuButtonPrefab, Transform _shopMenuButtonPanel, ItemType _itemType)
    {
        // Checking if prefab and panel are valid
        if (_shopMenuButtonPrefab == null || _shopMenuButtonPanel == null)
        {
            Debug.LogError("Menu Button prefab or panel is null!");
            return null;
        }

        // Instantiating the button
        GameObject newButton = Object.Instantiate(_shopMenuButtonPrefab, _shopMenuButtonPanel);

        // Fetching TMP_Text component in the button and setting its text
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.text = _itemType.ToString();
        }
        else
        {
            Debug.LogWarning("Text component not found in button prefab.");
        }

        return newButton;
    }

    public Transform GetShopGrid()
    {
        return shopGrid;
    }

    public void UpdateShopEmptyText(bool _isEmpty)
    {
        if (shopEmptyText != null)
        {
            shopEmptyText.gameObject.SetActive(_isEmpty);
        }
    }
    public void UpdateShopItemTypeText(string _text)
    {
        shopItemTypeText.text = $"Item Type: {_text}";
    }

    public void UpdateShopItemCount(int _itemCount)
    {
        shopItemCountText.text = $"Items Count: {_itemCount}";
    }
}
