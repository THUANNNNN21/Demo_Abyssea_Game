using System;
using UnityEngine;

[Serializable]
public class ItemDropRate
{
    [HideInInspector] public string name = "Item";
    public ItemInInventory itemInInventory;
    public float dropRate;

    // Automatically update name when itemInInventory changes in Inspector
    public void OnValidate()
    {
        if (itemInInventory != null && itemInInventory.itemSO != null)
        {
            name = itemInInventory.itemSO.itemName.ToString();
        }
        else
        {
            name = "Empty Item";
        }
    }
}
