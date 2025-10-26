using TMPro;
using UnityEngine;

[RequireComponent(typeof(DespawnObject))]
public class UISlotSpawner : SpawnObject
{
    [SerializeField] private UIInventoryController uiInventoryController;
    public UIInventoryController UIInventoryController => uiInventoryController;
    [SerializeField] private DespawnObject despawnObject;
    public DespawnObject DespawnObject => despawnObject;
    protected override void LoadComponents()
    {
        this.LoadController();
        this.LoadSpawner();
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
    private void LoadSpawner()
    {
        if (this.Spawnner != null) return;
        this.Spawnner = this.uiInventoryController.ContentHolder;
        Debug.LogWarning(this.gameObject.name + ": Load Spawnner");
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
            this.prefabToSpawn = Resources.Load<GameObject>("_Prefab/UI/ItemSlot");
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
    public GameObject SpawnItem()
    {
        GameObject itemObj = this.SpawnAndReturn();
        return itemObj;
    }
    public void ClearItems()
    {
        foreach (Transform item in this.Spawnner.transform)
        {
            this.DespawnObject.ReturnObject(item.gameObject);
        }
    }
}
