using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MyMonoBehaviour
{
    [Header("References Components")]
    [SerializeField] private Transform model;
    public Transform Model { get => model; }

    [SerializeField] private EnemyMovement movement;
    public EnemyMovement Movement { get => movement; }

    [SerializeField] private EnemyDamSender damageSender;
    public EnemyDamSender DamageSender { get => damageSender; }

    [SerializeField] private EnemyDamReceiver enemyDamReceiver;
    public EnemyDamReceiver EnemyDamReceiver { get => enemyDamReceiver; }
    [SerializeField] private EnemyAbility enemyAbility;
    public EnemyAbility EnemyAbility { get => enemyAbility; }

    [SerializeField] private DespawnByDistance despawnByDistance;
    public DespawnByDistance DespawnByDistance { get => despawnByDistance; }
    [SerializeField] private EnemySO enemySO;
    public EnemySO EnemySO { get => enemySO; }
    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; }
    [SerializeField] private EnemyLevelUp enemyLevelUp;
    public EnemyLevelUp EnemyLevelUp { get => enemyLevelUp; }
    [SerializeField] private EnemyCheckPlayer enemyCheckPlayer;
    public EnemyCheckPlayer EnemyCheckPlayer { get => enemyCheckPlayer; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadDamageSender();
        this.LoadDamageReceiver();
        this.LoadEnemyAbility();
        this.LoadSO();
        this.LoadEnemyCheckPlayer();
        this.LoadMovement();
        this.LoadDespawnByDistance();
        this.LoadAnimator();
        this.LoadEnemyLevelUp();
    }
    private void LoadEnemyAbility()
    {
        if (this.enemyAbility != null) return;
        else
        {
            this.enemyAbility = this.GetComponentInChildren<EnemyAbility>();
            Debug.LogWarning($"LoadEnemyAbility: {this.gameObject.name}");
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
            this.movement = this.GetComponentInChildren<EnemyMovement>();
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
    private void LoadEnemyLevelUp()
    {
        if (this.enemyLevelUp == null)
        {
            this.enemyLevelUp = this.GetComponentInChildren<EnemyLevelUp>();
        }
    }
    private void LoadEnemyCheckPlayer()
    {
        if (this.enemyCheckPlayer != null) return;
        else
        {
            this.enemyCheckPlayer = this.GetComponentInChildren<EnemyCheckPlayer>();
            Debug.LogWarning($"LoadEnemyCheckPlayer: {this.gameObject.name}");
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
            this.movement.SetTargetRadius(this.enemySO.targetRadius);
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
