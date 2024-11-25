using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController
{
    private ShopModel shopModel;
    private ShopView shopView;
    private List<ItemController> itemControllers;

    public ShopController(Transform _shopGrid, TMP_Text _shopEmptyText, TMP_Text _shopItemCountText)
    {
        // Instantiating Model
        shopModel = new ShopModel();

        // Instantiating View
        shopView = new ShopView(_shopGrid, _shopEmptyText, _shopItemCountText);

        // Controller List
        itemControllers = new List<ItemController>();

        // Initial UI values
        UpdateShopTexts();
    }

    public void AddButtonToPanel(GameObject _shopMenuButtonPrefab, Transform _shopMenuButtonPanel, string _shopMenuButtonText, 
        ItemType _itemType)
    {
        // Instantiating the button
        GameObject newButton = shopView.CreateButton(_shopMenuButtonPrefab, _shopMenuButtonPanel, _shopMenuButtonText);

        // Setting up button logic (e.g., click events)
        Button button = newButton.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnButtonClicked(_shopMenuButtonText));
        }
        else
        {
            Debug.LogWarning("Button component not found in button prefab.");
        }
    }

    private void OnButtonClicked(string _shopMenuButtonText)
    {
        Debug.Log($"Button '{_shopMenuButtonText}' clicked!");
        // Handle button-specific actions here
    }

    public void AddItem(ItemScriptableObject _itemData, GameObject _itemPrefab)
    {
        // Create ItemController
        var itemController = new ItemController(_itemData, shopView.GetShopGrid(), _itemPrefab);
        itemControllers.Add(itemController);

        // Add to Model
        shopModel.AddItem(itemController.GetModel());

        // Update UI Metrics
        UpdateShopTexts();
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

            // Update UI Metrics
            UpdateShopTexts();
        }
    }

    private void UpdateShopTexts()
    {
        shopView.UpdateShopEmptyText(itemControllers.Count == 0);
        shopView.UpdateShopItemCount(shopModel.ItemsCount);
    }
}
