using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemDatabase", menuName = "Items/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemScriptableObject> allItems;
}
