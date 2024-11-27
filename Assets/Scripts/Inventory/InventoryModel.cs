using ServiceLocator.Item;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Inventory
{
    public class InventoryModel
    {
        private List<ItemModel> items;

        public InventoryModel(InventoryScriptableObject _scriptableObject)
        {
            MaxWeight = _scriptableObject.maxWeight;
            items = new List<ItemModel>();

            // Initial Values
            Currency = 10;
            CurrentWeight = 1;
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
        public int Currency { get; private set; }
        public float CurrentWeight { get; private set; }
        public float MaxWeight { get; private set; }

        // Setters
        public void UpdateCurrency(int _delta)
        {
            Currency = Mathf.Max(0, Currency + _delta);
        }
        // Setters
        public void UpdateCurrentWeight(int _delta)
        {
            CurrentWeight = Mathf.Max(0, CurrentWeight + _delta);
        }
    }
}