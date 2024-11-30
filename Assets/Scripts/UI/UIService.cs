using ServiceLocator.Item;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private Transform inventoryGrid; // Assign Inventory Content Game Object inside Inventory Panel
        [SerializeField] private Transform inventoryMenuButtonPanel;
        [SerializeField] private Transform shopGrid; // Assign Shop Content Game Object inside Shop Panel
        [SerializeField] private Transform shopMenuButtonPanel;
        [SerializeField] private Transform itemMenuButtonPanel;

        [Header("UI Elements")]
        [SerializeField] private TMP_Text inventoryEmptyText;
        [SerializeField] private TMP_Text inventoryCurrencyText;
        [SerializeField] private TMP_Text inventoryWeightText;
        [SerializeField] private TMP_Text shopEmptyText;
        [SerializeField] private TMP_Text shopItemTypeText;
        [SerializeField] private TMP_Text shopItemsCountText;
        [SerializeField] private RawImage itemImage;
        [SerializeField] private TMP_Text itemDescription;
        [SerializeField] private TMP_Text itemPrice;
        [SerializeField] private TMP_Text itemWeight;
        [SerializeField] private TMP_Text itemType;
        [SerializeField] private TMP_Text itemRarity;
        [SerializeField] private TMP_Text itemQuantity;

        public void Init() { }

        // Getters
        public Transform GetInventoryGrid() => inventoryGrid;
        public Transform GetInventoryButtonPanel() => inventoryMenuButtonPanel;
        public Transform GetShopGrid() => shopGrid;
        public Transform GetShopButtonPanel() => shopMenuButtonPanel;
        public Transform GetItemButtonPanel() => itemMenuButtonPanel;

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
        public void SetButtonInteractivity(Button _button, bool _isInteractable)
        {
            if (_button != null)
            {
                _button.interactable = _isInteractable;
            }
        }
        private void SetItemImage(Sprite _sprite)
        {
            itemImage.texture = _sprite.texture;
        }
        private void SetItemDescription(string _text)
        {
            itemDescription.text = _text;
        }
        private void SetItemPrice(int _value)
        {
            itemPrice.text = $"Price: {_value}";
        }
        private void SetItemWeight(float _value)
        {
            itemWeight.text = $"Weight: {_value}";
        }
        private void SetItemType(string _text)
        {
            itemType.text = $"Type: {_text}";
        }
        private void SetItemRarity(string _text)
        {
            itemRarity.text = $"Rarity: {_text}";
        }
        private void SetItemQuantity(int _value)
        {
            itemQuantity.text = $"Quantity: {_value}x";
        }
        public void SetItemText(ItemModel _itemModel)
        {
            SetItemImage(_itemModel.Icon);
            SetItemDescription(_itemModel.Description);

            if (_itemModel.UISection == UISection.Inventory)
            {
                SetItemPrice(_itemModel.SellingPrice);
            }
            else if (_itemModel.UISection == UISection.Shop)
            {
                SetItemPrice(_itemModel.BuyingPrice);
            }

            SetItemWeight(_itemModel.Weight);
            SetItemType(_itemModel.Type.ToString());
            SetItemRarity(_itemModel.Rarity.ToString());
            SetItemQuantity(_itemModel.Quantity);
        }
    }
}