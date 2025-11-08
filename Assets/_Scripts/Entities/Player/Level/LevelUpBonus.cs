using System;
using UnityEngine;

public class LevelUpBonus : MyMonoBehaviour
{
    [SerializeField] private LevelController levelController;
    public LevelController LevelController => levelController;
    void OnEnable()
    {
        LevelUp.OnLevelUp += BonusPoint;
    }
    void OnDisable()
    {
        LevelUp.OnLevelUp -= BonusPoint;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevelController();
    }

    protected virtual void LoadLevelController()
    {
        if (levelController != null) return;
        levelController = this.GetComponentInParent<LevelController>();
        Debug.LogWarning($"LevelUpBonus: LoadLevelController in {this.name} ");
    }

    protected void BonusPoint(int currentLevel)
    {
        this.BonusDamage(currentLevel);
        this.BonusMaxHealth(currentLevel);
        this.BonusMoveSpeed(currentLevel);
        this.BonusInventorySize(currentLevel);
        this.SizeGrowth(currentLevel);
        LevelController.PlayerController.SkillController.UpdateSkillDelays(currentLevel);
    }

    private void BonusDamage(int currentLevel)
    {
        float damageMultiplier = 1f + 0.05f * currentLevel;
        int baseDamage = LevelController.PlayerController.DamSender.Damage;
        int newDamage = Mathf.RoundToInt(baseDamage * damageMultiplier);
        this.LevelController.PlayerController.DamSender.SetDamage(newDamage);
    }

    private void BonusMaxHealth(int currentLevel)
    {
        float healthMultiplier = 1f + 0.08f * currentLevel;
        int baseHealth = LevelController.PlayerController.PlayerDamReceiver.HealthMax;
        int newMaxHealth = Mathf.RoundToInt(baseHealth * healthMultiplier);
        this.LevelController.PlayerController.PlayerDamReceiver.SetHPMax(newMaxHealth);
    }

    private void BonusMoveSpeed(int currentLevel)
    {
        float speedMultiplier = 1f + 0.02f * currentLevel;
        float newMoveSpeed = LevelController.PlayerController.PlayerMovement.Speed * speedMultiplier;
        this.LevelController.PlayerController.PlayerMovement.SetSpeed(newMoveSpeed);
    }

    private void BonusInventorySize(int currentLevel)
    {
        int inventoryBonus = currentLevel / 3;
        int baseInventorySize = LevelController.PlayerController.InventoryController.Inventory.MaxSlot;
        int newInventorySize = baseInventorySize + inventoryBonus;
        this.LevelController.PlayerController.InventoryController.Inventory.SetMaxSlot(newInventorySize);
    }

    private void SizeGrowth(int currentLevel)
    {
        float scaleMultiplier = 1f + 0.02f * currentLevel;
        Vector3 newScale = Vector3.one * scaleMultiplier;
        this.LevelController.PlayerController.transform.localScale = newScale;
    }
}