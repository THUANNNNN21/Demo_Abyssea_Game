using System.Collections.Generic;
using UnityEngine;

public class Burn : Cooldown
{
    [SerializeField] private BurnController burnController;
    public BurnController BurnController { get => burnController; }
    [SerializeField] private CircleCollider2D circleCollider2D;
    public CircleCollider2D CircleCollider2D { get => circleCollider2D; }
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private List<Transform> enemiesInRange = new();
    [SerializeField] private int burnDamage = 5;
    [SerializeField] private float burningTime = 6f;
    private void LoadBurnController()
    {
        if (this.burnController == null)
        {
            this.burnController = GetComponentInParent<BurnController>();
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBurnController();
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
    protected override void LoadValues()
    {
        base.LoadValues();
        this.SetDelayTime(1f);
        this.SetRange(4f);
    }
    private void FixedUpdate()
    {
        this.Timing();
        if (this.isReady)
        {
            this.Burning();
        }

    }
    void OnEnable()
    {
        Invoke(nameof(this.DisableSelf), this.burningTime);
    }
    public void DisableSelf()
    {
        gameObject.SetActive(false);
        this.burnController.Model.SetActive(false);
    }
    //implement trigger to detect enemies in range range
    protected void OnTriggerEnter2D(Collider2D other)
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
    public void Burning()
    {
        if (enemiesInRange.Count == 0) return;

        foreach (Transform enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                this.SendDamage(enemy);
            }
        }
        this.ResetCooldown();
    }
    private void SendDamage(Transform enemy)
    {
        this.burnController.PlayerController.DamSender.SendDamage(enemy, this.burnDamage);
    }
    public void SetRange(float range)
    {
        this.CircleCollider2D.radius = range;
    }
}