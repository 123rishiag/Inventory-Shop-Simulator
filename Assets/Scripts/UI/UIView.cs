using ServiceLocator.Item;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Object = UnityEngine.Object;

namespace ServiceLocator.UI
{
    public class UIView : MonoBehaviour
    {
        // Variables
        #region Variables
        [Header("Prefabs")]
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GameObject menuButtonPrefab;

        [Header("Panels")]
        [SerializeField] private Transform inventoryGrid; // Assign Inventory Content Game Object inside Inventory Panel
        [SerializeField] private Transform inventoryMenuButtonPanel;
        [SerializeField] private Transform shopGrid; // Assign Shop Content Game Object inside Shop Panel
        [SerializeField] private Transform shopMenuButtonPanel;
        [SerializeField] private GameObject itemPanel; // Item Panel Object
        [SerializeField] private Transform itemMenuButtonPanel;

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
        #endregion

        // General Functions
        #region General Functions
        public GameObject CreateMenuButtonPrefab(UISection _uISection, string _menuButtonText)
        {
            Transform menuButtonPanel = GetMenuButtonPanel(_uISection);

            // Checking if prefab and panel are valid
            if (menuButtonPrefab == null || menuButtonPanel == null)
            {
                Debug.LogError("Menu Button prefab or panel is null!");
                return null;
            }

            // Instantiating the button
            GameObject newButton = Object.Instantiate(menuButtonPrefab, menuButtonPanel);

            // Fetching TMP_Text component in the button and setting its text
            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = _menuButtonText;
            }
            else
            {
                Debug.LogWarning("Text component not found in button prefab.");
            }

            return newButton;
        }
        public GameObject CreateItemButtonPrefab(UISection _uISection)
        {
            Transform itemPanel = GetItemGridPanel(_uISection);

            // Checking if prefab and panel are valid
            if (itemPrefab == null || itemPanel == null)
            {
                Debug.LogError("Item Button prefab or panel is null!");
                return null;
            }

            // Instantiating the button
            GameObject newObject = Object.Instantiate(itemPrefab, itemPanel);

            return newObject;
        }
        public (Button, Button, Button) CreateItemQuanityButtons(ItemModel _itemModel, int currentQuantity)
        {
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

            return (decrementbutton, incrementbutton, doneButton);
        }
        public int OnQuantityDecrementButton(Button _decrementButton, Button _incrementButton,
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
        public int OnQuantityIncrementButton(Button _decrementButton, Button _incrementButton,
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
        public (Button, Button) CreateTransactionConfirmationButtons(ItemModel _itemModel, int _quantity)
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
                transactionConfirmationText.text = $"Do you really wanna {activity} {_quantity} {_itemModel.Name} for " +
                    $"{_quantity * price}A.";
            }

            // Fetching UI Elements
            Button yesButton = transactionConfirmationYesButton.GetComponent<Button>();
            Button noButton = transactionConfirmationNoButton.GetComponent<Button>();

            return (yesButton, noButton);
        }
        public void PopupNotification(string _text)
        {
            notificationPopupText.text = _text;
            StartCoroutine(PopupNotificationCounter(3f));
        }
        private IEnumerator PopupNotificationCounter(float _timeInSeconds)
        {
            notificationPopupPanel.gameObject.SetActive(true);
            yield return new WaitForSeconds(_timeInSeconds);
            notificationPopupPanel.gameObject.SetActive(false);
        }
        private void HideItemPanel()
        {
            if (itemPanel != null)
            {
                itemPanel.SetActive(false);
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
        #endregion

        // Getters
        #region Getters
        public GameObject GetTransactionConfirmationPanel()
        {
            return transactionConfirmationPanel;
        }
        public GameObject GetQuantitySelectionPanel()
        {
            return quantitySelectionPanel;
        }
        public GameObject GetNotificationPopupPanel()
        {
            return notificationPopupPanel;
        }
        private Transform GetMenuButtonPanel(UISection _uiSection)
        {
            switch (_uiSection)
            {
                case UISection.Inventory:
                    return inventoryMenuButtonPanel;
                case UISection.Shop:
                    return shopMenuButtonPanel;
                case UISection.Item:
                    return itemMenuButtonPanel;
                default:
                    return null;
            }
        }
        private Transform GetItemGridPanel(UISection _uiSection)
        {
            switch (_uiSection)
            {
                case UISection.Inventory:
                    return inventoryGrid;
                case UISection.Shop:
                    return shopGrid;
                default:
                    return null;
            }
        }
        #endregion

        // Setters
        #region Setters

        // General Setters
        #region General Setters
        public void SetButtonInteractivity(Button _button, bool _isInteractable)
        {
            if (_button != null)
            {
                _button.interactable = _isInteractable;
            }
        }
        #endregion

        // Inventory Setters
        #region Inventory Setters
        public void UpdateInventoryUI(int _itemCount, int _currency, float _currentWeight, float _totalWeight)
        {
            // Update UI texts based on items
            UpdateInventoryEmptyText(_itemCount == 0);
            UpdateInventoryCurrency(_currency);
            UpdateInventoryWeight(_currentWeight, _totalWeight);

            HideItemPanel();
        }
        private void UpdateInventoryEmptyText(bool _isEmpty)
        {
            if (inventoryEmptyText != null)
            {
                inventoryEmptyText.gameObject.SetActive(_isEmpty);
            }
        }
        private void UpdateInventoryCurrency(int _currency)
        {
            inventoryCurrencyText.text = $"Currency: {_currency}A";
        }
        private void UpdateInventoryWeight(float _currentWeight, float _maxWeight)
        {
            inventoryWeightText.text = $"Weight: {_currentWeight}/{_maxWeight}";
        }
        #endregion

        // Shop Setters
        #region Shop Setters
        public void UpdateShopUI(ItemType _itemType, int _itemCount)
        {
            UpdateShopEmptyText(_itemCount == 0);
            UpdateShopItemTypeText(_itemType.ToString());
            UpdateShopItemCount(_itemCount);

            HideItemPanel();
        }
        private void UpdateShopEmptyText(bool _isEmpty)
        {
            if (shopEmptyText != null)
            {
                shopEmptyText.gameObject.SetActive(_isEmpty);
            }
        }
        private void UpdateShopItemTypeText(string _text)
        {
            shopItemTypeText.text = $"Item Type: {_text}";
        }
        private void UpdateShopItemCount(int _itemCount)
        {
            shopItemsCountText.text = $"Items Count: {_itemCount}";
        }
        #endregion

        // Item Setters
        #region Item Setters
        public void UpdateItemMenuUI(ItemModel _itemModel, GameObject _menubutton)
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
            UpdateItemMenuButtonText(_itemModel, _menubutton);
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
        private void UpdateItemMenuButtonText(ItemModel _itemModel, GameObject _menubutton)
        {
            // Fetching TMP_Text component in the button and setting its text
            TMP_Text buttonText = _menubutton.GetComponentInChildren<TMP_Text>();

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
        #endregion

        // Ending Setters Region
        #endregion
    }
}