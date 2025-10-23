using System.Collections.Generic;
using UnityEngine;

public class Burn : Cooldown
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    [SerializeField] private CircleCollider2D circleCollider2D;
    public CircleCollider2D CircleCollider2D { get => circleCollider2D; }
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private List<Transform> enemiesInRange = new();
    private bool isBurning = false;
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
        this.LoadCollider();
        this.LoadRigidbody();
    }
    private void LoadCollider()
    {
        if (this.circleCollider2D != null) return;
        this.circleCollider2D = GetComponent<CircleCollider2D>();
    }
    private void LoadRigidbody()
    {
        if (this.rb2D == null)
        {
            this.rb2D = GetComponent<Rigidbody2D>();
        }
        this.rb2D.bodyType = RigidbodyType2D.Kinematic; // Không cần vật lý, chỉ cần trigger
        this.rb2D.gravityScale = 0; // Không cần trọng lực
    }
    private void FixedUpdate()
    {
        this.Timing();
        this.Burning();
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Transform enemyTransform = other.transform;

            if (!enemiesInRange.Contains(enemyTransform))
            {
                enemiesInRange.Add(enemyTransform);
                Debug.Log($"Enemy entered burn range: {enemyTransform.name}");
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
                Debug.Log($"Enemy exited burn range: {enemyTransform.name}");
            }
        }
    }
    public void StartBurning()
    {
        if (this.isReady && enemiesInRange.Count > 0 && !isBurning)
        {
            isBurning = true;
            this.playerController.Animator.SetTrigger("burn");
        }
    }
    public void CompleteBurning()
    {
        isBurning = false;
    }
    public void Burning()
    {
        if (enemiesInRange.Count == 0) return;

        Debug.Log($"Attacking {enemiesInRange.Count} enemies");

        foreach (Transform enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                this.playerController.DamSender.SendDamage(enemy);
            }
        }
        this.ResetCooldown();
    }
}