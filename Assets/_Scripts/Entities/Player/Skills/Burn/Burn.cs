using System.Collections.Generic;
using UnityEngine;

public class Burn : MyMonoBehaviour
{
    [SerializeField] private BurnController burnController;
    public BurnController BurnController { get => burnController; }
    [SerializeField] private CircleCollider2D circleCollider2D;
    public CircleCollider2D CircleCollider2D { get => circleCollider2D; }
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private List<Transform> enemiesInRange = new();
    [Header("Burn Settings")]
    [SerializeField] private int burnDamage = 5;
    [SerializeField] private float burningTime = 6f; // Thời gian tổng của burn
    [SerializeField] private float tickRate = 1f; // Mỗi bao nhiêu giây đốt một lần

    private bool isBurning = false;
    private float burnTimer = 0f;
    private float tickTimer = 0f;

    private void LoadBurnController()
    {
        if (this.burnController != null) return;
        this.burnController = GetComponentInParent<BurnController>();
        Debug.LogWarning(this.gameObject.name + ": Load BurnController");
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
        Debug.LogWarning(this.gameObject.name + ": Load CircleCollider2D");
    }

    private void LoadRigidbody()
    {
        if (this.rb2D != null) return;
        this.rb2D = GetComponent<Rigidbody2D>();
        this.rb2D.bodyType = RigidbodyType2D.Kinematic;
        this.rb2D.gravityScale = 0;
        Debug.LogWarning(this.gameObject.name + ": Load Rigidbody2D");
    }

    protected override void LoadValues()
    {
        base.LoadValues();
        this.SetRange(4f);
    }

    private void FixedUpdate()
    {
        this.BurningProcess();
    }

    public void StartBurn()
    {
        isBurning = true;
        burnTimer = 0f;
        tickTimer = 0f;
        // Debug.Log($"Bắt đầu burn trong {burningTime} giây");
    }
    private void BurningProcess()
    {
        if (!isBurning) return;

        burnTimer += Time.fixedDeltaTime;
        tickTimer += Time.fixedDeltaTime;

        // Mỗi tickRate giây đốt một lần
        if (tickTimer >= tickRate)
        {
            this.Burning();
            tickTimer -= tickRate;
        }

        // Kết thúc burn khi hết thời gian
        if (burnTimer >= burningTime)
        {
            isBurning = false;
            burnTimer = 0f;
            tickTimer = 0f;
            this.StopBurn();
        }
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
    }
    private void SendDamage(Transform enemy)
    {
        this.BurnController.SkillController.PlayerController.DamSender.SendDamage(enemy, this.burnDamage);
        // Debug.Log($"Đốt {enemy.name} gây {burnDamage} sát thương");
    }
    public void SetRange(float range)
    {
        this.CircleCollider2D.radius = range;
    }
    private void StopBurn()
    {
        this.burnController.Fx.SetActive(false);
    }
}