using System;
using UnityEngine;

public class InventoryDrop : MyMonoBehaviour
{
    public static event Action AfterDropItem;
    [SerializeField] private InventoryController inventoryController;
    public InventoryController InventoryController { get => inventoryController; }
    [SerializeField] private GameObject dropPoint;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventoryController();
        this.LoadDropPoint();
    }
    private void LoadInventoryController()
    {
        if (this.inventoryController != null) return;
        else
        {
            this.inventoryController = GetComponentInParent<InventoryController>();
        }
    }
    private void LoadDropPoint()
    {
        if (this.dropPoint != null) return;
        else
        {
            this.dropPoint = this.GetDropPoint();
        }
    }
    public void DropItem(string id)
    {
        this.DropByID(id, this.dropPoint);
        AfterDropItem?.Invoke();
    }
    private GameObject GetDropPoint()
    {
        DropPoint dropPoint = this.GetComponentInChildren<DropPoint>();
        if (dropPoint != null)
            return dropPoint.gameObject;
        return null;
    }
    private void DropByID(string id, GameObject dropPoint)
    {
        ItemInInventory itemInInventory = this.InventoryController.Inventory.ListItems.Find(item => item.ID == id);
        if (itemInInventory == null)
        {
            return;
        }
        ItemSpawner.Instance.DropItemInInventory(itemInInventory, dropPoint);
        InventoryController.Inventory.DeductItem(itemInInventory.itemSO.itemName, itemInInventory.upgradeLevel, 1);
    }
    // private void DropByIndex(int index, GameObject dropPoint)
    // {
    //     if (index > InventoryController.ListItems.Count - 1)
    //     {
    //         return;
    //     }
    //     ItemInInventory itemInInventory = this.InventoryController.ListItems[index];
    //     ItemSpawner.Instance.DropItemInInventory(itemInInventory, dropPoint);
    //     InventoryController.DeductItem(itemInInventory.itemSO.itemID, itemInInventory.upgradeLevel, 1);
    // }
}
