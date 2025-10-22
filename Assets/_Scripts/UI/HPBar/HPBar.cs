using UnityEngine;
using System;

[RequireComponent(typeof(DespawnObject))]
[RequireComponent(typeof(UIFollowTarget))]
public class HPBar : MyMonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController => enemyController;
    [SerializeField] private UIFollowTarget uiFollowTarget;
    public UIFollowTarget UIFollowTarget => uiFollowTarget;
    [SerializeField] private HPSlider hpSlider;
    public HPSlider HPSlider => hpSlider;
    [SerializeField] private DespawnObject despawnObject;
    public DespawnObject DespawnObject => despawnObject;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHPSlider();
        this.LoadUIFollowTarget();
        this.LoadDespawnObject();
    }
    private void LoadHPSlider()
    {
        if (this.hpSlider != null) return;
        this.hpSlider = GetComponentInChildren<HPSlider>();
        Debug.LogWarning($"Load HPSlider: {this.hpSlider.gameObject.name}", gameObject);
    }
    private void LoadUIFollowTarget()
    {
        if (this.uiFollowTarget != null) return;
        this.uiFollowTarget = GetComponent<UIFollowTarget>();
        Debug.LogWarning($"Load UIFollowTarget: {this.uiFollowTarget.gameObject.name}", gameObject);
    }
    private void LoadDespawnObject()
    {
        if (this.despawnObject != null) return;
        this.despawnObject = GetComponent<DespawnObject>();
        Debug.LogWarning($"Load DespawnObject: {this.despawnObject.gameObject.name}", gameObject);
    }

    private void OnEnable()
    {
        EnemyAnimmationEvent.OnEnemyDeath += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        EnemyAnimmationEvent.OnEnemyDeath -= HandleEnemyDeath;
    }
    private void HandleEnemyDeath(EnemyController deadEnemy)
    {
        if (deadEnemy != this.enemyController) return;
        this.ResetHPBar();
    }

    private void FixedUpdate()
    {
        this.HPShowing();
    }
    public void HPShowing()
    {
        int hp = enemyController.EnemyDamReceiver.Health;
        int maxHp = enemyController.EnemyDamReceiver.HealthMax;
        this.hpSlider.SetHP(hp);
        this.hpSlider.SetHPMax(maxHp);
    }
    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
        this.GetTarget();
    }
    private void GetTarget()
    {
        this.uiFollowTarget.SetTarget(this.enemyController.transform);
    }
    public void ResetHPBar()
    {
        this.hpSlider.SetHP(this.enemyController.EnemyDamReceiver.HealthMax);
        this.despawnObject.HandleDespawn();
    }

}
