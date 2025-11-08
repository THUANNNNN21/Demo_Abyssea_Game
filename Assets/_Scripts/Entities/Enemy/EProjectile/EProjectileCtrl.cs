using UnityEngine;

public class EProjectileCtrl : MyMonoBehaviour
{
    [SerializeField] private EProjectileImpact projectileImpact;
    public EProjectileImpact ProjectileImpact => projectileImpact;
    [SerializeField] private DamageSender damSender;
    public DamageSender DamSender => damSender;
    [SerializeField] private DespawnByDistance despawnByDistance;
    public DespawnByDistance DespawnByDistance => despawnByDistance;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    [SerializeField] private ProjectileSO projectileSO;
    public ProjectileSO ProjectileSO => projectileSO;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProjectileImpact();
        this.LoadDamageSender();
        this.LoadDespawnByDistance();
        this.LoadAnimator();
        this.LoadProjectileSO();
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
    private void LoadAnimator()
    {
        if (animator != null) return;
        animator = GetComponentInChildren<Animator>();
        Debug.LogWarning($"Load Animator in {gameObject.name}.");
    }
    private void LoadProjectileSO()
    {
        if (projectileSO != null) return;
        projectileSO = Resources.Load<ProjectileSO>("_SO/ProjectileSO/" + this.name);
        Debug.LogWarning($"Load ProjectileSO in {gameObject.name}.");
    }
    protected override void LoadValues()
    {
        base.LoadValues();

        if (MapManager.Instance == null || MapManager.Instance.MapLevel == null)
        {
            Debug.LogWarning($"MapManager or MapLevel not found for {gameObject.name}. Using default level 1.");
            LoadValuesWithLevel(1);
            return;
        }

        int mapLevel = MapManager.Instance.MapLevel.CurrentLevel;
        LoadValuesWithLevel(mapLevel);
    }

    private void LoadValuesWithLevel(int mapLevel)
    {
        if (damSender != null && projectileSO != null)
        {
            damSender.SetDamage(projectileSO.damage * Mathf.RoundToInt(1 + 0.2f * mapLevel));
        }
    }
}
