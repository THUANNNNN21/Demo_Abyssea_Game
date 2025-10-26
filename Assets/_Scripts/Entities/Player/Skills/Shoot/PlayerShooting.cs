using UnityEngine;

public class PlayerShooting : Shooting
{
    // [Header("Skill Settings")]
    // [SerializeField] private int assignedSkillIndex = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        this.LoadSkillSO();
        this.LoadPrefab();
    }
    private void LoadSpawner()
    {
        if (this.Spawnner == null)
        {
            this.Spawnner = GameObject.Find("ProjectileContainer");
        }
    }
    private void LoadSkillSO()
    {
        if (this.skillSO == null)
        {
            this.skillSO = Resources.Load<SkillSO>("_SO/SkillSO/" + this.name);
        }
    }
    private void LoadPrefab()
    {
        if (this.prefabToSpawn == null)
        {
            this.prefabToSpawn = Resources.Load<GameObject>("_Prefab/Projectile/Rocket");
        }
    }
    // private void OnEnable()
    // {
    //     InputManager.OnItemCast += this.OnMouseClickReceived;
    // }
    // private void OnDisable()
    // {
    //     InputManager.OnItemCast -= this.OnMouseClickReceived;
    // }
    void FixedUpdate()
    {
        this.Timing();
    }
    // private void OnMouseClickReceived(int skillIndex)
    // {
    //     // ✅ Only shoot if this is the correct skill
    //     if (skillIndex == assignedSkillIndex)
    //     {
    //         Debug.Log($"PlayerShooting: Casting skill {skillIndex + 1}");
    //         this.SpawnWithCooldown();
    //     }
    //     else
    //     {
    //         Debug.Log($"PlayerShooting: Ignoring skill {skillIndex + 1} (not assigned to this shooter)");
    //     }
    // }
    public void SpawnWithCooldown()
    {
        if (!this.isReady) return;
        this.Spawn();
        this.ResetCooldown();
    }
}
