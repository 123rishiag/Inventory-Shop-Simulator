using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemController
    {
        private ItemModel itemModel;
        private ItemView itemView;

        public ItemController(ItemScriptableObject _itemScriptableObject, Transform _parentGrid, GameObject _itemPrefab)
        {
            // Creating the Model
            itemModel = new ItemModel(_itemScriptableObject);

            // Creating the View
            itemView = ItemView.CreateView(_parentGrid, this, _itemPrefab);
        }

        // Getters
        public ItemModel GetModel() => itemModel;
        public ItemView GetView() => itemView;

        // Setters
        public void UpdateItemQuantity(int _delta)
        {
            itemModel.UpdateQuantity(_delta);
            itemView.UpdateQuantity();
        }
    }
}