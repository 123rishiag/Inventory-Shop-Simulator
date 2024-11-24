using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<ItemScriptableObject> shopItems;
    public List<ItemScriptableObject> inventoryItems = new List<ItemScriptableObject>();

    [Header("Inventory Config")]
    public InventoryConfigScriptableObject inventoryConfig;
}
