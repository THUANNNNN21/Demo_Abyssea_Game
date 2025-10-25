using UnityEngine;

public class LevelController : MyMonoBehaviour
{
    [SerializeField] PlayerController playerController;
    public PlayerController PlayerController => playerController;
    [SerializeField] private LevelUp levelUp;
    public LevelUp LevelUp => levelUp;
    [SerializeField] private LevelUpBonus levelUpBonus;
    public LevelUpBonus LevelUpBonus => levelUpBonus;
    [SerializeField] LevelFX levelFX;
    public LevelFX LevelFX => levelFX;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
        this.LoadLevelUp();
        this.LoadLevelUpBonus();
        this.LoadLevelFX();
    }
    protected virtual void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = this.GetComponentInParent<PlayerController>();
        Debug.LogWarning($"LevelController: LoadPlayerController in {this.name} ");
    }
    protected virtual void LoadLevelUp()
    {
        if (levelUp != null) return;
        levelUp = this.GetComponentInChildren<LevelUp>();
        Debug.LogWarning($"LevelController: LoadLevelUp in {this.name} ");
    }
    protected virtual void LoadLevelUpBonus()
    {
        if (levelUpBonus != null) return;
        levelUpBonus = this.GetComponentInChildren<LevelUpBonus>();
        Debug.LogWarning($"LevelController: LoadLevelUpBonus in {this.name} ");
    }
    protected virtual void LoadLevelFX()
    {
        if (levelFX != null) return;
        levelFX = this.GetComponentInChildren<LevelFX>();
        Debug.LogWarning($"LevelController: LoadLevelFX in {this.name} ");
        this.levelFX.SelfDisable();
    }
}
