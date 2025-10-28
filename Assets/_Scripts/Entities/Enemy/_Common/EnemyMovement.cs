using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : FollowPlayerMovement
{
    [Header("Normal Enemy Movement Settings")]
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController => enemyController;
    [SerializeField] private float minDistanceToTarget = 0.5f;
    [SerializeField] private float maxDistanceToTarget = 2.0f;
    [SerializeField] private float stopBuffer = 0.1f;
    private bool isWithinRange = false;
    [SerializeField] protected float distance;
    private AppearanceStateTracker moveAfterAppear;
    private ShootingStateTracker shootingStateTracker;
    private float horizontal;
    private Rigidbody2D rb;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyController();
        LoadMoveAfterAppear();
        LoadShootingStateTracker();
        LoadRigidbody2D();
    }

    private void LoadMoveAfterAppear()
    {
        moveAfterAppear = enemyController.AfterAppear;
    }

    private void LoadShootingStateTracker()
    {
        shootingStateTracker = enemyController.ShootingStateTracker;
    }

    protected void LoadEnemyController()
    {
        if (enemyController == null)
        {
            enemyController = transform.parent.GetComponentInParent<EnemyController>();
        }
    }

    private void LoadRigidbody2D()
    {
        if (rb == null)
        {
            rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        }
    }

    protected override void LoadValues()
    {
        base.LoadValues();
        SetSpeed(enemyController.EnemySO.speed);
    }

    void FixedUpdate()
    {
        if (MoveAfterAppear() && MoveAfterShoot())
        {
            LookAtTarget();
            Moving();
        }
    }

    protected override void Moving()
    {
        MoveByDistance();
    }

    private void MoveByDistance()
    {
        if (target == null) return;

        distance = Vector3.Distance(transform.parent.position, target.position);

        // Move towards target if farther than minimum distance
        if (distance > minDistanceToTarget)
        {
            MoveTowardsTarget();
        }
        // Otherwise, stay still
    }

    private void MoveTowardsTarget()
    {
        Vector3 newPosition = transform.parent.position + speed * Time.fixedDeltaTime * direction;
        rb.MovePosition(newPosition);
        RotateController();
    }

    private void NameAxis()
    {
        horizontal = direction.x;
    }

    private bool MoveAfterAppear()
    {
        if (moveAfterAppear != null)
        {
            return moveAfterAppear.HasAppeared;
        }
        return true;
    }

    private bool MoveAfterShoot()
    {
        if (shootingStateTracker != null)
        {
            return !shootingStateTracker.IsShooting;
        }
        return true;
    }

    private void RotateController()
    {
        NameAxis();
        Vector3 currentScale = transform.parent.localScale;

        if (horizontal < 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        else if (horizontal > 0)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
        }

        transform.parent.localScale = currentScale;
    }
}
