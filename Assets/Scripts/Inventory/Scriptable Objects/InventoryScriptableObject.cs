using ServiceLocator.Item;
using UnityEngine;

namespace ServiceLocator.Inventory
{
    [CreateAssetMenu(fileName = "InventoryScriptableObject", menuName = "ScriptableObjects/InventoryScriptableObject")]
    public class InventoryScriptableObject : ScriptableObject
    {
        public ItemDatabaseScriptableObject itemDatabase;

        public GameObject menuButtonPrefab;
        public GameObject itemPrefab;

        public float maxWeight; // Maximum allowable weight
    }
}