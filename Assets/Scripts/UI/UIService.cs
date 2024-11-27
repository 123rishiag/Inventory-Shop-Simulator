using TMPro;
using UnityEngine;

namespace ServiceLocator.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private Transform inventoryGrid; // Assign Inventory Content Game Object inside Inventory Panel
        [SerializeField] private Transform inventoryMenuButtonPanel;
        [SerializeField] private Transform shopGrid; // Assign Shop Content Game Object inside Shop Panel
        [SerializeField] private Transform shopMenuButtonPanel;

        [Header("UI Elements")]
        [SerializeField] private TMP_Text inventoryEmptyText;
        [SerializeField] private TMP_Text inventoryCurrencyText;
        [SerializeField] private TMP_Text inventoryWeightText;
        [SerializeField] private TMP_Text shopEmptyText;
        [SerializeField] private TMP_Text shopItemTypeText;
        [SerializeField] private TMP_Text shopItemsCountText;

        public void Init() { }

        // Getters
        public Transform GetInventoryGrid() => inventoryGrid;
        public Transform GetInventoryButtonPanel() => inventoryMenuButtonPanel;
        public Transform GetShopGrid() => shopGrid;
        public Transform GetShopButtonPanel() => shopMenuButtonPanel;

        // Setters
        public void UpdateInventoryEmptyText(bool _isEmpty)
        {
            if (inventoryEmptyText != null)
            {
                inventoryEmptyText.gameObject.SetActive(_isEmpty);
            }
        }
        public void UpdateInventoryCurrency(int _currency)
        {
            inventoryCurrencyText.text = $"Currency: {_currency}";
        }
        public void UpdateInventoryWeight(float _currentWeight, float _maxWeight)
        {
            inventoryWeightText.text = $"Weight: {_currentWeight}/{_maxWeight}";
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
            shopItemsCountText.text = $"Items Count: {_itemCount}";
        }
    }
}