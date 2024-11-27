using ServiceLocator.Item;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Inventory
{
    [CreateAssetMenu(fileName = "InventoryScriptableObject", menuName = "ScriptableObjects/InventoryScriptableObject")]
    public class InventoryScriptableObject : ScriptableObject
    {
        public List<ItemScriptableObject> allItems;

        public GameObject menuButtonPrefab;
        public GameObject itemPrefab;

        public float maxWeight; // Maximum allowable weight
    }
}