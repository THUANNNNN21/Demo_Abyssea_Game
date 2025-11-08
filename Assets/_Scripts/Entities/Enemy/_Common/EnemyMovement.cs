using UnityEngine;

public class EnemyMovement : FollowPlayerMovement
{
    [Header("Normal Enemy Movement Settings")]
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController => enemyController;

    [Header("Movement Configuration")]
    [SerializeField] private float timeToChangeTarget = 0.5f;
    [SerializeField] private float targetRadius = 2f;
    [SerializeField] private float fakeTargetRadius = 10f;
    [SerializeField] private float distanceToPlayerForMove = 20f;

    [Header("Debug Info")]
    [SerializeField] protected float distance;

    private Transform dummyTarget;
    private float timer = 0f;
    private AppearanceStateTracker moveAfterAppear;
    private ShootingStateTracker shootingStateTracker;
    private float horizontal;
    private Rigidbody2D rb;

    #region Unity Lifecycle
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyController();
        LoadDummyTarget();
        LoadMoveAfterAppear();
        LoadShootingStateTracker();
        LoadRigidbody2D();
    }

    protected override void LoadValues()
    {
        base.LoadValues();
        SetSpeed(RandomSpeed());
    }

    protected override void LoadTarget()
    {
        if (dummyTarget != null)
        {
            target = dummyTarget;
            GetTarget(); // Initialize target position
        }
    }

    void FixedUpdate()
    {
        UpdateTargetTimer();

        if (CanMove() && target != null)
        {
            LookAtTarget();
            Moving();
        }
    }
    #endregion

    #region Load Methods
    private void LoadEnemyController()
    {
        if (enemyController != null) return;
        enemyController = GetComponentInParent<EnemyController>();
        Debug.LogWarning($"EnemyMovement: LoadEnemyController {transform.name}!");
    }

    private void LoadDummyTarget()
    {
        if (dummyTarget == null)
        {
            dummyTarget = transform.Find("DummyTarget");
            if (dummyTarget == null)
            {
                Debug.LogWarning("DummyTarget child not found. Please add a child GameObject named 'DummyTarget'.");
            }
        }
    }

    private void LoadMoveAfterAppear()
    {
        moveAfterAppear = enemyController.AfterAppear;
    }

    private void LoadShootingStateTracker()
    {
        shootingStateTracker = enemyController.ShootingStateTracker;
    }

    private void LoadRigidbody2D()
    {
        if (rb == null)
        {
            rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        }
    }
    #endregion

    #region Movement Logic
    private void UpdateTargetTimer()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= timeToChangeTarget)
        {
            GetTarget();
            timer = 0f;
        }
    }

    private bool CanMove()
    {
        return MoveAfterAppear() && MoveAfterShoot();
    }

    private bool MoveAfterAppear()
    {
        return moveAfterAppear != null ? moveAfterAppear.HasAppeared : true;
    }

    private bool MoveAfterShoot()
    {
        return shootingStateTracker != null ? !shootingStateTracker.IsShooting : true;
    }

    protected override void Moving()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        if (target == null) return;

        Vector3 newPosition = transform.parent.position + speed * Time.fixedDeltaTime * direction;
        rb.MovePosition(newPosition);
        RotateController();
    }

    private void RotateController()
    {
        horizontal = direction.x;

        if (!Mathf.Approximately(horizontal, 0))
        {
            float moveX = horizontal > 0 ? 1 : -1;
            enemyController.Animator.SetFloat("MoveX", moveX);
        }
    }
    #endregion

    #region Target Selection
    protected override void GetTarget()
    {
        if (dummyTarget == null) return;

        if (enemyController.EnemyCheckPlayer.CheckNearPlayer(distanceToPlayerForMove))
        {
            GetPlayerTarget();
        }
        else
        {
            GetFakePlayerTarget();
        }
        target = dummyTarget;
    }

    private void GetPlayerTarget()
    {
        if (PlayerController != null && dummyTarget != null)
        {
            Vector3 randomOffset = Random.insideUnitCircle * targetRadius;
            Vector3 newPosition = PlayerController.transform.position + randomOffset;
            dummyTarget.position = newPosition;
        }
    }

    private void GetFakePlayerTarget()
    {
        Vector3 randomOffset = Random.insideUnitCircle * fakeTargetRadius;
        Vector3 newPosition = transform.position + randomOffset;
        dummyTarget.position = newPosition;
    }
    #endregion

    #region Public Methods
    public void SetTargetRadius(float radius)
    {
        targetRadius = radius;
    }

    private float RandomSpeed()
    {
        float minSpeed = enemyController.EnemySO.speed - 1f;
        float maxSpeed = enemyController.EnemySO.speed + 1f;
        return Random.Range(minSpeed, maxSpeed);
    }
    #endregion
}
