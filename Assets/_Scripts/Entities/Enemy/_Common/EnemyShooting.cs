using System;
using UnityEngine;
public class EnemyShooting : Shooting, IShootingObservable
{
    [Header("Enemy Shooting Components")]
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController => this.enemyController;
    [SerializeField] private AppearanceStateTracker shootAfterAppearing;
    public AppearanceStateTracker ShootAfterAppearing => this.shootAfterAppearing;
    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private bool isStoppingShooting = false;
    [SerializeField] float distanceToPlayerForShoot = 20f;
    public event Action OnShooting;
    public event Action OnShootComplete;
    // [SerializeField] protected List<Transform> minions;
    // [SerializeField] protected int prefabsCanExist = 10;
    // public event Action OnDestroyed;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyController();
        this.LoadSpawner();
        this.LoadSkillSO();
        this.SetPrefabToSpawn();
        this.LoadPrefab();
        this.LoadShootAfterAppearing();
        this.LoadAnimator();
    }
    private void LoadShootAfterAppearing()
    {
        this.shootAfterAppearing = this.enemyController.AfterAppear;
    }
    private void LoadEnemyController()
    {
        if (this.enemyController != null) return;
        else
        {
            this.enemyController = GetComponentInParent<EnemyController>();
        }
    }
    private void LoadSpawner()
    {
        if (this.Spawnner == null)
        {
            this.Spawnner = GameObject.Find("EnemySpawner");
        }
    }
    private void LoadSkillSO()
    {
        if (this.skillSO == null)
        {
            this.skillSO = Resources.Load<SkillSO>("_SO/SkillSO/" + this.name);
        }
    }
    private void LoadPrefab()
    {
        if (this.prefabToSpawn == null)
        {
            this.prefabToSpawn = Resources.Load<GameObject>("_Prefab/Enemy/" + this.prefabName);
        }
    }
    private void LoadAnimator()
    {
        if (this.animator == null)
        {
            this.animator = this.enemyController.Animator;
        }
    }
    protected override void LoadValues()
    {
        base.LoadValues();
        this.SetDelayTime(skillSO.cooldownTime);
    }
    void FixedUpdate()
    {
        this.Shoot();
    }
    private void Shoot()
    {
        if (this.CanShoot())
        {
            this.Timing();
            this.SpawnWithCooldown();
        }
    }
    protected virtual void SpawnWithCooldown()
    {
        if (this.isReady && enemyController.EnemyCheckPlayer.CheckNearPlayer(this.distanceToPlayerForShoot))
        {
            if (isStoppingShooting)
            {
                this.TriggerOnShooting();
            }
            this.PlayShootAnimation();
            GameObject enemy = this.SpawnAndReturn();
            this.ResetCooldown();
        }
    }
    private void PlayShootAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("shoot");
        }
    }
    protected virtual void TriggerOnShooting()
    {
        OnShooting?.Invoke();
    }
    public virtual void TriggerOnShootComplete()
    {
        OnShootComplete?.Invoke();
    }
    private bool CanShoot()
    {
        if (this.shootAfterAppearing != null)
        {
            return this.shootAfterAppearing.HasAppeared;
        }
        return true;
    }
}
