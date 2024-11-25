using System.Collections.Generic;

public class ShopModel
{
    private List<ItemModel> items;

    public ShopModel()
    {
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

    public ItemType SelectedItemType { get; set; }
}
