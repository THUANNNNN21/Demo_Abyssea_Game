using System;
using UnityEngine;

public class LevelUpBonus : MyMonoBehaviour
{
    [SerializeField] private LevelController levelController;
    public LevelController LevelController => levelController;
    [SerializeField] private int bonusPointsPerLevel = 5;
    public int BonusPointsPerLevel => bonusPointsPerLevel;
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
        int totalBonusPoints = bonusPointsPerLevel + currentLevel;

        this.BonusDamage(totalBonusPoints);
        this.BonusMaxHealth(totalBonusPoints);
        this.BonusMoveSpeed(totalBonusPoints);
        this.BonusInventorySize();
        this.SizeGrowth(currentLevel);
        Debug.Log($"LevelUpBonus: Applied {totalBonusPoints} bonus points for Level {currentLevel}");
    }

    private void BonusDamage(int bonusPoints)
    {
        int newDamage = LevelController.PlayerController.DamSender.Damage + bonusPoints;
        this.LevelController.PlayerController.DamSender.SetDamage(newDamage);
    }

    private void BonusMaxHealth(int bonusPoints)
    {
        int newMaxHealth = LevelController.PlayerController.PlayerDamReceiver.HealthMax + bonusPoints * 10;
        this.LevelController.PlayerController.PlayerDamReceiver.SetHPMax(newMaxHealth);
    }

    private void BonusMoveSpeed(int bonusPoints)
    {
        float newMoveSpeed = LevelController.PlayerController.PlayerMovement.Speed + bonusPoints * 0.05f;
        this.LevelController.PlayerController.PlayerMovement.SetSpeed(newMoveSpeed);
    }

    private void BonusInventorySize()
    {
        int newInventorySize = LevelController.PlayerController.InventoryController.MaxSlot + 1;
        this.LevelController.PlayerController.InventoryController.SetMaxSlot(newInventorySize);
    }

    private void SizeGrowth(int currentLevel)
    {
        float bonusPoints = currentLevel * 0.1f + 1f;

        // Nhân scale hiện tại với multiplier
        Vector3 newScale = new(bonusPoints, bonusPoints, 1f);

        this.LevelController.PlayerController.transform.localScale = newScale;
    }

    [Header("Testing")]
    [SerializeField] private int testLevel = 2;

    [ContextMenu("Test Bonus")]
    public void TestBonus()
    {
        this.BonusPoint(testLevel);
        Debug.Log($"Applied bonus for Level {testLevel}: {bonusPointsPerLevel + testLevel} total points");
    }
}