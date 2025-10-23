using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIInventory : MyMonoBehaviour
{
    [SerializeField] private UIInventoryController uiInventoryController;
    public UIInventoryController UIInventoryController => uiInventoryController;
    [SerializeField] protected InventorySort inventorySort = InventorySort.ByName;

    private static UIInventory instance;
    public static UIInventory Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        this.SubscribeEvents();
    }
    protected override void LoadComponents()
    {
        this.LoadController();
    }
    private void LoadController()
    {
        if (this.uiInventoryController == null)
        {
            this.uiInventoryController = this.GetComponentInParent<UIInventoryController>();
        }
    }
    void Start()
    {
        this.Close();
        this.ShowInventory();
    }
    private void SubscribeEvents()
    {
        InventoryDrop.AfterDropItem += ShowInventory;
        ItemLooter.AfterPickupItem += ShowInventory;
        ItemUpgrade.AfterUpgradeItem += ShowInventory;
    }
    void OnDestroy()
    {
        InventoryDrop.AfterDropItem -= ShowInventory;
        ItemLooter.AfterPickupItem -= ShowInventory;
        ItemUpgrade.AfterUpgradeItem -= ShowInventory;
    }
    public virtual void Toggle()
    {
        this.uiInventoryController.gameObject.SetActive(!this.uiInventoryController.gameObject.activeSelf);
    }
    public void Close()
    {
        this.uiInventoryController.gameObject.SetActive(false);
    }
    public void ShowInventory()
    {

        List<ItemInInventory> items = PlayerController.Instance.InventoryController.ListItems;

        if (items.Count == 0)
        {
            this.ClearItems();
            return;
        }

        this.ClearItems();

        // ✅ Sort TRƯỚC - hiệu quả nhất cho small lists
        List<ItemInInventory> sortedItems = this.SortItems(items);

        // ✅ Spawn theo thứ tự đã sort
        foreach (ItemInInventory item in sortedItems)
        {
            if (item != null) // ✅ Extra safety
            {
                this.uiInventoryController.UIInventorySpawner.SpawnItem(item);
            }
        }
    }
    private void ClearItems()
    {
        this.uiInventoryController.UIInventorySpawner.ClearItems();
    }
    // ✅ Optimized sorting algorithm
    private List<ItemInInventory> SortItems(List<ItemInInventory> items)
    {
        if (items == null || items.Count <= 1) return items ?? new List<ItemInInventory>();

        switch (this.inventorySort)
        {
            case InventorySort.ByName:
                // ✅ Sort by ItemID.ToString() (itemName)
                return items
                    .Where(item => item?.itemSO != null) // Filter valid items
                    .OrderBy(item => item.itemSO.itemID.ToString())
                    .ToList();

            case InventorySort.ByCount:
                return items
                    .Where(item => item != null)
                    .OrderBy(item => item.itemsCount)
                    .ToList();

            default:
                return items.Where(item => item != null).ToList();
        }
    }
}

