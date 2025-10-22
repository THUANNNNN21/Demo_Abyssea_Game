using UnityEngine;

public class UIFollowTarget : EntityFollowTarget
{
    private void FixedUpdate()
    {
        this.Moving();
    }
    protected override void Moving()
    {
        if (this.target == null) return;
        transform.position = Vector3.Lerp(transform.position, this.target.position, Time.fixedDeltaTime * this.speed);
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
