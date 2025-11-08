using UnityEngine;

public class LockTargetMovement : FollowPlayerMovement
{
    public bool initialized = false;
    void Start()
    {
        this.GetTarget();
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            LookAtTarget();
            Moving();
        }
    }

    protected override void LookAtTarget()
    {
        if (!initialized)
        {
            base.LookAtTarget();
            initialized = true;
        }
    }
}