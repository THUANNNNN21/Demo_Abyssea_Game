using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class ItemDropTest : MyMonoBehaviour
{
    public int dropCount;
    public List<ItemDropCount> itemDropCounts = new();
    public EnemyController enemyController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(DropTesting), 2f, 0.5f);
    }
    private void DropTesting()
    {
        dropCount += 1;
        List<ItemDropRate> dropList = ItemSpawner.Instance.Drop(this.enemyController.EnemyDamReceiver.GetDropList(), this.gameObject);
        ItemDropCount itemDropCount;
        foreach (ItemDropRate item in dropList)
        {
            itemDropCount = itemDropCounts.Find(x => x.itemName == item.itemInInventory.itemSO.name);
            if (itemDropCount == null)
            {
                itemDropCount = new ItemDropCount { itemName = item.itemInInventory.itemSO.name };
                itemDropCounts.Add(itemDropCount);
            }
            itemDropCount.count += 1;
            itemDropCount.rate = Mathf.Round(((float)itemDropCount.count / (float)dropCount) * 100f) / 100f;
        }
    }
}
[Serializable]
public class ItemDropCount
{
    public string itemName;
    public int count;
    public float rate;
}