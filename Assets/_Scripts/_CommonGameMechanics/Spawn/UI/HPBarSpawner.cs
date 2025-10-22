using UnityEngine;

public class HPBarSpawner : SpawnObject
{
    public static HPBarSpawner Instance { get; private set; }
    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        base.Awake();
    }
    protected override void LoadComponents()
    {
        this.LoadSpawner();
        this.LoadPoolObject();
        this.LoadPrefabToSpawn();
    }
    private void LoadSpawner()
    {
        if (this.Spawnner == null)
        {
            Transform holder = this.transform.Find("Holder");
            if (holder != null)
            {
                this.Spawnner = holder.gameObject;
            }
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
            this.prefabToSpawn = Resources.Load<GameObject>("_Prefab/UI/HPBar");
        }
    }
    public void SpawnHPBar(EnemyController enemyController)
    {
        this.spawnPoint = enemyController.transform;
        GameObject hpBar = this.SpawnAndReturn();
        HPBar hpBarComponent = hpBar.GetComponent<HPBar>();
        hpBarComponent.SetEnemyController(enemyController);
    }
}
