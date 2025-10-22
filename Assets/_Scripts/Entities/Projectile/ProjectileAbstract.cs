using UnityEngine;

public abstract class ProjectileAbstract : MyMonoBehaviour
{
    [Header("Projectile Abstract")]
    [SerializeField] protected ProjectileController projectileCtlr;
    public ProjectileController ProjectileCtlr { get => projectileCtlr; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProjectileController();
    }

    protected virtual void LoadProjectileController()
    {
        if (this.projectileCtlr != null) return;
        this.projectileCtlr = transform.parent.GetComponent<ProjectileController>();
    }
}