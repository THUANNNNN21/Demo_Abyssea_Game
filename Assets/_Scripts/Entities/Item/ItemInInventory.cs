using System;
using UnityEngine;

[Serializable]
public class ItemInInventory
{
    [SerializeField] private string id;
    public string ID { get { return id; } set { id = value; } }
    public ItemSO itemSO;
    public int upgradeLevel;
    public int maxStack;
    public int itemsCount;
    public static string RandomID()
    {
        return RandomStringGenorator.Generate(26);
    }
}
