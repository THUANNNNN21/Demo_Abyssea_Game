using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#region Header
public class InventoryController : MyMonoBehaviour
{
    #region Properties
    [SerializeField] private ItemLooter itemLooter;
    public ItemLooter ItemLooter { get => itemLooter; }
    [SerializeField] private ItemUpgrade itemUpgrade;
    public ItemUpgrade ItemUpgrade { get => itemUpgrade; }
    [SerializeField] private Inventory inventory;
    public Inventory Inventory { get => inventory; }
    [SerializeField] private InventorySort inventorySort;
    public InventorySort InventorySort { get => inventorySort; }
    [SerializeField] private InventoryDrop inventoryDrop;
    public InventoryDrop InventoryDrop { get => inventoryDrop; }
    #endregion

    //     #region Unity Methods
    //     void Start()
    //     {
    //         SortInventory();
    //     }
    //     #endregion

    #region Load Methods
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventory();
        this.LoadItemLooter();
        this.LoadItemUpgrade();
        this.LoadInventorySort();
        this.LoadInventoryDrop();
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
    private void LoadInventory()
    {
        if (this.inventory != null) return;
        this.inventory = GetComponentInChildren<Inventory>();
        Debug.LogWarning(this.gameObject.name + ": Load Inventory");
    }
    private void LoadInventorySort()
    {
        if (this.inventorySort != null) return;
        this.inventorySort = GetComponentInChildren<InventorySort>();
        Debug.LogWarning(this.gameObject.name + ": Load InventorySort");
    }
    private void LoadInventoryDrop()
    {
        if (this.inventoryDrop != null) return;
        this.inventoryDrop = GetComponentInChildren<InventoryDrop>();
        Debug.LogWarning(this.gameObject.name + ": Load InventoryDrop");
    }
    #endregion

    //     #region Inventory Methods
    //     public bool AddItem(ItemInInventory itemInInventory, int itemLevel, int addCount)
    //     {

    //         ItemSO itemSO = this.GetItemByID(itemInInventory.itemSO.itemName);

    //         if (itemSO == null)
    //         {
    //             return false;
    //         }

    //         ItemInInventory itemExist;
    //         ItemName itemID = itemInInventory.itemSO.itemName;
    //         int addRemain = addCount;
    //         int itemMaxStack;
    //         int canAdd;
    //         for (int i = 0; i < this.maxSlot; i++)
    //         {
    //             if (itemLevel > itemSO.levelUpRecipes.Count)
    //             {
    //                 return false;
    //             }
    //             itemExist = this.GetItemNotFullStack(itemID, itemLevel);
    //             if (itemExist != null)
    //             {
    //                 // newCount = itemExist.itemsCount + addRemain;
    //                 itemMaxStack = this.GetMaxStack(itemExist.itemSO);
    //                 canAdd = itemMaxStack - itemExist.itemsCount;
    //                 int addNow = Mathf.Min(canAdd, addRemain);
    //                 itemExist.itemsCount += addNow;
    //                 addRemain -= addNow;
    //             }
    //             else
    //             {
    //                 if (IsInventoryFull()) return false;
    //                 itemExist = this.CreateEmtyItem(itemSO, itemLevel);
    //                 this.listItems.Add(itemExist);
    //             }
    //             if (addRemain < 1) break;
    //         }

    //         SortInventory(); // ✅ Sort after adding items
    //         return true;
    //     }
    //     private ItemSO GetItemByID(ItemName itemID)
    //     {

    //         var itemsSO = Resources.LoadAll("_SO/ItemSO", typeof(ItemSO));

    //         // Debug.Log($"[InventoryController] Found {itemsSO.Length} items in Resources");

    //         foreach (ItemSO item in itemsSO)
    //         {
    //             // Debug.Log($"  - Checking: {item.name} (ItemID: {item.itemID})");

    //             if (item.itemName != itemID) continue;

    //             // Debug.Log($"[InventoryController] ✅ Match found: {item.name}");
    //             return item;
    //         }

