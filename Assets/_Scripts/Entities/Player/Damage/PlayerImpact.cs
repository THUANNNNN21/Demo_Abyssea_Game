using System.Collections.Generic;
using UnityEngine;

public class PlayerImpact : Impact
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    [SerializeField] private List<Transform> enemiesInRange = new();


    private void LoadPlayerController()
    {
        if (this.playerController == null)
        {
            this.playerController = GetComponentInParent<PlayerController>();
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
    }

    void FixedUpdate()
    {
        this.Timing();
        this.StartAttack();
        this.CleanupNullEnemies();
    }
    private void StartAttack()
    {
        if (this.isReady && enemiesInRange.Count > 0)
        {
            this.playerController.Animator.SetBool("attack", true);
            this.ResetCooldown();
        }
    }
    public void CompleteAttack()
    {
        this.playerController.Animator.SetBool("attack", false);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform enemyTransform = other.transform;

            if (!enemiesInRange.Contains(enemyTransform))
            {
                enemiesInRange.Add(enemyTransform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform enemyTransform = other.transform;

            if (enemiesInRange.Contains(enemyTransform))
            {
                enemiesInRange.Remove(enemyTransform);
            }
        }
    }
    public void AttackAllEnemies()
    {
        if (enemiesInRange.Count == 0) return;

        foreach (Transform enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                this.SendDamage(enemy);
                // Debug.Log("PlayerImpact: Attacked " + enemy.name);
            }
        }
    }

    private void SendDamage(Transform target)
    {
        if (this.playerController.DamSender != null)
        {
            this.playerController.DamSender.SendDamage(target);
        }
    }

    private void CleanupNullEnemies()
    {
        enemiesInRange.RemoveAll(enemy => enemy == null);
    }
}
