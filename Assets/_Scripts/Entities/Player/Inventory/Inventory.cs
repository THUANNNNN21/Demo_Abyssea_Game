using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#region Header
public class Inventory : MyMonoBehaviour
{
    #region Properties
    [SerializeField] private InventoryController inventoryController;
    public InventoryController InventoryController { get => inventoryController; }
    [SerializeField] private int maxSlot = 20;
    public int MaxSlot { get => maxSlot; }
    [SerializeField] private List<ItemInInventory> listItems;
    public List<ItemInInventory> ListItems { get => listItems; }
    #endregion

    #region Load Methods
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventoryController();
    }
    private void LoadInventoryController()
    {
        if (this.inventoryController != null) return;
        this.inventoryController = GetComponentInChildren<InventoryController>();
        Debug.LogWarning(this.gameObject.name + ": Load InventoryController");
    }
    #endregion

    #region Inventory Methods
    public bool AddItem(ItemInInventory itemInInventory, int itemLevel, int addCount)
    {
        ItemSO itemSO = this.GetItemByName(itemInInventory.itemSO.itemName);
        if (itemSO == null) return false;
        if (itemLevel > itemSO.levelUpRecipes.Count) return false;

        ItemName itemName = itemInInventory.itemSO.itemName;
        int addRemain = addCount;

        while (addRemain > 0)
        {
            ItemInInventory itemExist = this.GetItemNotFullStack(itemName, itemLevel);
            if (itemExist != null)
            {
                int itemMaxStack = this.GetMaxStack(itemExist.itemSO);
                int canAdd = itemMaxStack - itemExist.itemsCount;
                int addNow = Mathf.Min(canAdd, addRemain);
                itemExist.itemsCount += addNow;
                addRemain -= addNow;
            }
            else
            {
                if (IsInventoryFull()) return false;
                ItemInInventory newItem = this.CreateEmtyItem(itemSO, itemLevel);
                int itemMaxStack = this.GetMaxStack(itemSO);
                int addNow = Mathf.Min(itemMaxStack, addRemain);
                newItem.itemsCount = addNow;
                addRemain -= addNow;
                this.listItems.Add(newItem);
            }
        }
        return true;
    }
    private ItemSO GetItemByName(ItemName itemName)
    {
        var itemsSO = Resources.LoadAll("_SO/ItemSO", typeof(ItemSO));

        foreach (ItemSO item in itemsSO)
        {
            if (item.itemName != itemName) continue;
            return item;
        }
        return null;
    }
    private ItemInInventory GetItemNotFullStack(ItemName itemName, int itemLevel)
    {
        foreach (ItemInInventory itemInInventory in this.ListItems)
        {
            if (itemName != itemInInventory.itemSO.itemName) continue;
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

    public bool ItemCheck(ItemName itemName, int itemLevel, int amount)
    {
        int totalCount = this.GetTotalItemCount(itemName, itemLevel);
        if (totalCount < amount)
        {
            return false;
        }
        return true;
    }
    public int GetTotalItemCount(ItemName itemName, int itemLevel)
    {
        int totalCount = 0;
        foreach (ItemInInventory itemInInventory in this.ListItems)
        {
            if (itemInInventory.itemSO.itemName == itemName && itemInInventory.upgradeLevel == itemLevel)
            {
                totalCount += itemInInventory.itemsCount;
            }
        }
        return totalCount;
    }
    public void DeductItem(ItemName itemName, int itemLevel, int deductAmount)
    {
        if (deductAmount < 1) return;
        // Kiểm tra đủ số lượng trước khi trừ
        int totalCount = this.GetTotalItemCount(itemName, itemLevel);
        if (totalCount < deductAmount) return;

        for (int i = this.ListItems.Count - 1; i >= 0 && deductAmount > 0; i--)
        {
            var item = this.ListItems[i];
            if (item.upgradeLevel != itemLevel) continue;
            if (item.itemSO.itemName != itemName) continue;

            int deduct = Mathf.Min(item.itemsCount, deductAmount);
            item.itemsCount -= deduct;
            deductAmount -= deduct;

            if (deductAmount <= 0) break; // Đã trừ đủ, dừng lại
        }
        this.ClearEmptyItem();
        // SortInventory();
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
    public void SetItemSlotType(string itemID, bool isHotKeySlot)
    {
        ItemInInventory item = listItems.FirstOrDefault(i => i.ID == itemID);
        if (item != null)
        {
            item.isOnHotKeySlot = isHotKeySlot;
        }
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
    #endregion
    public void SetListItems(List<ItemInInventory> items)
    {
        this.listItems = items;
    }
    public void SetMaxSlot(int maxSlot)
    {
        this.maxSlot = maxSlot;
    }
}
#endregion