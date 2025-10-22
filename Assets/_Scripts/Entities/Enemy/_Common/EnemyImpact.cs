using UnityEngine;

public class EnemyImpact : Impact
{
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController { get => enemyController; }
    private void LoadMeteoriteController()
    {
        if (this.enemyController == null)
        {
            this.enemyController = GetComponentInParent<EnemyController>();
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMeteoriteController();
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.SendDamage(other.transform);
            if (this.EnemyController.EnemySO.isDestroyWhenImpact)
            {
                this.DestroyWhenImpact();
            }
            EnemyController.EnemyDamReceiver.CheckDead();
        }
    }
    private void SendDamage(Transform target)
    {
        if (this.enemyController.DamageSender != null)
        {
            this.enemyController.DamageSender.SendDamage(target);
        }
    }
    protected virtual void DestroyWhenImpact()
    {
        this.EnemyController.EnemyDamReceiver.Health = 0;
    }
}
