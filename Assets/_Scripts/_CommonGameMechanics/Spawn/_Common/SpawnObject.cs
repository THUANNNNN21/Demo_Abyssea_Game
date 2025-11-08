using System.Collections.Generic;
using UnityEngine;
public abstract class SpawnObject : Cooldown
{
    [Header("Spawn Object Settings")]
    [SerializeField] protected GameObject prefabToSpawn;
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected GameObject Spawnner;
    public PoolObject poolObject;
    [SerializeField] protected List<Transform> minions;
    [SerializeField] protected int prefabsCanExist = 10;

    protected virtual GameObject GetObjectFromPool()
    {
        for (int i = 0; i < poolObject.pool.Count; i++)
        {
            if (poolObject.pool[i].name == this.prefabToSpawn.name)
            {
                return this.GetPoolObj(i);
            }
        }
        return this.CreateNewObject();
    }
    private GameObject GetPoolObj(int index)
    {
        GameObject pooledObj = poolObject.pool[index].gameObject;
        poolObject.pool.RemoveAt(index);
        return pooledObj;
    }
    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(this.prefabToSpawn, this.Spawnner.transform);
        newObj.name = this.prefabToSpawn.name;
        return newObj;
    }
    // Use this method to spawn and not return the object
    protected virtual void Spawn()
    {
        this.ClearMinion();
        if (this.minions.Count >= this.prefabsCanExist) return;
        GameObject obj = this.GetObjectFromPool();
        obj.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        obj.SetActive(true);
        // foreach (var despawn in obj.GetComponents<DespawnObject>())
        // {
        //     despawn.poolObject = this.poolObject;
        // }
        this.ResetAnimator(obj.GetComponentInChildren<Animator>());
        minions.Add(obj.transform);
    }
    // Use this method to spawn and return the spawned object
    protected virtual GameObject SpawnAndReturn()
    {
        this.ClearMinion();
        if (this.minions.Count >= this.prefabsCanExist) return null;
        GameObject obj = this.GetObjectFromPool();
        obj.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        obj.SetActive(true);
        // foreach (var despawn in obj.GetComponents<DespawnObject>())
        // {
        //     despawn.poolObject = this.poolObject;
        // }
        this.ResetAnimator(obj.GetComponentInChildren<Animator>());
        minions.Add(obj.transform);
        return obj;
    }
    protected virtual void ResetAnimator(Animator animator)
    {
        if (animator != null)
        {
            animator.Update(0);
            animator.ResetTrigger("isDestroyed");
        }
    }
    private void ClearMinion()
    {
        for (int i = minions.Count - 1; i >= 0; i--)
        {
            if (minions[i] == null || !minions[i].gameObject.activeSelf)
            {
                minions.RemoveAt(i);
            }
        }
    }
}
