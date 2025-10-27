using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MyMonoBehaviour
{
    [SerializeField] private UIInventoryController uiInventoryController;
    public UIInventoryController UIInventoryController => uiInventoryController;

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
        this.ShowSlotItems();
        this.ShowInventory();
    }
    private void SubscribeEvents()
    {
        InventoryDrop.AfterDropItem += ShowInventory;
        ItemLooter.AfterPickupItem += ShowInventory;
        ItemUpgrade.AfterUpgradeItem += ShowInventory;
        LevelUp.OnLevelUp += ShowInventory;
    }
    void OnDestroy()
    {
        InventoryDrop.AfterDropItem -= ShowInventory;
        ItemLooter.AfterPickupItem -= ShowInventory;
        ItemUpgrade.AfterUpgradeItem -= ShowInventory;
        LevelUp.OnLevelUp -= ShowInventory;
    }
    public virtual void Toggle()
    {
        this.uiInventoryController.gameObject.SetActive(!this.uiInventoryController.gameObject.activeSelf);
    }
    public void Close()
    {
        this.uiInventoryController.gameObject.SetActive(false);
    }
    private void ShowSlotItems()
    {
        this.uiInventoryController.UISlotInventorySpawner.ClearItems();
        int maxSlot = PlayerController.Instance.InventoryController.MaxSlot;
        // Debug.Log($"Spawning {maxSlot} slots");

        for (int i = 0; i < maxSlot; i++)
        {
            GameObject slot = this.uiInventoryController.UISlotInventorySpawner.SpawnItem();
            // if (slot != null)
            // {
            //     Debug.Log($"Spawned slot {i}: {slot.name}");
            // }
        }
    }
    private void ShowInventory(int level)
    {
        this.ShowInventory();
    }
    public void ShowInventory()
    {
        // ✅ Get sorted items directly from InventoryController
        List<ItemInInventory> sortedItems = PlayerController.Instance.InventoryController.GetSortedItems();
        // Debug.Log($"ShowInventory called. Items count: {sortedItems.Count}");

        if (sortedItems.Count == 0)
        {
            this.ClearItems();
            return;
        }

        this.ClearItems();

        foreach (ItemInInventory item in sortedItems)
        {
            if (item != null)
            {
                GameObject spawnedItem = this.uiInventoryController.UIInventorySpawner.SpawnItem(item);
                // if (spawnedItem != null)
                // {
                //     Debug.Log($"Spawned item: {item.itemSO.itemID}");
                // }
            }
        }
    }
    private void ClearItems()
    {
        this.uiInventoryController.UIInventorySpawner.ClearItems();
        Debug.Log("Cleared all items in UI Inventory");
    }
}

