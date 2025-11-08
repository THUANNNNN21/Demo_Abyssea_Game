using UnityEngine;

public class EnemyAbility : MyMonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController => enemyController;
    [SerializeField] private EnemyImpact enemyImpact;
    public EnemyImpact EnemyImpact => enemyImpact;
    [SerializeField] private EnemyShooting enemyShooting;
    public EnemyShooting EnemyShooting => enemyShooting;
    [SerializeField] private ShootingStateTracker shootingStateTracker;
    public ShootingStateTracker ShootingStateTracker => this.shootingStateTracker;
    [SerializeField] private ObjAppearing objAppearing;
    public ObjAppearing ObjAppearing { get => objAppearing; }
    [SerializeField] private AppearanceStateTracker afterAppear;
    public AppearanceStateTracker AfterAppear => this.afterAppear;
    [SerializeField] private EnemyWarp enemyWarp;
    public EnemyWarp EnemyWarp => enemyWarp;
    [SerializeField] private EnemySummon enemySummon;
    public EnemySummon EnemySummon => enemySummon;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyController();
        this.LoadObjAppearing();
        this.LoadAfterAppear();
        this.LoadEnemyImpact();
        this.LoadEnemyShooting();
        this.LoadShootingStateTracker();
        this.LoadEnemyWarp();
        this.LoadEnemySummon();

    }
    private void LoadEnemyImpact()
    {
        if (this.enemyImpact != null) return;
        else
        {
            this.enemyImpact = this.GetComponentInChildren<EnemyImpact>();
        }
    }
    private void LoadEnemyController()
    {
        if (enemyController != null) return;
        enemyController = GetComponentInParent<EnemyController>();
    }
    private void LoadEnemyShooting()
    {
        if (this.enemyShooting != null) return;
        else
        {
            this.enemyShooting = this.GetComponentInChildren<EnemyShooting>();
        }
    }
    private void LoadEnemyWarp()
    {
        if (this.enemyWarp != null) return;
        else
        {
            this.enemyWarp = this.GetComponentInChildren<EnemyWarp>();
        }
    }
    private void LoadEnemySummon()
    {
        if (this.enemySummon != null) return;
        else
        {
            this.enemySummon = this.GetComponentInChildren<EnemySummon>();
        }
    }
    protected void LoadObjAppearing()
    {
        if (this.objAppearing != null) return;
        else
        {
            this.objAppearing = GetComponentInChildren<ObjAppearing>();
        }
    }
    private void LoadAfterAppear()
    {
        if (this.afterAppear != null) return;
        else
        {
            this.afterAppear = GetComponentInChildren<AppearanceStateTracker>();
        }
    }
    protected void LoadShootingStateTracker()
    {
        if (this.shootingStateTracker != null) return;
        else
        {
            this.shootingStateTracker = GetComponentInChildren<ShootingStateTracker>();
        }
    }
}
