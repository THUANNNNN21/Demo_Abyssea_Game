using System.Collections.Generic;
using UnityEngine;

public abstract class Shooting : SpawnObject
{
    protected string prefabName;
    [Header("Prefabs Can Exist")]
    [SerializeField] protected SkillSO skillSO;
    public SkillSO SkillSO { get => this.skillSO; }
    protected override void LoadComponents()
    {
        this.LoadSpawnPoint();
        this.LoadPoolObject();
    }
    private void LoadSpawnPoint()
    {
        if (this.spawnPoint == null)
        {
            this.spawnPoint = this.transform;
        }
    }
    private void LoadPoolObject()
    {
        if (this.poolObject == null)
        {
            this.poolObject = GameObject.Find("PoolManager").GetComponent<PoolObject>();
        }
    }
    protected void SetPrefabToSpawn()
    {
        this.prefabName = this.skillSO.prefabToSpawnName.ToString();
    }
}
