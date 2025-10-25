using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class DamageReceiver : MyMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] private int health;
    [SerializeField] private int healthMax;
    public int HealthMax { get => healthMax; }
    [SerializeField] protected bool isDead = false;
    public int Health
    {
        get { return this.health; }
        set
        {
            this.health = Mathf.Clamp(value, 0, this.healthMax);
            if (this.health <= 0)
            {
                this.CheckDead();
            }
        }
    }
    public void Add(int add)
    {
        if (this.isDead) return;
        this.Health += add;
    }
    public void Remove(int remove)
    {
        if (this.isDead) return;
        this.Health -= remove;
    }
    public void CheckDead()
    {
        if (this.health <= 0)
        {
            this.OnDead();
        }
    }

    public void SetHPMax(int max)
    {
        int delta = max - this.healthMax;
        this.healthMax = max;
        this.Health += delta;
    }
    public void ResetHealth()
    {
        if (this.CanResetHealth())
        {
            this.Health = this.healthMax;
            this.isDead = false;
        }
        else
        {
            Debug.LogWarning($"Cannot reset health: healthMax is {healthMax} for {gameObject.name}");
        }
    }
    protected virtual void OnDead()
    {
        if (this.isDead) return; // Ngăn gọi lặp
        this.isDead = true;
    }
    public bool CanResetHealth() => this.healthMax > 0;
    void OnEnable()
    {
        this.ResetHealth();
    }
}

