using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySort : MyMonoBehaviour
{
    [SerializeField] private InventoryController inventoryController;
    public InventoryController InventoryController { get => inventoryController; }
    [SerializeField] private InvSortType invSortType = InvSortType.ByName;
    public InvSortType InventorySortType
    {
        get => invSortType;
        set
        {
            invSortType = value;
            SortItems();
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemController();
    }
    private void LoadItemController()
    {
        if (this.inventoryController != null) return;
        this.inventoryController = GetComponentInParent<InventoryController>();
        Debug.LogWarning(this.gameObject.name + ": Load InventoryController");
    }
    #region Unity Methods
    void Start()
    {
        SortInventory();
    }
    #endregion

    #region Sort Methods
    public List<ItemInInventory> GetSortedItems()
    {
        return SortItems();
    }
    private List<ItemInInventory> SortItems()
    {
        List<ItemInInventory> listItems = this.inventoryController.Inventory.ListItems;
        if (listItems == null || listItems.Count <= 1)
            return listItems ?? new List<ItemInInventory>();
        List<ItemInInventory> sorted = invSortType switch
        {
            InvSortType.ByName => listItems
                                .Where(item => item?.itemSO != null)
                                .OrderBy(item => item.itemSO.itemName.ToString())
                                .ToList(),
            InvSortType.ByCount => listItems
                                .Where(item => item != null)
                                .OrderBy(item => item.itemsCount)
                                .ToList(),
            _ => listItems.Where(item => item != null).ToList(),
        };
        return sorted;
    }
    private void SortInventory()
    {
        this.inventoryController.Inventory.SetListItems(SortItems());
    }
    #endregion
}