using UnityEngine;

public class EProjectileCtrl : MyMonoBehaviour
{
    [SerializeField] private EProjectileImpact projectileImpact;
    public EProjectileImpact ProjectileImpact => projectileImpact;
    [SerializeField] private DamageSender damSender;
    public DamageSender DamSender => damSender;
    [SerializeField] private DespawnByDistance despawnByDistance;
    public DespawnByDistance DespawnByDistance => despawnByDistance;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProjectileImpact();
        this.LoadDamageSender();
        this.LoadDespawnByDistance();
    }
    private void LoadProjectileImpact()
    {
        if (projectileImpact != null) return;
        projectileImpact = GetComponentInChildren<EProjectileImpact>();
        Debug.LogWarning($"Load EProjectileImpact in {gameObject.name}.");
    }

    private void LoadDamageSender()
    {
        if (damSender != null) return;
        damSender = GetComponentInChildren<DamageSender>();
        Debug.LogWarning($"Load DamageSender in {gameObject.name}.");
    }
    private void LoadDespawnByDistance()
    {
        if (despawnByDistance != null) return;
        despawnByDistance = GetComponentInChildren<DespawnByDistance>();
        Debug.LogWarning($"Load DespawnByDistance in {gameObject.name}.");
    }
}
