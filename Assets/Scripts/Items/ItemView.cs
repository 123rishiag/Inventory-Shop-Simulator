using ServiceLocator.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocator.Item
{
    public class ItemView : MonoBehaviour
    {
        private Image iconImage;

        [Header("UI Components")]
        [SerializeField] private TMP_Text quantityText;

        private ItemController itemController;

        // Sets the controller and initializes the view
        public void SetController(ItemController _itemController)
        {
            itemController = _itemController;

            iconImage = GetComponent<Image>();
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

        // Setters
        public void UpdateQuantity()
        {
            quantityText.text = $"{itemController.GetModel().Quantity}x";
        }
    }
}