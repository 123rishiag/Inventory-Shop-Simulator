using ServiceLocator.Item;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Inventory
{
    public class InventoryModel
    {
        private UISection uiSection;
        private List<ItemModel> items;

        public InventoryModel(InventoryScriptableObject _scriptableObject)
        {
            MaxWeight = Mathf.Round(_scriptableObject.maxWeight * 10f) / 10f;
            uiSection = UISection.Inventory;
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
        public int Currency { get; private set; }
        public float CurrentWeight { get; private set; }
        public float MaxWeight { get; private set; }

        // Setters
        public void UpdateUI(int _currencyDelta, float _weightDelta)
        {
            UpdateCurrency(_currencyDelta);
            UpdateCurrentWeight(_weightDelta);
        }
        private void UpdateCurrency(int _delta)
        {
            Currency = Mathf.Max(0, Currency + _delta);
        }
        // Setters
        private void UpdateCurrentWeight(float _delta)
        {
            CurrentWeight = Mathf.Max(0, Mathf.Round((CurrentWeight + _delta) * 10f) / 10f);
        }
    }
}