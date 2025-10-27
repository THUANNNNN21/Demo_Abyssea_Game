using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryController : MyMonoBehaviour
{
    [SerializeField] private ItemLooter itemLooter;
    public ItemLooter ItemLooter { get => itemLooter; }
    [SerializeField] private ItemUpgrade itemUpgrade;
    public ItemUpgrade ItemUpgrade { get => itemUpgrade; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemLooter();
        this.LoadItemUpgrade();
    }
    private void LoadItemLooter()
    {
        if (this.itemLooter != null) return;
        this.itemLooter = GetComponentInChildren<ItemLooter>();
        Debug.LogWarning(this.gameObject.name + ": Load ItemLooter");
    }
    private void LoadItemUpgrade()
    {
        if (this.itemUpgrade != null) return;
        this.itemUpgrade = GetComponentInChildren<ItemUpgrade>();
        Debug.LogWarning(this.gameObject.name + ": Load ItemUpgrade");
    }
    [SerializeField] private int maxSlot = 20;
    public int MaxSlot { get => maxSlot; }
    [SerializeField] private List<ItemInInventory> listItems;
    public List<ItemInInventory> ListItems { get => listItems; }
    [SerializeField] private InventorySort inventorySort = InventorySort.ByName;
    public InventorySort InventorySort
    {
        get => inventorySort;
        set
        {
            inventorySort = value;
            SortItems(); // Auto-sort when changed
        }
    }
    public bool AddItem(ItemInInventory itemInInventory, int itemLevel, int addCount)
    {
        // Debug.Log($"[InventoryController] Trying to add: {itemInInventory.itemSO.itemID} | Level: {itemLevel} | Count: {addCount}");

        ItemSO itemSO = this.GetItemByID(itemInInventory.itemSO.itemID);

        if (itemSO == null)
        {
            // Debug.LogError($"[InventoryController] ItemSO not found for ItemID: {itemInInventory.itemSO.itemID}");
            return false; // ❌ Đây là lỗi
        }

        // Debug.Log($"[InventoryController] Found ItemSO: {itemSO.name}");

        ItemInInventory itemExist;
        ItemID itemID = itemInInventory.itemSO.itemID;
        int addRemain = addCount;
        int itemMaxStack;
        int canAdd;
        for (int i = 0; i < this.maxSlot; i++)
        {
            if (itemLevel > itemSO.levelUpRecipes.Count)
            {
                return false;
            }
            itemExist = this.GetItemNotFullStack(itemID, itemLevel);
            if (itemExist != null)
            {
                // newCount = itemExist.itemsCount + addRemain;
                itemMaxStack = this.GetMaxStack(itemExist.itemSO);
                canAdd = itemMaxStack - itemExist.itemsCount;
                int addNow = Mathf.Min(canAdd, addRemain);
                itemExist.itemsCount += addNow;
                addRemain -= addNow;
                // if (newCount > itemMaxStack && itemMaxStack > 0)
                // {
                //     addMore = itemMaxStack - itemExist.itemsCount;
                //     newCount = itemExist.itemsCount + addMore;
                //     addRemain -= addMore;
                // }
                // else
                // {
                //     addRemain = 0;
                // }
                // itemExist.itemsCount = newCount;
            }
            else
            {
                if (IsInventoryFull()) return false;
                itemExist = this.CreateEmtyItem(itemSO, itemLevel);
                this.listItems.Add(itemExist);
            }
            if (addRemain < 1) break;
        }

        SortInventory(); // ✅ Sort after adding items
        return true;
    }
    private ItemSO GetItemByID(ItemID itemID)
    {
        // Debug.Log($"[InventoryController] Searching for ItemID: {itemID}");

        var itemsSO = Resources.LoadAll("_SO/ItemSO", typeof(ItemSO));

        // Debug.Log($"[InventoryController] Found {itemsSO.Length} items in Resources");

        foreach (ItemSO item in itemsSO)
        {
            // Debug.Log($"  - Checking: {item.name} (ItemID: {item.itemID})");

            if (item.itemID != itemID) continue;

            // Debug.Log($"[InventoryController] ✅ Match found: {item.name}");
            return item;
        }

        // Debug.LogWarning($"[InventoryController] ❌ No ItemSO found for ItemID: {itemID}");
        return null;
    }
    private ItemInInventory GetItemNotFullStack(ItemID itemID, int itemLevel)
    {
        foreach (ItemInInventory itemInInventory in this.ListItems)
        {
            if (itemID != itemInInventory.itemSO.itemID) continue;
            if (itemLevel != itemInInventory.upgradeLevel) continue;
            if (IsFullStack(itemInInventory)) continue;
            return itemInInventory;
        }
        return null;
    }
    private bool IsFullStack(ItemInInventory itemInInventory)
    {
        if (itemInInventory == null) return true;
        int maxStack = GetMaxStack(itemInInventory.itemSO);
        if (maxStack < 1) return false;
        return itemInInventory.itemsCount >= maxStack;
    }
    private int GetMaxStack(ItemSO item)
    {
        if (item == null) return 0;
        int maxStack;
        if (item.itemType == ItemType.Equipment)
        {
            maxStack = 1;
        }
        else
        {
            maxStack = item.defaultMaxStack;
        }
        return maxStack;
    }
    private bool IsInventoryFull()
    {
        if (ListItems.Count >= this.maxSlot) return true;
        else return false;
    }
    private ItemInInventory CreateEmtyItem(ItemSO itemSO, int itemLevel)
    {
        ItemInInventory itemInInventory = new()
        {
            ID = ItemInInventory.RandomID(),
            itemSO = itemSO,
            maxStack = GetMaxStack(itemSO),
            upgradeLevel = itemLevel
        };
        return itemInInventory;
    }
    // public bool AddItem(ItemID itemID, int addCount)
    // {
    //     ItemInInventory itemInInventory = this.GetItemByID(itemID);
    //     int newCount = itemInInventory.itemsCount + addCount;
    //     if (newCount > itemInInventory.maxStack)
    //     {
    //         itemInInventory.itemsCount = itemInInventory.maxStack;
    //         return false;
    //     }
    //     else
    //     {
    //         itemInInventory.itemsCount = newCount;
    //     }
    //     return true;
    // }
    // public ItemInInventory GetItemByID(ItemID itemID)
    // {
    //     ItemInInventory itemInInventory = this.items.Find(item => item.itemSO.itemID == itemID);
    //     if (itemInInventory != null)
    //     {
    //         return itemInInventory;
    //     }
    //     itemInInventory = this.AddEmptyItem(itemID);
    //     return itemInInventory;
    // }
    // protected ItemInInventory AddEmptyItem(ItemID itemID)
    // {
    //     var itemSO = Resources.LoadAll("_Prefab/Item", typeof(ItemSO));
    //     foreach (ItemSO item in itemSO)
    //     {
    //         if (item.itemID != itemID) continue;
    //         ItemInInventory newItem = new()
    //         {
    //             itemSO = item,
    //             maxStack = item.defaultMaxStack,
    //         };
    //         this.items.Add(newItem);
    //         return newItem;
    //     }
    //     return null;
    // }

    public bool ItemCheck(ItemID itemID, int itemLevel, int amount)
    {
        int totalCount = this.GetTotalItemCount(itemID, itemLevel);
        if (totalCount < amount)
        {
            return false;
        }
        return true;
    }
    public int GetTotalItemCount(ItemID itemID, int itemLevel)
    {
        int totalCount = 0;
        foreach (ItemInInventory itemInInventory in this.ListItems)
        {
            if (itemInInventory.itemSO.itemID == itemID && itemInInventory.upgradeLevel == itemLevel)
            {
                totalCount += itemInInventory.itemsCount;
            }
        }
        return totalCount;
    }
    public void DeductItem(ItemID itemID, int itemLevel, int deductAmount)
    {
        ItemInInventory itemInInventory;
        int deduct;
        for (int i = this.ListItems.Count - 1; i >= 0; i--)
        {
            if (deductAmount < 1) break;
            itemInInventory = this.ListItems[i];
            if (itemInInventory.upgradeLevel != itemLevel) continue;
            if (itemInInventory.itemSO.itemID != itemID) continue;
            if (itemInInventory.itemsCount < deductAmount)
            {
                deduct = itemInInventory.itemsCount;
                deductAmount -= deduct;
            }
            else
            {
                deduct = deductAmount;
                deductAmount = 0;
            }
            itemInInventory.itemsCount -= deduct;
        }
        this.ClearEmptyItem();
        SortInventory(); // ✅ Sort after removing items
    }
    private void ClearEmptyItem()
    {
        for (int i = this.ListItems.Count - 1; i >= 0; i--)
        {
            if (this.ListItems[i].itemsCount < 1)
            {
                this.ListItems.RemoveAt(i);
            }
        }
    }
    public void SetMaxSlot(int maxSlot)
    {
        this.maxSlot = maxSlot;
    }

    public List<ItemInInventory> GetSortedItems()
    {
        return SortItems();
    }

    private List<ItemInInventory> SortItems()
    {
        if (listItems == null || listItems.Count <= 1)
            return listItems ?? new List<ItemInInventory>();
        List<ItemInInventory> sorted = inventorySort switch
        {
            InventorySort.ByName => listItems
                                .Where(item => item?.itemSO != null)
                                .OrderBy(item => item.itemSO.itemID.ToString())
                                .ToList(),
            InventorySort.ByCount => listItems
                                .Where(item => item != null)
                                .OrderBy(item => item.itemsCount)
                                .ToList(),
            _ => listItems.Where(item => item != null).ToList(),
        };
        return sorted;
    }

    // Optional: Sort in-place
    public void SortInventory()
    {
        listItems = SortItems();
    }
    public void SetItemSlotType(string itemID, bool isHotKeySlot)
    {
        ItemInInventory item = listItems.FirstOrDefault(i => i.ID == itemID);
        if (item != null)
        {
            item.isOnHotKeySlot = isHotKeySlot;
            // Debug.Log($"[InventoryController] Set ItemID: {itemID} to isOnHotKeySlot: {isHotKeySlot}");
        }
    }
    void Start()
    {
        SortInventory(); // ✅ Sort on start
    }
    public ItemInInventory GetItemByID(string id)
    {
        foreach (ItemInInventory item in this.ListItems)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }
}