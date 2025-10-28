using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MyMonoBehaviour
{
    [Header("References Components")]
    [SerializeField] private Transform model;
    public Transform Model { get => model; }

    [SerializeField] private FollowPlayerMovement movement;
    public FollowPlayerMovement Movement { get => movement; }

    [SerializeField] private EnemyDamSender damageSender;
    public EnemyDamSender DamageSender { get => damageSender; }

    [SerializeField] private EnemyDamReceiver enemyDamReceiver;
    public EnemyDamReceiver EnemyDamReceiver { get => enemyDamReceiver; }
    [SerializeField] private EnemyImpact enemyImpact;
    public EnemyImpact EnemyImpact { get => enemyImpact; }

    [SerializeField] private DespawnByDistance despawnByDistance;
    public DespawnByDistance DespawnByDistance { get => despawnByDistance; }
    [SerializeField] private ObjAppearing objAppearing;
    public ObjAppearing ObjAppearing { get => objAppearing; }
    [SerializeField] private AppearanceStateTracker afterAppear;
    public AppearanceStateTracker AfterAppear => this.afterAppear;
    [SerializeField] private ShootingStateTracker shootingStateTracker;
    public ShootingStateTracker ShootingStateTracker => this.shootingStateTracker;
    [SerializeField] private EnemySO enemySO;
    public EnemySO EnemySO { get => enemySO; }
    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; }
    [SerializeField] private EnemyWarp warp;
    public EnemyWarp Warp { get => warp; }
    [SerializeField] private EnemyLevelUp enemyLevelUp;
    public EnemyLevelUp EnemyLevelUp { get => enemyLevelUp; }
    [SerializeField] private List<EnemyShooting> enemyShooting;
    public List<EnemyShooting> EnemyShooting { get => enemyShooting; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadDamageSender();
        this.LoadDamageReceiver();
        this.LoadEnemyImpact();
        this.LoadSO();
        this.LoadMovement();
        this.LoadDespawnByDistance();
        this.LoadAnimator();
        this.LoadObjAppearing();
        this.LoadAfterAppear();
        this.LoadEnemyLevelUp();
        this.LoadShootingStateTracker();
        this.LoadListEnemyShooting();
        this.LoadWarp();
    }
    private void LoadEnemyImpact()
    {
        if (this.enemyImpact != null) return;
        else
        {
            this.enemyImpact = this.GetComponentInChildren<EnemyImpact>();
            Debug.LogWarning($"LoadEnemyImpact: {this.gameObject.name}");
        }
    }
    private void LoadModel()
    {
        if (this.model == null)
        {
            this.model = this.GetComponentInChildren<Animator>().transform;
        }
    }
    private void LoadAnimator()
    {
        if (this.animator == null)
        {
            this.animator = this.GetComponentInChildren<Animator>();
        }
    }
    private void LoadMovement()
    {
        if (this.movement == null)
        {
            this.movement = this.GetComponentInChildren<FollowPlayerMovement>();
        }
    }
    private void LoadDamageSender()
    {
        if (this.damageSender == null)
        {
            this.damageSender = this.GetComponentInChildren<EnemyDamSender>();
        }
    }
    private void LoadDamageReceiver()
    {
        if (this.enemyDamReceiver == null)
        {
            this.enemyDamReceiver = this.GetComponentInChildren<EnemyDamReceiver>();
        }
    }
    private void LoadSO()
    {
        if (this.enemySO == null)
        {
            this.enemySO = Resources.Load<EnemySO>("_SO/EnemySO/" + this.name);
        }
    }
    private void LoadDespawnByDistance()
    {
        if (this.despawnByDistance == null)
        {
            this.despawnByDistance = this.GetComponent<DespawnByDistance>();
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
    private void LoadEnemyLevelUp()
    {
        if (this.enemyLevelUp == null)
        {
            this.enemyLevelUp = this.GetComponentInChildren<EnemyLevelUp>();
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
    private void LoadListEnemyShooting()
    {
        if (this.enemyShooting == null || this.enemyShooting.Count == 0)
        {
            this.enemyShooting = new List<EnemyShooting>(GetComponentsInChildren<EnemyShooting>());
        }
    }
    private void LoadWarp()
    {
        if (this.warp != null) return;
        else
        {
            this.warp = this.GetComponentInChildren<EnemyWarp>();
            Debug.LogWarning($"LoadWarp: {this.gameObject.name}");
        }
    }
    protected override void LoadValues()
    {
        if (this.enemySO == null) return;
        if (enemyDamReceiver != null)
        {
            this.enemyDamReceiver.SetHPMax(this.enemySO.maxHP);
        }
        if (this.damageSender != null)
        {
            this.damageSender.SetDamage(this.enemySO.damage);
        }
        if (this.movement != null)
        {
            this.movement.SetSpeed(this.enemySO.speed);
        }
        if (this.despawnByDistance != null)
        {
            this.despawnByDistance.SetDistance(this.enemySO.maxDistance);
        }
        if (this.enemyLevelUp != null)
        {
            this.enemyLevelUp.SetScale();
        }
    }
}
