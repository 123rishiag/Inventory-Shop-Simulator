using ServiceLocator.Inventory;
using ServiceLocator.UI;
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

        public static ItemView CreateView(ItemController _itemController, Transform _parentGrid, GameObject _itemPrefab)
        {
            if (_itemPrefab == null || _parentGrid == null)
            {
                Debug.LogError("ItemView: Prefab or Parent Grid is null!");
                return null;
            }

            GameObject itemObject = Object.Instantiate(_itemPrefab, _parentGrid);
            ItemView itemView = itemObject.GetComponent<ItemView>();
            if (itemView == null)
            {
                Debug.LogError("ItemView: Prefab does not have an ItemView component!");
                return null;
            }

            itemView.SetController(_itemController);

            // Add OnClick listener
            Button button = itemObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => _itemController.ProcessItem());
            }
            else
            {
                Debug.LogError("ItemView: Prefab does not have a Button component!");
            }

            return itemView;
        }

        // Sets the controller and initializes the view
        private void SetController(ItemController _itemController)
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