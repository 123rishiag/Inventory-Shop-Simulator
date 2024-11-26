using ServiceLocator.Item;
using System.Collections.Generic;

namespace ServiceLocator.Shop
{
    public class ShopModel
    {
        private List<ItemModel> items;

        public ShopModel()
        {
            items = new List<ItemModel>();

            // Initial Values
            SelectedItemType = ItemType.All;
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

        public ItemType SelectedItemType { get; set; }
    }
}