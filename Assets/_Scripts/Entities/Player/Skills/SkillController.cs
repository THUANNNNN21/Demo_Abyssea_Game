using UnityEngine;

public class SkillController : MyMonoBehaviour
{
    #region Component References
    [Header("References Settings")]
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    [SerializeField] private PlayerShooting playerShooting;
    public PlayerShooting PlayerShooting { get => playerShooting; }

    [SerializeField] private Warp warp;
    public Warp Warp { get => warp; }

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
        this.warp = GetComponentInChildren<Warp>();
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
    #endregion
}
