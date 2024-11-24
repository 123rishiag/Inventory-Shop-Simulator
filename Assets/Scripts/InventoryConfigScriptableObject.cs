using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryConfig", menuName = "Configs/Inventory System")]
public class InventoryConfigScriptableObject : ScriptableObject
{
    public float maxWeight; // Maximum allowable weight
}