using ServiceLocator.Item;
using UnityEngine;

namespace ServiceLocator.Shop
{
    [CreateAssetMenu(fileName = "ShopScriptableObject", menuName = "ScriptableObjects/ShopScriptableObject")]
    public class ShopScriptableObject : ScriptableObject
    {
        public ItemDatabaseScriptableObject itemDatabase;
    }
}