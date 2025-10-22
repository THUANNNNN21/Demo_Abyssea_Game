using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : SpawnObject
{
    public static ItemSpawner Instance { get; private set; }
    [SerializeField] private List<GameObject> listPrefabs = new();
    public List<GameObject> ListPrefabs { get => listPrefabs; }
    [SerializeField] private float gameDropRate = 5f;
    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        base.Awake();
    }
    protected override void LoadComponents()
    {
        this.LoadSpawner();
        this.LoadPoolObject();
        this.LoadListPrefabs();
    }
    private void LoadSpawner()
    {
        if (this.Spawnner != null) return;
        else
        {
            this.Spawnner = this.transform.Find("Holder").gameObject;
        }
    }
    private void LoadPoolObject()
    {
        if (this.poolObject != null) return;
        else
        {
            this.poolObject = GameObject.Find("PoolManager").GetComponent<PoolObject>();
        }
    }
    private void LoadListPrefabs()
    {
        if (this.listPrefabs.Count > 0) return;
        else
        {
            this.listPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("_Prefab/Item"));
        }
    }
    //============================= Drop Item By Dead Object ============================//
    public List<ItemDropRate> Drop(List<ItemDropRate> dropRates, GameObject deadObject)
    {
        this.spawnPoint = deadObject.transform;
        List<ItemDropRate> itemsToDrop = this.GetItemsByDropRate(dropRates);
        foreach (var item in itemsToDrop)
        {
            this.prefabToSpawn = this.GetPrefabByDropRate(item);
            GameObject droppedItem = this.SpawnAndReturn();
            if (droppedItem == null) continue;
        }
        return itemsToDrop;
    }
    private List<ItemDropRate> GetItemsByDropRate(List<ItemDropRate> items)
    {
        List<ItemDropRate> listDroppedItems = new();
        float rate, itemRate;
        int itemDropMore;
        foreach (ItemDropRate item in items)
        {
            rate = Random.Range(0f, 1f);
            itemRate = (item.dropRate) / 100f * GameDropRate();
            itemDropMore = Mathf.FloorToInt(itemRate);
            if (itemDropMore > 0)
            {
                itemRate -= itemDropMore;
                for (int i = 0; i < itemDropMore; i++)
                {
                    listDroppedItems.Add(item);
                }
            }
            Debug.Log($"Item: {item.itemInInventory.itemSO.name} - Rate: {rate} - ItemRate: {itemRate}");
            if (rate <= itemRate)
            {
                listDroppedItems.Add(item);
            }
        }
        return listDroppedItems;
    }
    public float GameDropRate()
    {
        float dropRateFromItems = 0f;
        return this.gameDropRate + dropRateFromItems;
    }
    public GameObject GetPrefabByDropRate(ItemDropRate item)
    {
        string targetName = item.itemInInventory.itemSO.name;
        foreach (var prefab in this.listPrefabs)
        {
            if (prefab.name == targetName)
            {
                this.prefabToSpawn = prefab;
                return prefab;
            }
        }
        return null;
    }
    //============================= Drop Item In Inventory ============================//
    public void DropItemInInventory(ItemInInventory itemInInventory, GameObject dropPoint)
    {
        this.spawnPoint = dropPoint.transform;
        this.prefabToSpawn = this.GetPrefabInInventory(itemInInventory);
        GameObject droppedItem = this.SpawnAndReturn();
        this.SetItemInInventory(droppedItem, itemInInventory);
    }
    private GameObject GetPrefabInInventory(ItemInInventory itemInInventory)
    {
        ItemID targetID = itemInInventory.itemSO.itemID;
        foreach (var prefab in this.listPrefabs)
        {
            if (prefab.GetComponent<ItemController>().ItemInInventory.itemSO.itemID == targetID)
            {
                this.prefabToSpawn = prefab;
                return prefab;
            }
        }
        return null;
    }
    private void SetItemInInventory(GameObject droppedItem, ItemInInventory source)
    {
        if (droppedItem.TryGetComponent<ItemController>(out var itemController))
        {
            var target = itemController.ItemInInventory;
            target.itemSO = source.itemSO;
            target.upgradeLevel = source.upgradeLevel;
            target.maxStack = source.maxStack;
            target.itemsCount = 1;
        }
    }
}