using UnityEngine;

public class EntityFollowTarget : EntityMovement
{
    [Header("Follow Target Settings")]
    [SerializeField] protected Transform target;
    protected virtual void LookAtTarget()
    {
        this.direction = (target.position - transform.parent.position).normalized;
    }
    protected virtual void Moving()
    {
        this.transform.parent.Translate(speed * Time.fixedDeltaTime * this.direction);
    }
}
