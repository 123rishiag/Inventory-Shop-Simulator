using ServiceLocator.Item;
using UnityEngine;

namespace ServiceLocator.Inventory
{
    [CreateAssetMenu(fileName = "InventoryScriptableObject", menuName = "ScriptableObjects/InventoryScriptableObject")]
    public class InventoryScriptableObject : ScriptableObject
    {
        public ItemDatabaseScriptableObject itemDatabase;
        public float maxWeight; // Maximum allowable weight
    }
}