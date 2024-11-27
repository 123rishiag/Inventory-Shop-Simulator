using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Item
{
    [CreateAssetMenu(fileName = "NewItemDatabase", menuName = "Items/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemScriptableObject> allItems;
    }
}