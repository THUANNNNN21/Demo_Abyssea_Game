using UnityEngine;

public class EnemyDamSender : DamageSender
{
    [Header("Meteorite Damage Sender")]
    [SerializeField] private EnemyController enemyController;

    private void LoadController()
    {
        if (this.enemyController == null)
        {
            this.enemyController = this.GetComponentInParent<EnemyController>();
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadController();
    }
}