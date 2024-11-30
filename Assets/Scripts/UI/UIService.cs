using ServiceLocator.Item;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace ServiceLocator.UI
{
    public class UIService : MonoBehaviour
    {
        // ItemButton
        private GameObject itemMenuButton;

        [Header("Panels")]
        [SerializeField] private Transform inventoryGrid; // Assign Inventory Content Game Object inside Inventory Panel
        [SerializeField] private Transform inventoryMenuButtonPanel;
        [SerializeField] private Transform shopGrid; // Assign Shop Content Game Object inside Shop Panel
        [SerializeField] private Transform shopMenuButtonPanel;
        [SerializeField] private Transform itemMenuButtonPanel;
        [SerializeField] private GameObject itemMenuButtonPrefab;
        [SerializeField] private GameObject itemPanel;

        [Header("UI Elements")]
        [SerializeField] private TMP_Text inventoryEmptyText;
        [SerializeField] private TMP_Text inventoryCurrencyText;
        [SerializeField] private TMP_Text inventoryWeightText;
        [SerializeField] private TMP_Text shopEmptyText;
        [SerializeField] private TMP_Text shopItemTypeText;
        [SerializeField] private TMP_Text shopItemsCountText;
        [SerializeField] private TMP_Text itemUISection;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private RawImage itemImage;
        [SerializeField] private TMP_Text itemDescription;
        [SerializeField] private TMP_Text itemPrice;
        [SerializeField] private TMP_Text itemWeight;
        [SerializeField] private TMP_Text itemType;
        [SerializeField] private TMP_Text itemRarity;
        [SerializeField] private TMP_Text itemQuantity;

        [Header("Notification Popup Elements")]
        [SerializeField] private GameObject notificationPopupPanel;
        [SerializeField] private TMP_Text notificationPopupText;

        public void Init()
        {
            AddButtonToItemPanel();
        }

        // Getters
        public Transform GetInventoryGrid() => inventoryGrid;
        public Transform GetInventoryButtonPanel() => inventoryMenuButtonPanel;
        public Transform GetShopGrid() => shopGrid;
        public Transform GetShopButtonPanel() => shopMenuButtonPanel;
        public Transform GetItemButtonPanel() => itemMenuButtonPanel;
        public GameObject GetItemMenuButton() => itemMenuButton;

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
        private void UpdateItemUISection(string _text)
        {
            itemUISection.text = $"UI Section: {_text}";
        }
        private void UpdateItemName(string _text)
        {
            itemName.text = _text;
        }
        private void UpdateItemImage(Sprite _sprite)
        {
            itemImage.texture = _sprite.texture;
        }
        private void UpdateItemDescription(string _text)
        {
            itemDescription.text = _text;
        }
        private void UpdateItemPrice(int _value)
        {
            itemPrice.text = $"Price: {_value}";
        }
        private void UpdateItemWeight(float _value)
        {
            itemWeight.text = $"Weight: {_value}";
        }
        private void UpdateItemType(string _text)
        {
            itemType.text = $"Type: {_text}";
        }
        private void UpdateItemRarity(string _text)
        {
            itemRarity.text = $"Rarity: {_text}";
        }
        private void UpdateItemQuantity(int _value)
        {
            itemQuantity.text = $"Quantity: {_value}x";
        }
        private void AddButtonToItemPanel()
        {
            // Instantiating the button
            // Checking if prefab and panel are valid
            if (itemMenuButtonPrefab == null || itemMenuButtonPanel == null)
            {
                Debug.LogError("Menu Button prefab or panel is null!");
                return;
            }

            // Instantiating the button
            itemMenuButton = Object.Instantiate(itemMenuButtonPrefab, itemMenuButtonPanel);

        }
        public void UpdateItemMenuUI(ItemModel _itemModel)
        {
            StartCoroutine(ShowItemPanel(0.2f));
            UpdateItemUISection(_itemModel.UISection.ToString());
            UpdateItemName(_itemModel.Name);
            UpdateItemImage(_itemModel.Icon);
            UpdateItemDescription(_itemModel.Description);

            if (_itemModel.UISection == UISection.Inventory)
            {
                UpdateItemPrice(_itemModel.SellingPrice);
            }
            else if (_itemModel.UISection == UISection.Shop)
            {
                UpdateItemPrice(_itemModel.BuyingPrice);
            }

            UpdateItemWeight(_itemModel.Weight);
            UpdateItemType(_itemModel.Type.ToString());
            UpdateItemRarity(_itemModel.Rarity.ToString());
            UpdateItemQuantity(_itemModel.Quantity);
            SetItemMenuButtonText(_itemModel);
        }
        private void SetItemMenuButtonText(ItemModel _itemModel)
        {
            // Fetching TMP_Text component in the button and setting its text
            TMP_Text buttonText = itemMenuButton.GetComponentInChildren<TMP_Text>();

            if (buttonText != null)
            {
                switch (_itemModel.UISection)
                {
                    case UISection.Inventory:
                        buttonText.text = "Sell";
                        break;
                    case UISection.Shop:
                        buttonText.text = "Buy";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.LogWarning("Text component not found in button prefab.");
            }
        }
        private IEnumerator ShowItemPanel(float _timeInSeconds)
        {
            if (itemPanel != null)
            {
                itemPanel.SetActive(false);
                yield return new WaitForSeconds(_timeInSeconds);
                itemPanel.SetActive(true);
            }
        }
        private IEnumerator PopupNotification(float _timeInSeconds)
        {
            notificationPopupPanel.gameObject.SetActive(true);
            yield return new WaitForSeconds(_timeInSeconds);
            notificationPopupPanel.gameObject.SetActive(false);
        }
        public void PopupNotification(string _text)
        {
            notificationPopupText.text = _text;
            StartCoroutine(PopupNotification(3f));
        }
        public void SetButtonInteractivity(Button _button, bool _isInteractable)
        {
            if (_button != null)
            {
                _button.interactable = _isInteractable;
            }
        }
    }
}