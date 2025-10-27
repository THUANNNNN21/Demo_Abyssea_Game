using System;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShooting : Shooting, IShootingObservable
{
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController => this.enemyController;
    [SerializeField] private AppearanceStateTracker shootAfterAppearing;
    public AppearanceStateTracker ShootAfterAppearing => this.shootAfterAppearing;
    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private bool isStoppingShooting = false;
    public bool IsStoppingShooting { get => isStoppingShooting; set => isStoppingShooting = value; }
    public event Action OnShooting;
    public event Action OnStopShooting;
    [SerializeField] protected List<Transform> minions;
    [SerializeField] protected int prefabsCanExist = 10;
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
        this.ClearMinion();
        this.Shoot();
    }
    private void ClearMinion()
    {
        for (int i = minions.Count - 1; i >= 0; i--)
        {
            if (minions[i] == null || !minions[i].gameObject.activeSelf)
            {
                minions.RemoveAt(i);
                // Debug.Log($"Removed inactive minion at index {i}");
            }
        }
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
        if (this.isReady && this.minions.Count <= this.prefabsCanExist)
        {
            if (isStoppingShooting)
            {
                this.TriggerOnShooting();
            }
            this.PlayShootAnimation();
            GameObject enemy = this.SpawnAndReturn();
            minions.Add(enemy.transform);
            this.ResetCooldown();
        }
    }
    private void PlayShootAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }
    }
    protected virtual void TriggerOnShooting()
    {
        OnShooting?.Invoke();
    }
    public virtual void TriggerOnStopShooting()
    {
        OnStopShooting?.Invoke();
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
