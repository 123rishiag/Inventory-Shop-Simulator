using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController
{
    private ShopModel shopModel;
    private ShopView shopView;
    private List<ItemController> itemControllers;
    private List<ItemController> filteredItemControllers;

    public ShopController(Transform _shopGrid, TMP_Text _shopEmptyText, TMP_Text _shopItemTypeText, TMP_Text _shopItemCountText)
    {
        // Instantiating Model
        shopModel = new ShopModel();

        // Instantiating View
        shopView = new ShopView(_shopGrid, _shopEmptyText, _shopItemTypeText, _shopItemCountText);

        // Controllers List
        itemControllers = new List<ItemController>();
        filteredItemControllers = new List<ItemController>();

        // Selected Item Type Filter
        shopModel.SelectedItemType = ItemType.All;

        // Show Filtered Items
        ShowFilteredItems();
    }

    public void AddButtonToPanel(GameObject _shopMenuButtonPrefab, Transform _shopMenuButtonPanel, ItemType _itemType)
    {
        // Instantiating the button
        GameObject newButton = shopView.CreateButton(_shopMenuButtonPrefab, _shopMenuButtonPanel, _itemType);

        // Setting up button logic (e.g., click events)
        Button button = newButton.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnButtonClicked(_itemType));
        }
        else
        {
            Debug.LogWarning("Button component not found in button prefab.");
        }
    }

    private void OnButtonClicked(ItemType _selectedItemType)
    {
        shopModel.SelectedItemType = _selectedItemType;
        ShowFilteredItems();
    }

    private void ShowFilteredItems()
    {
        // Clear filtered list
        filteredItemControllers.Clear();

        // Filter items and update visibility
        foreach (var itemController in itemControllers)
        {
            if (shopModel.SelectedItemType == ItemType.All || itemController.GetModel().Type == shopModel.SelectedItemType)
            {
                itemController.ShowView();
                filteredItemControllers.Add(itemController);
            }
            else
            {
                itemController.HideView();
            }
        }

        UpdateShopTexts();
    }

    public void AddItem(ItemScriptableObject _itemData, GameObject _itemPrefab)
    {
        // Create ItemController
        var itemController = new ItemController(_itemData, shopView.GetShopGrid(), _itemPrefab);
        itemControllers.Add(itemController);

        // Add to Model
        shopModel.AddItem(itemController.GetModel());

        // Show New Filtered Items
        ShowFilteredItems();
    }

    public void RemoveItem(ItemController _itemController)
    {
        if (itemControllers.Contains(_itemController))
        {
            // Remove from Model
            shopModel.RemoveItem(_itemController.GetModel());

            // Remove from Controller List
            itemControllers.Remove(_itemController);

            // Destroy the Item View
            _itemController.DestroyView();

            // Show New Filtered Items
            ShowFilteredItems();
        }
    }

    private void UpdateShopTexts()
    {
        // Update UI texts based on filtered items
        shopView.UpdateShopEmptyText(filteredItemControllers.Count == 0);
        shopView.UpdateShopItemTypeText(shopModel.SelectedItemType.ToString());
        shopView.UpdateShopItemCount(filteredItemControllers.Count);
    }
}
