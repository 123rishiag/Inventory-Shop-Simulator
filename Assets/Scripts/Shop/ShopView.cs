using TMPro;
using UnityEngine;

public class ShopView
{
    private Transform shopGrid;
    private TMP_Text shopEmptyText;
    private TMP_Text shopItemCountText;

    public ShopView(Transform _shopGrid, TMP_Text _shopEmptyText, TMP_Text _shopItemCountText)
    {
        shopGrid = _shopGrid;
        shopEmptyText = _shopEmptyText;
        shopItemCountText = _shopItemCountText;
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

    public void UpdateShopItemCount(int _itemCount)
    {
        shopItemCountText.text = $"Items Count: {_itemCount}";
    }
}
