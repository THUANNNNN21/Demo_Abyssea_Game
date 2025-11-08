using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamReceiver : DamageReceiver
{
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController { get => enemyController; }

    private void LoadController()
    {
        if (this.enemyController != null)
        {
            return;
        }
        else
        {
            this.enemyController = GetComponentInParent<EnemyController>();
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadController();
    }

    protected override void OnDead()
    {
        if (this.isDead) return;
        base.OnDead();
        this.AddAmount();
        enemyController.Animator.SetTrigger("isDestroyed");
    }
    private void AddAmount()
    {
        PlayerController.Instance.LevelController.LevelUp.AddExp(enemyController.EnemySO.expReward);
        GameManager.Instance.AddScore(enemyController.EnemySO.scoreReward);
        GameManager.Instance.AddTime(enemyController.EnemySO.timeReward);
    }
    public void DropItems()
    {
        if (ItemSpawner.Instance != null)
        {
            ItemSpawner.Instance.Drop(this.GetDropList(), this.gameObject);
        }
    }
    public List<ItemDropRate> GetDropList()
    {
        return enemyController.EnemySO.dropList;
    }
    private void OnEnable()
    {
        this.ResetHealth();
    }
}
