using UnityEngine;
public class ProjectileMovement : EntityFollowTarget
{
    protected override void LoadComponents()
    {
        this.LoadTarget();
    }
    protected override void LoadValues()
    {
        this.SetSpeed(30f); // Set a default speed for the projectile
    }
    private void LoadTarget()
    {
        if (this.target != null) return;
        this.target = this.transform.Find("MouseTarget");
    }
    void FixedUpdate()
    {
        this.LookAtTarget();  // Cập nhật direction trước
        this.Moving();        // Sau đó di chuyển
    }
    protected override void LookAtTarget()
    {
        if (!this.initialized)
        {
            this.target.position = InputManager.Instance.GetMouseWorldPosition();
            base.LookAtTarget();
            this.initialized = true;
        }
    }
    // protected override void MakeTrajectory()
    // {
    //     this.GetDirection();
    //     Vector3 newPosition = this.transform.position + speed * Time.deltaTime * this.direction;
    //     this.transform.parent.position = newPosition;
    // }
    // private void GetDirection()
    // {
    //     // Chỉ lấy hướng ban đầu theo chuột một lần khi spawn
    //     if (!this.initialized)
    //     {
    //         this.direction = (InputManager.Instance.GetMouseWorldPosition() - this.transform.position).normalized;
    //         this.initialized = true;
    //     }
    // }
}