using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageReceiver : MyMonoBehaviour
{
    #region Inspector Fields
    [Header("Damage Receiver")]
    [SerializeField] private int health;
    [SerializeField] private int healthMax;
    [SerializeField] protected bool isDead = false;
    #endregion

    #region Properties
    public int HealthMax => healthMax;
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
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        this.ResetHealth();
    }
    #endregion

    #region Public Methods
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

    public bool CanResetHealth() => this.healthMax > 0;
    #endregion

    #region Death Methods
    public void CheckDead()
    {
        if (this.health <= 0)
        {
            this.OnDead();
        }
    }

    protected virtual void OnDead()
    {
        if (this.isDead) return; // Prevent repeated calls
        this.isDead = true;
    }
    #endregion
}

