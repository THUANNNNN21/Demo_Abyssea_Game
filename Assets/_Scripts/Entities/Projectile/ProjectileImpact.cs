using UnityEngine;
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileImpact : Impact
{
    [Header("Projectile")]
    [SerializeField] private ProjectileController projectileController;
    public ProjectileController ProjectileController { get => projectileController; }

    private void LoadProjectileController()
    {
        if (this.projectileController == null)
            this.projectileController = GetComponentInParent<ProjectileController>();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProjectileController();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        this.projectileController.DamageSender.SendDamage(other.transform);
        this.projectileController.Animator.SetTrigger("isDestroyed");
    }
}
