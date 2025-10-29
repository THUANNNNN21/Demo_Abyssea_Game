using UnityEngine;
using System;

public class LevelUp : MyMonoBehaviour
{
    [SerializeField] private LevelController levelController;
    public LevelController LevelController => levelController;
    [SerializeField] private int currentLevel = 1;
    public int CurrentLevel => currentLevel;
    [SerializeField] private int maxLevel = 10;
    public int MaxLevel => maxLevel;
    [SerializeField] private int currentExp = 0;
    public int CurrentExp => currentExp;
    [SerializeField] private int expToNextLevel = 100;
    public int ExpToNextLevel => expToNextLevel;
    [SerializeField] private float expGrow = 1.2f;
    public float ExpGrow => expGrow;
    public static event Action<int> OnLevelUp; // Event khi level up
    public static event Action<int, int> OnExpChanged; // Event khi EXP thay đổi
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
    public void AddExp(int exp)
    {
        currentExp += exp;
        OnExpChanged?.Invoke(currentExp, expToNextLevel);
        while (this.CanLevelUp())
        {
            this.LevelingUp();
        }
    }
    private bool CanLevelUp()
    {
        return currentExp >= expToNextLevel && currentLevel < maxLevel;
    }
    private void LevelingUp()
    {
        currentLevel++;
        this.ResetCurrentExp();
        this.PlayAniLevelUp();
        this.UpdateNextLevelExpRequirement();
        OnLevelUp?.Invoke(currentLevel);
    }
    private void ResetCurrentExp()
    {
        currentExp -= expToNextLevel;
    }
    private void PlayAniLevelUp()
    {
        this.levelController.LevelFX.SelfEnable();
    }
    private void UpdateNextLevelExpRequirement()
    {
        float newExpRequirement = this.expToNextLevel * ExpGrow;
        this.expToNextLevel = Mathf.RoundToInt(newExpRequirement);
    }
    [Header("Testing")]
    [SerializeField] private int addExp;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Test();
        }
    }
    private void Test()
    {
        AddExp(addExp);
    }
}
