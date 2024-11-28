using ServiceLocator.Item;
using System.Collections.Generic;

namespace ServiceLocator.Shop
{
    public class ShopModel
    {
        private UISection uiSection;
        private List<ItemModel> items;

        public ShopModel()
        {
            uiSection = UISection.Shop;
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
        public UISection UISection => uiSection;
        public List<ItemModel> Items => items;
        public ItemType SelectedItemType { get; set; }
    }
}