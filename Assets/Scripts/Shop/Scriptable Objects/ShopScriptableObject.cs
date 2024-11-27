using ServiceLocator.Item;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Shop
{
    [CreateAssetMenu(fileName = "ShopScriptableObject", menuName = "ScriptableObjects/ShopScriptableObject")]
    public class ShopScriptableObject : ScriptableObject
    {
        public List<ItemScriptableObject> allItems;

        public GameObject menuButtonPrefab;
        public GameObject itemPrefab;
    }
}