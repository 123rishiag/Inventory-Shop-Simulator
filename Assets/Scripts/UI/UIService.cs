using ServiceLocator.Item;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Object = UnityEngine.Object;

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

        [Header("Transaction Confirmation Elements")]
        [SerializeField] private GameObject transactionConfirmationPanel;
        [SerializeField] private TMP_Text transactionConfirmationText;
        [SerializeField] private Button transactionConfirmationYesButton;
        [SerializeField] private Button transactionConfirmationNoButton;

        [Header("Quantity Selection Elements")]
        [SerializeField] private GameObject quantitySelectionPanel;
        [SerializeField] private Button quantitySelectionDecrementButton;
        [SerializeField] private TMP_Text quantitySelectionText;
        [SerializeField] private Button quantitySelectionIncrementButton;
        [SerializeField] private Button quantitySelectionDoneButton;

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
        public void HideItemPanel()
        {
            if (itemPanel != null)
            {
                itemPanel.SetActive(false);
            }
        }
        public void OnTransactionConfirmation(ItemModel _itemModel, int _quantity, Action<bool> _callback)
        {
            // Enabling Panel
            transactionConfirmationPanel.SetActive(true);

            // Setting Text
            string activity = string.Empty;
            int price = 0;
            switch (_itemModel.UISection)
            {
                case UISection.Inventory:
                    activity = "sell";
                    price = _itemModel.SellingPrice;
                    break;
                case UISection.Shop:
                    activity = "buy";
                    price = _itemModel.BuyingPrice;
                    break;
                default:
                    break;
            }

            if (transactionConfirmationText != null)
            {
                transactionConfirmationText.text = $"Do you really wanna {activity} '{_quantity} {_itemModel.Name}' for " +
                    $"{_quantity * price}.";
            }

            // Fetching UI Elements
            Button yesButton = transactionConfirmationYesButton.GetComponent<Button>();
            Button noButton = transactionConfirmationNoButton.GetComponent<Button>();

            if (yesButton != null)
            {
                yesButton.onClick.RemoveAllListeners();
                yesButton.onClick.AddListener(() =>
                {
                    transactionConfirmationPanel.SetActive(false);
                    _callback.Invoke(true);
                }
                );
            }

            if (noButton != null)
            {
                noButton.onClick.RemoveAllListeners();
                noButton.onClick.AddListener(() =>
                {
                    transactionConfirmationPanel.SetActive(false);
                    _callback.Invoke(false);
                }
                );
            }
        }
        private int OnQuantityDecrementButton(Button _decrementButton, Button _incrementButton,
            int _minQuantity, int _quantity)
        {
            if (_quantity != _minQuantity)
            {
                _quantity -= 1;

                // Updating Panel Text
                if (quantitySelectionText != null)
                {
                    quantitySelectionText.text = _quantity.ToString();
                }

                // Disabling Decrement Button if quantity is minimum
                if (_quantity == _minQuantity && _decrementButton != null)
                {
                    _decrementButton.interactable = false;
                }

                // If quantity decreases, enabling Increment Button
                if (_incrementButton != null)
                {
                    _incrementButton.interactable = true;
                }
            }

            return _quantity;
        }
        private int OnQuantityIncrementButton(Button _decrementButton, Button _incrementButton,
            int _maxQuantity, int _quantity)
        {
            if (_quantity != _maxQuantity)
            {
                _quantity += 1;

                // Updating Panel Text
                if (quantitySelectionText != null)
                {
                    quantitySelectionText.text = _quantity.ToString();
                }

                // Disabling Increment Button if quantity is maximum
                if (_quantity == _maxQuantity && _incrementButton != null)
                {
                    _incrementButton.interactable = false;
                }

                // If quantity increases, enabling Decrement Button
                if (_decrementButton != null)
                {
                    _decrementButton.interactable = true;
                }
            }

            return _quantity;
        }
        public void OnSetItemQuanity(ItemModel _itemModel, Action<int> _callback)
        {
            // Setting Initial Values
            int minQuantity = 1;
            int currentQuantity = minQuantity;
            int maxQuantity = _itemModel.Quantity;
            if (quantitySelectionText != null)
            {
                quantitySelectionText.text = currentQuantity.ToString();
            }

            // Enabling Panel
            quantitySelectionPanel.SetActive(true);

            // Fetching buttons
            Button decrementbutton = quantitySelectionDecrementButton.GetComponent<Button>();
            Button incrementbutton = quantitySelectionIncrementButton.GetComponent<Button>();
            Button doneButton = quantitySelectionDoneButton.GetComponent<Button>();

            // Decrement Button Setup
            if (decrementbutton != null)
            {
                decrementbutton.onClick.RemoveAllListeners();
                decrementbutton.onClick.AddListener(() =>
                {
                    currentQuantity = OnQuantityDecrementButton(decrementbutton, incrementbutton,
                        minQuantity, currentQuantity);
                }
                );

                // Initial Button Status
                if (currentQuantity == minQuantity)
                {
                    decrementbutton.interactable = false;
                }
                else
                {
                    decrementbutton.interactable = true;
                }
            }

            // Increment Button Setup
            if (incrementbutton != null)
            {
                incrementbutton.onClick.RemoveAllListeners();
                incrementbutton.onClick.AddListener(() =>
                {
                    currentQuantity = OnQuantityIncrementButton(decrementbutton, incrementbutton,
                        maxQuantity, currentQuantity);
                }
                );

                // Initial Button Status
                if (currentQuantity == maxQuantity)
                {
                    incrementbutton.interactable = false;
                }
                else
                {
                    incrementbutton.interactable = true;
                }
            }

            // Done Button Setup
            if (doneButton != null)
            {
                doneButton.onClick.RemoveAllListeners();
                doneButton.onClick.AddListener(() =>
                {
                    quantitySelectionPanel.SetActive(false);
                    _callback.Invoke(currentQuantity);
                }
                );
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