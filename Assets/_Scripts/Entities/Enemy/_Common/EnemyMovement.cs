using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : FollowPlayerMovement
{
    [Header("Normal Enemy Movement Settings")]
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController { get => enemyController; }
    [SerializeField] private float minDistanceToTarget = 0.5f;
    [SerializeField] private float maxDistanceToTarget = 2.0f;
    [SerializeField] private float stopBuffer = 0.1f;
    private bool isWithinRange = false;
    [SerializeField] protected float distance;
    private AppearanceStateTracker moveAfterAppear;
    private ShootingStateTracker shootingStateTracker;
    private float horizontal;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyController();
        this.LoadMoveAfterAppear();
        this.LoadShootingStateTracker();
    }
    private void LoadMoveAfterAppear()
    {
        this.moveAfterAppear = this.enemyController.AfterAppear;
    }
    private void LoadShootingStateTracker()
    {
        this.shootingStateTracker = this.enemyController.ShootingStateTracker;
    }
    protected void LoadEnemyController()
    {
        if (this.enemyController == null)
        {
            this.enemyController = transform.parent.GetComponentInParent<EnemyController>();
        }
    }
    protected override void LoadValues()
    {
        base.LoadValues();
        this.SetSpeed(enemyController.EnemySO.speed);
    }
    void FixedUpdate()
    {
        if (this.MoveAfterAppear() && this.MoveAfterShoot())
        {
            this.LookAtTarget();
            this.Moving();
        }
    }
    protected override void Moving()
    {
        this.MoveByDistance();
    }
    private void MoveByDistance()
    {
        if (this.target == null) return;

        this.distance = Vector3.Distance(transform.parent.position, target.position);

        if (!isWithinRange)
        {
            // ✅ Đang ở xa → tiến lại gần
            if (this.distance > this.minDistanceToTarget + stopBuffer)
            {
                this.MoveTowardsTarget();
            }
            else
            {
                // ✅ Đã vào vùng an toàn
                isWithinRange = true;
            }
        }
        else
        {
            // ✅ Đang trong vùng → kiểm tra cần di chuyển
            if (this.distance > this.maxDistanceToTarget)
            {
                isWithinRange = false;
                this.MoveTowardsTarget();
            }
            else if (this.distance < this.minDistanceToTarget - stopBuffer)
            {
                this.MoveAwayFromTarget();
            }
            // Nếu trong khoảng [minDistance-buffer, maxDistance] → đứng yên
        }
    }
    private void MoveTowardsTarget()
    {
        Vector3 newPosition = this.transform.parent.position + this.speed * Time.fixedDeltaTime * this.direction;
        this.transform.parent.position = newPosition;
        this.RotateController();
    }
    private void MoveAwayFromTarget()
    {
        Vector3 newPosition = this.transform.parent.position - 100 * Time.fixedDeltaTime * this.direction;
        this.transform.parent.position = newPosition;
        this.RotateController();
    }
    private void NameAxis()
    {
        this.horizontal = this.direction.x;
    }
    private bool MoveAfterAppear()
    {
        if (this.moveAfterAppear != null)
        {
            return this.moveAfterAppear.HasAppeared;
        }
        return true;
    }
    private bool MoveAfterShoot()
    {
        if (this.shootingStateTracker != null)
        {
            return !this.shootingStateTracker.IsShooting;
        }
        return true;
    }
    private void RotateController()
    {
        this.NameAxis();
        Vector3 currentScale = this.transform.parent.localScale;

        if (horizontal < 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x); // Đảm bảo X dương
        }
        else if (horizontal > 0)
        {
            currentScale.x = -Mathf.Abs(currentScale.x); // Đảm bảo X âm
        }

        this.transform.parent.localScale = currentScale;
    }
}
