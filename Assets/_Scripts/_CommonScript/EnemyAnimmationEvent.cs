using UnityEngine;
using System;

public class EnemyAnimmationEvent : MyMonoBehaviour
{
    public static event Action<EnemyController> OnEnemyDeath;
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController { get => enemyController; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyController();
    }
    private void LoadEnemyController()
    {
        if (this.enemyController != null) return;
        else
        {
            this.enemyController = this.GetComponentInParent<EnemyController>();
        }
    }
    public void OnDeathAnimationComplete()
    {
        this.enemyController.DespawnByDistance.HandleDespawn();
        this.enemyController.EnemyDamReceiver.DropItems();
        OnEnemyDeath?.Invoke(this.enemyController);
    }
    public void OnAppearAnimationComplete()
    {
        this.enemyController.EnemyAbility.ObjAppearing.FullyAppeared();
    }
    public void OnShootAnimationComplete()
    {
        var shooting = this.enemyController.EnemyAbility.EnemyShooting;
        shooting.TriggerOnShootComplete();
    }
    public void OnAttackAnimationPlay()
    {
        this.enemyController.EnemyAbility.EnemyImpact.AttackAllPlayers();
    }
    public void OnAttackAnimationComplete()
    {
        this.enemyController.EnemyAbility.EnemyImpact.CompleteAttack();
    }
    public void OnWarpAnimationComplete()
    {
        this.enemyController.EnemyAbility.EnemyWarp.WarpFinish();
    }

    public void OnWarpAnimationPlay()
    {
        this.enemyController.EnemyAbility.EnemyWarp.Warping();
    }
}
