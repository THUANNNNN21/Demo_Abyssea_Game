using TMPro;
using Unity.VisualScripting;
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
        if (item.isOnHotKeySlot == true)
        {
            Debug.LogWarning("Item đang ở HotKeySlot, không thể spawn trong Inventory!");
            return null;
        }
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
        this.SetUIItem(uiItem, item);

        // UIItem uiItem = itemObj.GetComponentInChildren<UIItem>();
        // uiItem.SetItemName(item.itemSO.itemID.ToString());
        // uiItem.SetItemCount(item.itemsCount, item.itemSO.defaultMaxStack);
        // uiItem.SetItemSprite(item.itemSO.sprite);
        // uiItem.SetAbleToSelectSkill(item.itemSO.skillType);
        // uiItem.SetAbleToSelectEquipment(item.itemSO);
        // uiItem.SetID(item.ID);

        return itemObj;
    }
    private void SetUIItem(UIItem uiItem, ItemInInventory item)
    {
        uiItem.SetItemName(item.itemSO.itemName.ToString());
        uiItem.SetItemCount(item.itemsCount, item.itemSO.defaultMaxStack);
        uiItem.SetItemSprite(item.itemSO.sprite);
        uiItem.SetAbleToSelectSkill(item.itemSO.skillType);
        uiItem.SetAbleToSelectEquipment(item.itemSO);
        uiItem.SetID(item.ID);
        uiItem.SetCurrentLevel(item.upgradeLevel);
    }
    private Transform FindEmptySlot()
    {
        foreach (Transform slot in this.uiInventoryController.ContentHolder.transform)
        {
            if (slot.GetComponent<ItemSlot>() != null)
            {
                // Nếu slot không có item hoặc tất cả item con đều đang inactive
                bool allInactive = true;
                foreach (Transform child in slot)
                {
                    if (child.gameObject.activeSelf)
                    {
                        allInactive = false;
                        break;
                    }
                }
                if (slot.childCount == 0 || allInactive)
                {
                    return slot;
                }
            }
        }
        return null;
    }
    public void ClearItems()
    {
        this.ClearDirectItemsInHotKey();
        // Xóa items từ tất cả các slots trong ContentHolder
        foreach (Transform slot in this.uiInventoryController.ContentHolder.transform)
        {
            if (slot.GetComponent<ItemSlot>() != null)
            {
                // Xóa tất cả items trong slot này
                foreach (Transform item in slot)
                {
                    Debug.Log($"Despawning item: {item.gameObject.name}");
                    this.DespawnObject.ReturnObject(item.gameObject);
                }
            }
        }
    }
    public void ClearDirectItemsInHotKey()
    {
        // Tìm đối tượng duy nhất chứa HotKeyController
        HotKeyController hotKeyController = GameObject.FindFirstObjectByType<HotKeyController>();
        if (hotKeyController == null)
        {
            Debug.LogWarning("Không tìm thấy HotKeyController!");
            return;
        }

        Transform hotKeyTransform = hotKeyController.transform;

        // Xoá các con trực tiếp là item inventory
        for (int i = hotKeyTransform.childCount - 1; i >= 0; i--)
        {
            Transform child = hotKeyTransform.GetChild(i);
            if (child.GetComponent<UIItem>() != null)
            {
                Debug.Log($"Despawning hotkey item: {child.gameObject.name}");
                this.DespawnObject.ReturnObject(child.gameObject);
            }
            // Không xoá các item nằm sâu hơn (trong các con của con)
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
