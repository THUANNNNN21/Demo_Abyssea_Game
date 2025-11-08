using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamReceiver : DamageReceiver
{
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController { get => enemyController; }
    private int scoreReward;
    private int expReward;
    private float timeReward;

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
        SoundManager.Instance.PlaySound(SoundType.EnemyDeath, 0.5f);
    }
    private void AddAmount()
    {
        PlayerController.Instance.LevelController.LevelUp.AddExp(expReward);
        GameManager.Instance.AddScore(scoreReward);
        GameManager.Instance.AddTime(timeReward);
    }
    public void DropItems()
    {
        if (ItemSpawner.Instance != null || this.GetDropList().Count > 0)
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
    public void SetRewards(int exp, int score, float time)
    {
        this.expReward = exp;
        this.scoreReward = score;
        this.timeReward = time;
    }
}
