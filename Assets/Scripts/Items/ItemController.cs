using UnityEngine;

public class ItemController
{
    private ItemModel itemModel;
    private ItemView itemView;

    public ItemController(ItemScriptableObject _itemData, Transform _parentPanel, GameObject _itemPrefab)
    {
        // Creating the Model
        itemModel = new ItemModel(_itemData);

        // Creating the View
        itemView = ItemView.CreateView(_parentPanel, this, _itemPrefab);
    }

    // Getters
    public ItemModel GetModel()
    {
        return itemModel;
    }

    // Setters
    public void UpdateItemQuantity(int _delta)
    {
        itemModel.UpdateQuantity(_delta);
        itemView.UpdateQuantity(itemModel.Quantity);
    }
}
