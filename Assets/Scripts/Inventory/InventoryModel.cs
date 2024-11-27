using System.Collections.Generic;
using ServiceLocator.Item;

namespace ServiceLocator.Inventory
{
    public class InventoryModel
    {
        private List<ItemModel> items;
        private float maxWeight;

        public InventoryModel(float _maxWeight)
        {
            maxWeight = _maxWeight;
            items = new List<ItemModel>();
        }

        public void AddItem(ItemModel _item)
        {
            items.Add(_item);
        }

        public void RemoveItem(ItemModel _item)
        {
            items.Remove(_item);
        }

        // Getters
        public List<ItemModel> Items => items;
        public int Currency { get; set; }
        public float CurrentWeight { get; set; }
        public float MaxWeight => maxWeight;
    }
}