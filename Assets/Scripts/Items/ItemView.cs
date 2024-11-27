using TMPro;
using Unity.VisualScripting;
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

        public static ItemView CreateView(Transform _parentPanel, ItemController _itemController, GameObject _itemPrefab)
        {
            if (_itemPrefab == null || _parentPanel == null)
            {
                Debug.LogError("ItemView: Prefab or Parent Panel is null!");
                return null;
            }

            GameObject itemObject = Object.Instantiate(_itemPrefab, _parentPanel);
            ItemView itemView = itemObject.GetComponent<ItemView>();
            if (itemView == null)
            {
                Debug.LogError("ItemView: Prefab does not have an ItemView component!");
                return null;
            }

            itemView.SetController(_itemController);
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

        public void UpdateQuantity(int _newQuantity)
        {
            quantityText.text = $"{_newQuantity}x";
        }
    }
}