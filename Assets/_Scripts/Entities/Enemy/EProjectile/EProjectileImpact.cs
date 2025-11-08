using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EProjectileImpact : MyMonoBehaviour
{
    [SerializeField] private EProjectileCtrl projectileCtrl;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProjectileCtrl();
        this.LoadRigidbody2D();
        this.LoadCollider2D();
    }
    private void LoadProjectileCtrl()
    {
        if (projectileCtrl != null) return;
        projectileCtrl = GetComponentInParent<EProjectileCtrl>();
        Debug.LogWarning($"Load EProjectileCtrl in {gameObject.name}.");
    }
    private void LoadRigidbody2D()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        Debug.LogWarning($"Load Rigidbody2D in {gameObject.name}.");
    }
    private void LoadCollider2D()
    {
        if (col != null) return;
        col = GetComponent<Collider2D>();
        Debug.LogWarning($"Load Collider2D in {gameObject.name}.");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Projectile hit the player!");
            projectileCtrl.DamSender.SendDamage(other.transform);
            projectileCtrl.Animator.SetTrigger("isDestroyed");
            SoundManager.Instance.PlaySound(SoundType.Explosion, 1f);
        }
    }
}
