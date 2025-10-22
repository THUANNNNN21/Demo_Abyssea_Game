using UnityEngine;

public class ObjectFollowPlayer : FollowPlayerMovement
{
    protected override void LoadValues()
    {
        base.LoadValues();
        this.SetSpeed(2f);
        this.GetTarget();
    }
    private void FixedUpdate()
    {
        this.Moving();
    }
    protected override void Moving()
    {
        if (this.target == null) return;
        transform.position = Vector3.Lerp(transform.position, this.target.position, Time.fixedDeltaTime * this.speed);
    }
}
