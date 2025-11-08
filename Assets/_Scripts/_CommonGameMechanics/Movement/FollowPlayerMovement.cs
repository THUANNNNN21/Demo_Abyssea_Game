using UnityEngine;

public abstract class FollowPlayerMovement : EntityFollowTarget
{
    protected PlayerController PlayerController => PlayerController.Instance;

    protected override void LoadValues()
    {
        base.LoadValues();
    }

    protected virtual void LoadTarget()
    {
        if (PlayerController != null)
        {
            target = PlayerController.transform;
        }
    }

    protected virtual void GetTarget()
    {
        if (PlayerController != null)
        {
            this.target = PlayerController.transform;
        }
    }
}
