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

        public GameObject CreateButton(GameObject _menuButtonPrefab, Transform _menuButtonPanel, ItemType _itemType)
        {
            // Checking if prefab and panel are valid
            if (_menuButtonPrefab == null || _menuButtonPanel == null)
            {
                Debug.LogError("Menu Button prefab or panel is null!");
                return null;
            }

            // Instantiating the button
            GameObject newButton = Object.Instantiate(_menuButtonPrefab, _menuButtonPanel);

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
        }
    }
}