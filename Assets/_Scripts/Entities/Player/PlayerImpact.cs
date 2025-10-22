using System.Collections.Generic;
using UnityEngine;

public class PlayerImpact : Impact
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    [SerializeField] private List<Transform> enemiesInRange = new();

    // ✅ Animation state tracking
    private bool isAttacking = false;

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
        if (this.isReady && enemiesInRange.Count > 0 && !isAttacking)
        {
            isAttacking = true;
            this.playerController.Animator.SetTrigger("attack");
        }
    }
    public void CompleteAttack()
    {
        isAttacking = false;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform enemyTransform = other.transform;

            if (!enemiesInRange.Contains(enemyTransform))
            {
                enemiesInRange.Add(enemyTransform);
                Debug.Log($"Enemy entered range: {enemyTransform.name}");
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
                Debug.Log($"Enemy exited range: {enemyTransform.name}");
            }
        }
    }
    public void AttackAllEnemies()
    {
        if (enemiesInRange.Count == 0) return;

        Debug.Log($"Attacking {enemiesInRange.Count} enemies");

        foreach (Transform enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                this.SendDamage(enemy);
            }
        }

        this.ResetCooldown();
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
