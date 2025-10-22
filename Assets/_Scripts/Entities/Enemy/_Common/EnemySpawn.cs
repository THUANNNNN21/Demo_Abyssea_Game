using UnityEditor.Timeline;
using UnityEngine;

public class EnemySpawn : SpawnObject
{
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
        if (this.Spawnner == null)
        {
            Transform holder = this.transform.Find("Holder");
            if (holder != null)
            {
                this.Spawnner = holder.gameObject;
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
            this.SpawnRandom();
            this.ResetCooldown();
        }
    }
    private void SpawnRandom()
    {
        for (int i = 0; i < this.objectsPerSpawn; i++)
        {
            this.spawnPoint = this.getRandomPoints.GetRandomSpawnPoint();
            this.prefabToSpawn = this.getRandomPrefabs.GetRandomPrefab();
            this.Spawn();
        }
    }
    private void FixedUpdate()
    {
        this.Timing();
        this.SpawnWithCooldown();
    }
}
