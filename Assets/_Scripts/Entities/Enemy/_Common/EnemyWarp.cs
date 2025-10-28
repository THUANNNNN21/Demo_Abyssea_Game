using UnityEngine;

public class EnemyWarp : Warp
{
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController { get => enemyController; }
    [SerializeField] private bool isWarpToPlayer = true;
    [SerializeField] private float maxDistanceToWarp = 5f; // Distance threshold to trigger warp

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyController();
    }

    private void LoadEnemyController()
    {
        if (enemyController == null)
        {
            enemyController = GetComponentInParent<EnemyController>();
        }
    }

    void Update()
    {
        CheckWarpPosition();
    }

    void FixedUpdate()
    {
        Timing();
    }

    public void StartWarp()
    {
        if (!isReady) return;
        enemyController.Animator.SetBool("warp", true);
    }

    public void WarpFinish()
    {
        enemyController.Animator.SetBool("warp", false);
    }
    public void Warping()
    {
        Warping(enemyController.transform);
    }

    protected override void CheckWarpPosition()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 position = transform.position;
        float distance = Vector3.Distance(position, playerPos);
        if (distance > maxDistanceToWarp) return;
        if (!isReady) return;
        if (isWarpToPlayer)
        {
            Debug.Log("Warping to player");
            warpDirection = playerPos - position; // Warp towards player
        }
        else
        {
            Debug.Log("Warping away from player");
            warpDirection = position - playerPos; // Warp away from player
        }
        this.StartWarp();
    }
}
