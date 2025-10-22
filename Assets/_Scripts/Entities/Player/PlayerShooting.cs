using UnityEngine;

public class PlayerShooting : Shooting
{
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
    private void OnEnable()
    {
        InputManager.OnMouseClick += this.OnMouseClickReceived;
    }
    private void OnDisable()
    {
        InputManager.OnMouseClick -= this.OnMouseClickReceived;
    }
    void FixedUpdate()
    {
        this.Timing();
    }
    private void OnMouseClickReceived()
    {
        this.SpawnWithCooldown();
    }
    protected void SpawnWithCooldown()
    {
        if (!this.isReady) return;
        this.Spawn();
        this.ResetCooldown();
    }
}
