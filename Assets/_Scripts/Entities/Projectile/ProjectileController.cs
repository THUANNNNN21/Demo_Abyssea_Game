using UnityEngine;

public class ProjectileController : MyMonoBehaviour
{
    [Header("References Settings")]
    [SerializeField] private ProjectileSO projectileSO;
    public ProjectileSO ProjectileSO { get => projectileSO; }
    [SerializeField] protected DamageSender damageSender;
    public DamageSender DamageSender { get => damageSender; }
    [SerializeField] private DespawnByDistance despawnByDistance;
    public DespawnByDistance DespawnByDistance { get => despawnByDistance; }
    [SerializeField] private ProjectileMovement projectileMovement;
    public ProjectileMovement ProjectileMovement { get => projectileMovement; }
    [SerializeField] Animator animator;
    public Animator Animator { get => animator; }
    private void LoadDamageSender()
    {
        if (this.damageSender == null)
        {
            this.damageSender = this.GetComponentInChildren<DamageSender>();
        }
    }
    private void LoadProjectileSO()
    {
        if (this.projectileSO == null)
        {
            this.projectileSO = Resources.Load<ProjectileSO>("_SO/ProjectileSO/" + this.name);
        }
    }
    private void LoadDespawnByDistance()
    {
        if (this.despawnByDistance == null)
        {
            this.despawnByDistance = this.GetComponent<DespawnByDistance>();
        }
    }
    private void LoadProjectileTrajectory()
    {
        if (this.projectileMovement == null)
        {
            this.projectileMovement = this.GetComponentInChildren<ProjectileMovement>();
        }
    }
    private void LoadAnimator()
    {
        if (this.animator == null)
        {
            this.animator = this.GetComponentInChildren<Animator>();
        }
    }
    protected override void LoadComponents()
    {
        this.LoadDamageSender();
        this.LoadProjectileSO();
        this.LoadDespawnByDistance();
        this.LoadProjectileTrajectory();
        this.LoadAnimator();
    }
    protected override void LoadValues()
    {
        this.damageSender.SetDamage(this.projectileSO.damage);
        this.despawnByDistance.SetDistance(this.projectileSO.distance);
        this.projectileMovement.SetSpeed(this.projectileSO.speed);
    }
}
