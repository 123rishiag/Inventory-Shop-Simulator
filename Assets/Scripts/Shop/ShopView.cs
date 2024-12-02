using ServiceLocator.Item;
using ServiceLocator.UI;
using TMPro;
using UnityEngine;

namespace ServiceLocator.Shop
{
    public class ShopView
    {
        private ShopController shopController;
        public ShopView(ShopController _shopController)
        {
            shopController = _shopController;
        }

        public void ShowItems()
        {
            // Filter items and update visibility
            foreach (var itemController in shopController.GetItems())
            {
                if (shopController.GetModel().SelectedItemType == ItemType.All ||
                    itemController.GetModel().Type == shopController.GetModel().SelectedItemType)
                {
                    itemController.GetView().ShowView();
                    shopController.GetFilteredItems().Add(itemController);
                }
                else
                {
                    itemController.GetView().HideView();
                }
            }
        }

        public void UpdateUI(UIService _uiService)
        {
            // Update UI texts based on filtered items
            _uiService.UpdateShopEmptyText(shopController.GetFilteredItems().Count == 0);
            _uiService.UpdateShopItemTypeText(shopController.GetModel().SelectedItemType.ToString());
            _uiService.UpdateShopItemCount(shopController.GetFilteredItems().Count);

            _uiService.HideItemPanel();
        }
    }
}