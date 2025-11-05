using UnityEngine;

public class DespawnObject : MyMonoBehaviour
{
    public PoolObject poolObject;
    [SerializeField] protected Animator animator;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoolObject();
        this.LoadAnimator();
    }
    private void LoadPoolObject()
    {
        if (this.poolObject == null)
        {
            this.poolObject = GameObject.Find("PoolManager").GetComponent<PoolObject>();
        }
    }
    private void LoadAnimator()
    {
        if (this.animator == null)
        {
            this.animator = GetComponentInChildren<Animator>();
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
            var entity = obj.GetComponentInChildren<LockTargetMovement>();
            if (entity != null)
            {
                entity.initialized = false;
            }
            poolObject.pool.Add(obj.transform);
        }
    }
}