    //         // Debug.LogWarning($"[InventoryController] ❌ No ItemSO found for ItemID: {itemID}");
    //         return null;
    //     }
    //     private ItemInInventory GetItemNotFullStack(ItemName itemID, int itemLevel)
    //     {
    //         foreach (ItemInInventory itemInInventory in this.ListItems)
    //         {
    //             if (itemID != itemInInventory.itemSO.itemName) continue;
    //             if (itemLevel != itemInInventory.upgradeLevel) continue;
    //             if (IsFullStack(itemInInventory)) continue;
    //             return itemInInventory;
    //         }
    //         return null;
    //     }
    //     private bool IsFullStack(ItemInInventory itemInInventory)
    //     {
    //         if (itemInInventory == null) return true;
    //         int maxStack = GetMaxStack(itemInInventory.itemSO);
    //         if (maxStack < 1) return false;
    //         return itemInInventory.itemsCount >= maxStack;
    //     }
    //     private int GetMaxStack(ItemSO item)
    //     {
    //         if (item == null) return 0;
    //         int maxStack;
    //         if (item.itemType == ItemType.Equipment)
    //         {
    //             maxStack = 1;
    //         }
    //         else
    //         {
    //             maxStack = item.defaultMaxStack;
    //         }
    //         return maxStack;
    //     }
    //     private bool IsInventoryFull()
    //     {
    //         if (ListItems.Count >= this.maxSlot) return true;
    //         else return false;
    //     }
    //     private ItemInInventory CreateEmtyItem(ItemSO itemSO, int itemLevel)
    //     {
    //         ItemInInventory itemInInventory = new()
    //         {
    //             ID = ItemInInventory.RandomID(),
    //             itemSO = itemSO,
    //             maxStack = GetMaxStack(itemSO),
    //             upgradeLevel = itemLevel
    //         };
    //         return itemInInventory;
    //     }

    //     public bool ItemCheck(ItemName itemID, int itemLevel, int amount)
    //     {
    //         int totalCount = this.GetTotalItemCount(itemID, itemLevel);
    //         if (totalCount < amount)
    //         {
    //             return false;
    //         }
    //         return true;
    //     }
    //     public int GetTotalItemCount(ItemName itemID, int itemLevel)
    //     {
    //         int totalCount = 0;
    //         foreach (ItemInInventory itemInInventory in this.ListItems)
    //         {
    //             if (itemInInventory.itemSO.itemName == itemID && itemInInventory.upgradeLevel == itemLevel)
    //             {
    //                 totalCount += itemInInventory.itemsCount;
    //             }
    //         }
    //         return totalCount;
    //     }
    //     public void DeductItem(ItemName itemID, int itemLevel, int deductAmount)
    //     {
    //         ItemInInventory itemInInventory;
    //         int deduct;
    //         for (int i = this.ListItems.Count - 1; i >= 0; i--)
    //         {
    //             if (deductAmount < 1) break;
    //             itemInInventory = this.ListItems[i];
    //             if (itemInInventory.upgradeLevel != itemLevel) continue;
    //             if (itemInInventory.itemSO.itemName != itemID) continue;
    //             if (itemInInventory.itemsCount < deductAmount)
    //             {
    //                 deduct = itemInInventory.itemsCount;
    //                 deductAmount -= deduct;
    //             }
    //             else
    //             {
    //                 deduct = deductAmount;
    //                 deductAmount = 0;
    //             }
    //             itemInInventory.itemsCount -= deduct;
    //         }
    //         this.ClearEmptyItem();
    //         SortInventory();
    //     }
    //     private void ClearEmptyItem()
    //     {
    //         for (int i = this.ListItems.Count - 1; i >= 0; i--)
    //         {
    //             if (this.ListItems[i].itemsCount < 1)
    //             {
    //                 this.ListItems.RemoveAt(i);
    //             }
    //         }
    //     }
    //     public void SetMaxSlot(int maxSlot)
    //     {
    //         this.maxSlot = maxSlot;
    //     }
    //     public void SetItemSlotType(string itemID, bool isHotKeySlot)
    //     {
    //         ItemInInventory item = listItems.FirstOrDefault(i => i.ID == itemID);
    //         if (item != null)
    //         {
    //             item.isOnHotKeySlot = isHotKeySlot;
    //         }
    //     }
    //     public ItemInInventory GetItemByID(string id)
    //     {
    //         foreach (ItemInInventory item in this.ListItems)
    //         {
    //             if (item.ID == id)
    //             {
    //                 return item;
    //             }
    //         }
    //         return null;
    //     }
    //     #endregion

    //     #region Sort Methods
    //     public List<ItemInInventory> GetSortedItems()
    //     {
    //         return SortItems();
    //     }
    //     private List<ItemInInventory> SortItems()
    //     {
    //         if (listItems == null || listItems.Count <= 1)
    //             return listItems ?? new List<ItemInInventory>();
    //         List<ItemInInventory> sorted = inventorySort switch
    //         {
    //             InventorySort.ByName => listItems
    //                                 .Where(item => item?.itemSO != null)
    //                                 .OrderBy(item => item.itemSO.itemName.ToString())
    //                                 .ToList(),
    //             InventorySort.ByCount => listItems
    //                                 .Where(item => item != null)
    //                                 .OrderBy(item => item.itemsCount)
    //                                 .ToList(),
    //             _ => listItems.Where(item => item != null).ToList(),
    //         };
    //         return sorted;
    //     }
    //     public void SortInventory()
    //     {
    //         listItems = SortItems();
    //     }
    //     #endregion
}
#endregion