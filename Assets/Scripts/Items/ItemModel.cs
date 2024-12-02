using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemModel
    {
        public ItemModel(ItemScriptableObject _itemData)
        {
            ItemData = _itemData;
            Id = _itemData.Id;
            Name = _itemData.name;
            Icon = _itemData.icon;
            Description = _itemData.description;
            BuyingPrice = _itemData.buyingPrice;
            SellingPrice = _itemData.sellingPrice;
            Weight = _itemData.weight;
            Type = _itemData.type;
            Rarity = _itemData.rarity;
            Quantity = _itemData.quantity;
        }

        // Getters
        public UISection UISection { get; set; }
        public ItemScriptableObject ItemData { get; set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Sprite Icon { get; private set; }
        public string Description { get; private set; }
        public int BuyingPrice { get; private set; }
        public int SellingPrice { get; private set; }
        public float Weight { get; private set; }
        public ItemType Type { get; private set; }
        public ItemRarity Rarity { get; private set; }
        public int Quantity { get; private set; }

        // Setters
        public void UpdateQuantity(int _quantity, bool _isDelta)
        {
            if (_isDelta)
            {
                Quantity = Mathf.Max(0, Quantity + _quantity);
            }
            else
            {
                Quantity = Mathf.Max(0, _quantity);
            }
        }
    }
}