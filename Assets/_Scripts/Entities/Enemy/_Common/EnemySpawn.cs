using UnityEditor.Timeline;
using UnityEngine;

public class EnemySpawn : SpawnObject
{
    private static EnemySpawn instance;
    public static EnemySpawn Instance { get { return instance; } }
    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }
    [SerializeField] private int objectsPerSpawn;
    [Header("Random Instantiate")]
    [SerializeField] private GetRandomPoints getRandomPoints;
    [SerializeField] private GetRandomPrefabs getRandomPrefabs;
    protected override void LoadComponents()
    {
        this.LoadSpawner();
        this.LoadListPrefab();
        this.LoadListSpawnPoint();
        this.LoadPoolObject();
    }
    private void LoadSpawner()
    {
        if (this.spawnner == null)
        {
            Transform holder = this.transform.Find("Holder");
            if (holder != null)
            {
                this.spawnner = holder.gameObject;
            }
        }
    }
    private void LoadListPrefab()
    {
        if (this.getRandomPrefabs == null)
        {
            this.getRandomPrefabs = GetComponentInChildren<GetRandomPrefabs>();
        }
    }
    private void LoadListSpawnPoint()
    {
        if (this.getRandomPoints == null)
        {
            this.getRandomPoints = GetComponentInChildren<GetRandomPoints>();
        }
        if (getRandomPoints.SpawnPoints.Count == 0)
        {
            this.getRandomPoints.GetListSpawnPoints();
        }
    }
    private void LoadPoolObject()
    {
        if (this.poolObject == null)
        {
            this.poolObject = GameObject.Find("PoolManager").GetComponent<PoolObject>();
        }
    }
    protected override void LoadValues()
    {
        this.delayTime = 5f;
    }
    protected void SpawnWithCooldown()
    {
        if (this.isReady)
        {
            this.SpawnRandom(this.objectsPerSpawn);
            this.ResetCooldown();
        }
    }
    private void SpawnRandom(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            this.spawnPoint = this.getRandomPoints.GetRandomSpawnPoint();
            this.prefabToSpawn = this.getRandomPrefabs.GetRandomPrefab();
            GameObject enemy = this.SpawnAndReturn();
            if (enemy != null)
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                HPBarSpawner.Instance.SpawnHPBar(enemyController);
            }
        }
    }
    void Start()
    {
        this.SpawnRandom(10);
    }
    private void FixedUpdate()
    {
        this.Timing();
        this.SpawnWithCooldown();
    }
    public void SetObjectsPerSpawn(int amount)
    {
        this.objectsPerSpawn = amount;
    }
}
