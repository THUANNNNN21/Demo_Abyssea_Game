using UnityEngine;

public class LockTargetMovement : FollowPlayerMovement
{
    private void FixedUpdate()
    {
        this.LookAtTarget();
        this.Moving();
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
