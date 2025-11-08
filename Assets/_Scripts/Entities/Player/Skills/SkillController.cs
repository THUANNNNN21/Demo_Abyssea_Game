using UnityEngine;

public class SkillController : MyMonoBehaviour
{
    #region Component References
    [Header("References Settings")]
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    [SerializeField] private PlayerShooting playerShooting;
    public PlayerShooting PlayerShooting { get => playerShooting; }

    [SerializeField] private PlayerWarp warp;
    public PlayerWarp Warp { get => warp; }

    [SerializeField] private BurnController burnController;
    public BurnController BurnController { get => burnController; }

    [SerializeField] private HealController healController;
    public HealController HealController { get => healController; }
    #endregion

    #region Initialization
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
        this.LoadPlayerShooting();
        this.LoadWarp();
        this.LoadBurnController();
        this.LoadHealController();
    }
    // protected override void LoadValues()
    // {
    //     int level = playerController.LevelController.LevelUp.CurrentLevel;
    //     this.playerShooting.SetDelayTime(10 - level);
    //     this.burnController.ActiveBurn.SetDelayTime(10 - level);
    //     this.healController.ActiveHeal.SetDelayTime(10 - level);
    //     this.warp.SetDelayTime(10 - level);
    // }
    #endregion

    #region Component Loading Methods
    private void LoadPlayerController()
    {
        if (this.playerController != null) return;
        this.playerController = GetComponentInParent<PlayerController>();
        Debug.LogWarning(this.gameObject.name + ": Load PlayerController");
    }
    private void LoadPlayerShooting()
    {
        if (this.playerShooting != null) return;
        this.playerShooting = GetComponentInChildren<PlayerShooting>();
        Debug.LogWarning(this.gameObject.name + ": Load PlayerShooting");
    }

    private void LoadWarp()
    {
        if (this.warp != null) return;
        this.warp = GetComponentInChildren<PlayerWarp>();
        Debug.LogWarning(this.gameObject.name + ": Load Warp");
    }

    private void LoadBurnController()
    {
        if (this.burnController != null) return;
        this.burnController = GetComponentInChildren<BurnController>();
        Debug.LogWarning(this.gameObject.name + ": Load BurnController");
    }

    private void LoadHealController()
    {
        if (this.healController != null) return;
        this.healController = GetComponentInChildren<HealController>();
        Debug.LogWarning(this.gameObject.name + ": Load HealController");
    }
    public void UpdateSkillDelays(int level)
    {
        float newShootingDelay = NewDelayTime(this.playerShooting.DelayTime, level);
        playerShooting.SetDelayTime(newShootingDelay);

        float newBurnDelay = NewDelayTime(this.burnController.ActiveBurn.DelayTime, level);
        burnController.ActiveBurn.SetDelayTime(newBurnDelay);

        float newHealDelay = NewDelayTime(this.healController.ActiveHeal.DelayTime, level);
        healController.ActiveHeal.SetDelayTime(newHealDelay);

        float newWarpDelay = NewDelayTime(this.warp.DelayTime, level);
        warp.SetDelayTime(newWarpDelay);
    }
    private float NewDelayTime(float baseDelay, int level)
    {
        float percentReducePerLevel = 0.05f; // Giảm 5% mỗi level
        float minPercent = 0.3f;             // Không thấp hơn 30% ban đầu

        float scale = Mathf.Max(1f - level * percentReducePerLevel, minPercent);
        float newDelay = Mathf.FloorToInt(baseDelay * scale);
        return newDelay;
    }
    #endregion
}
