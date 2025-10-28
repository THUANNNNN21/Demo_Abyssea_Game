using System.Collections.Generic;
using UnityEngine;

public class EnemyImpact : Impact
{
    #region Header - Fields and Properties
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController => enemyController;
    [SerializeField] private List<Transform> playersInRange = new();
    #endregion

    #region Header - Unity Methods
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyController();
    }

    void FixedUpdate()
    {
        Timing();
        StartAttack();
        CleanupNullPlayers();
    }
    #endregion

    #region Header - Enemy Controller
    private void LoadEnemyController()
    {
        if (enemyController == null)
        {
            enemyController = GetComponentInParent<EnemyController>();
        }
    }
    #endregion

    #region Header - Attack Logic
    private void StartAttack()
    {
        if (isReady && playersInRange.Count > 0)
        {
            enemyController.Animator.SetBool("attack", true);
            ResetCooldown();
        }
    }

    public void CompleteAttack()
    {
        enemyController.Animator.SetBool("attack", false);
    }

    public void AttackAllPlayers()
    {
        if (playersInRange.Count == 0) return;

        foreach (Transform player in playersInRange)
        {
            if (player != null)
            {
                SendDamage(player);
                if (EnemyController.EnemySO.isDestroyWhenImpact)
                {
                    DestroyWhenImpact();
                }
                EnemyController.EnemyDamReceiver.CheckDead();
            }
        }
    }

    private void SendDamage(Transform target)
    {
        if (enemyController.DamageSender != null)
        {
            enemyController.DamageSender.SendDamage(target);
        }
    }

    protected virtual void DestroyWhenImpact()
    {
        EnemyController.EnemyDamReceiver.Health = 0;
    }
    #endregion

    #region Header - Trigger Events
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Transform playerTransform = other.transform;

            if (!playersInRange.Contains(playerTransform))
            {
                playersInRange.Add(playerTransform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Transform playerTransform = other.transform;

            if (playersInRange.Contains(playerTransform))
            {
                playersInRange.Remove(playerTransform);
            }
        }
    }
    #endregion

    #region Header - Utilities
    private void CleanupNullPlayers()
    {
        playersInRange.RemoveAll(player => player == null);
    }
    #endregion
}
