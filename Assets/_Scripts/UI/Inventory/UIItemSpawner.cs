using TMPro;
using UnityEngine;

[RequireComponent(typeof(DespawnObject))]
public class UIItemSpawner : SpawnObject
{
    [SerializeField] private UIInventoryController uiInventoryController;
    public UIInventoryController UIInventoryController => uiInventoryController;
    [SerializeField] private DespawnObject despawnObject;
    public DespawnObject DespawnObject => despawnObject;
    protected override void LoadComponents()
    {
        this.LoadController();
        this.LoadPoolObject();
        this.LoadPrefabToSpawn();
        this.LoadSpawnPoint();
        this.LoadDespawnObject();
    }
    private void LoadController()
    {
        if (this.uiInventoryController == null)
        {
            this.uiInventoryController = this.GetComponentInParent<UIInventoryController>();
        }
    }
    private void LoadPoolObject()
    {
        if (this.poolObject == null)
        {
            this.poolObject = GameObject.Find("PoolManager").GetComponent<PoolObject>();
        }
    }
    private void LoadPrefabToSpawn()
    {
        if (this.prefabToSpawn == null)
        {
            this.prefabToSpawn = Resources.Load<GameObject>("_Prefab/UI/UI_Item");
        }
    }
    private void LoadSpawnPoint()
    {
        if (this.spawnPoint == null)
        {
            this.spawnPoint = this.transform;
        }
    }
    private void LoadDespawnObject()
    {
        if (this.despawnObject == null)
        {
            this.despawnObject = this.GetComponent<DespawnObject>();
        }
    }
    public GameObject SpawnItem(ItemInInventory item)
    {
        // Tìm slot trống trong ContentHolder (slot đã được spawn bởi UISlotSpawner)
        Transform emptySlot = this.FindEmptySlot();
        if (emptySlot == null)
        {
            Debug.LogWarning("Không tìm thấy slot trống!");
            return null;
        }

        // Tạm thời set Spawnner là slot trống
        this.Spawnner = emptySlot.gameObject;
        GameObject itemObj = this.SpawnAndReturn();

        UIItem uiItem = itemObj.GetComponentInChildren<UIItem>();
        uiItem.SetItemName(item.itemSO.itemID.ToString());
        uiItem.SetItemCount(item.itemsCount, item.itemSO.defaultMaxStack);
        uiItem.SetItemSprite(item.itemSO.sprite);
        uiItem.SetAbleToSelectSkill(item.itemSO.skillType);

        return itemObj;
    }

    private Transform FindEmptySlot()
    {
        foreach (Transform slot in this.uiInventoryController.ContentHolder.transform)
        {
            // Kiểm tra slot có component ItemSlot và chưa có item (childCount == 0)
            if (slot.GetComponent<ItemSlot>() != null && slot.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
    public void ClearItems()
    {
        // Xóa items từ tất cả các slots trong ContentHolder
        foreach (Transform slot in this.uiInventoryController.ContentHolder.transform)
        {
            if (slot.GetComponent<ItemSlot>() != null)
            {
                // Xóa tất cả items trong slot này
                foreach (Transform item in slot)
                {
                    this.DespawnObject.ReturnObject(item.gameObject);
                }
            }
        }
    }
    // void Start()
    // {
    //     this.SpawnTest();
    // }
    // private void SpawnTest()
    // {
    //     for (int i = 0; i < 10; i++)
    //     {
    //         this.SpawnItem(null, i);
    //     }
    // }
}
