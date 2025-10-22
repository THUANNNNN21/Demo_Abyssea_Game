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
        this.enemyController.ObjAppearing.FullyAppeared();
        Debug.Log("Appear Animation Complete");
        HPBarSpawner.Instance.SpawnHPBar(this.enemyController);
    }
    public void OnShootAnimationComplete()
    {
        for (int i = 0; i < this.enemyController.EnemyShooting.Count; i++)
        {
            var shooting = this.enemyController.EnemyShooting[i];
            shooting.TriggerOnStopShooting();
        }
    }
}
