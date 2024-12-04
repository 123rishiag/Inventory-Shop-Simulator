using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Item
{
    public class ItemView : MonoBehaviour
    {
        private GameObject menuButton;

        [Header("UI Components")]
        [SerializeField] private Image iconImage;
        [SerializeField] private GameObject itemButton;
        [SerializeField] private TMP_Text quantityText;

        private ItemController itemController;

        // Sets the controller and initializes the view
        public void SetView(ItemController _itemController, GameObject _menubutton)
        {
            itemController = _itemController;
            menuButton = _menubutton;

            if (iconImage == null)
            {
                Debug.LogError("ItemView: No Image component found on this GameObject!");
                return;
            }

            iconImage.sprite = itemController.GetModel().Icon;
            quantityText.text = $"{itemController.GetModel().Quantity}x";
        }

        public void ShowView()
        {
            if (this != null)
            {
                gameObject.SetActive(true);
            }
        }

        public void HideView()
        {
            if (this != null)
            {
                gameObject.SetActive(false);
            }
        }

        // Destructor
        public void DestroyView()
        {
            if (this != null)
            {
                Object.Destroy(gameObject);
            }
        }

        // Getters
        public GameObject GetItemButton()
        {
            return itemButton;
        }
        public GameObject GetMenuButton()
        {
            return menuButton;
        }

        // Setters
        public void UpdateQuantity()
        {
            quantityText.text = $"{itemController.GetModel().Quantity}x";
        }
    }
}