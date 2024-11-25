using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class GameScreenUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private Transform shopPanel;

    [Header("Managers")]
    [SerializeField] private ItemManager itemManager;

    private void Start()
    {
        if (!ValidateItemManager()) 
            return;

        itemManager.PopulateShop(shopPanel);
    }

    private bool ValidateItemManager()
    {
        if (itemManager == null)
        {
            Debug.LogError("ItemManager is not assigned in the inspector!");
            return false;
        }
        return true;
    }
}
