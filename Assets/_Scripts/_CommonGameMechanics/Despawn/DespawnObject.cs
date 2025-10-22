using UnityEngine;

public class DespawnObject : MyMonoBehaviour
{
    public PoolObject poolObject;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoolObject();
    }
    private void LoadPoolObject()
    {
        if (this.poolObject == null)
        {
            this.poolObject = GameObject.Find("PoolManager").GetComponent<PoolObject>();
        }
    }
    public virtual void Despawn()
    {
        this.HandleDespawn();
    }
    public void HandleDespawn()
    {
        if (poolObject == null)
        {
            Destroy(gameObject);
            return;
        }

        if (poolObject.pool == null)
        {
            Destroy(gameObject);
            return;
        }
        this.ReturnObject(gameObject);
    }
    public void ReturnObject(GameObject obj)
    {
        if (!poolObject.pool.Contains(obj.transform))
        {
            obj.SetActive(false);
            var entity = obj.GetComponentInChildren<EntityMovement>();
            if (entity != null)
            {
                entity.initialized = false;
            }
            poolObject.pool.Add(obj.transform);
        }
    }
}
