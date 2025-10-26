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
    private void OnEnable()
    {
        InputManager.OnDropItem += DropItem;
    }
    private void OnDisable()
    {
        InputManager.OnDropItem -= DropItem;
    }
    private void DropItem(int itemPosition)
    {
        if (itemPosition < 1 || itemPosition > 9) return;
        int itemIndex = itemPosition - 1;
        this.DropByIndex(itemIndex, this.GetDropPoint());
        AfterDropItem?.Invoke();
    }
    private GameObject GetDropPoint()
    {
        DropPoint dropPoint = this.GetComponentInChildren<DropPoint>();
        if (dropPoint != null)
            return dropPoint.gameObject;
        return null;
    }
    private void DropByIndex(int index, GameObject dropPoint)
    {
        if (index > InventoryController.ListItems.Count - 1)
        {
            return;
        }
        ItemInInventory itemInInventory = this.InventoryController.ListItems[index];
        ItemSpawner.Instance.DropItemInInventory(itemInInventory, dropPoint);
        InventoryController.DeductItem(itemInInventory.itemSO.itemID, itemInInventory.upgradeLevel, 1);
    }
}
