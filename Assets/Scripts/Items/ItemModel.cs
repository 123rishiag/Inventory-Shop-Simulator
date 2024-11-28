using UnityEngine;

namespace ServiceLocator.Item
{
    public class ItemModel
    {
        public ItemModel(ItemScriptableObject _itemScriptableObject)
        {
            Id = _itemScriptableObject.Id;
            Icon = _itemScriptableObject.icon;
            Description = _itemScriptableObject.description;
            BuyingPrice = _itemScriptableObject.buyingPrice;
            SellingPrice = _itemScriptableObject.sellingPrice;
            Weight = _itemScriptableObject.weight;
            Type = _itemScriptableObject.type;
            Rarity = _itemScriptableObject.rarity;
            Quantity = _itemScriptableObject.quantity;
        }

        // Getters
        public UISection UISection { get; set; }
        public int Id { get; private set; }
        public Sprite Icon { get; private set; }
        public string Description { get; private set; }
        public int BuyingPrice { get; private set; }
        public int SellingPrice { get; private set; }
        public float Weight { get; private set; }
        public ItemType Type { get; private set; }
        public Rarity Rarity { get; private set; }
        public int Quantity { get; private set; }

        // Setters
        public void UpdateQuantity(int _delta)
        {
            Quantity = Mathf.Max(0, Quantity + _delta);
        }
    }
}