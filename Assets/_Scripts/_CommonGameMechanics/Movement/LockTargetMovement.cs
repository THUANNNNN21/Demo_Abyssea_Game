using UnityEngine;

public class LockTargetMovement : FollowPlayerMovement
{
    public bool initialized = false;
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
