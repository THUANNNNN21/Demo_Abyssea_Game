using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MyMonoBehaviour
{
    #region Singleton Pattern
    private static PlayerController instance;
    public static PlayerController Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    #region Component References
    [Header("References Settings")]
    [SerializeField] private PlayerSO playerSO;
    public PlayerSO PlayerSO { get => playerSO; }

    [SerializeField] private PlayerDamReceiver playerDamReceiver;
    public PlayerDamReceiver PlayerDamReceiver { get => playerDamReceiver; }

    [SerializeField] private PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement { get => playerMovement; }

    [SerializeField] private PlayerShooting playerShooting;
    public PlayerShooting PlayerShooting { get => playerShooting; }

    [SerializeField] private PlayerImpact playerImpact;
    public PlayerImpact PlayerImpact { get => playerImpact; }

    [SerializeField] private InventoryController inventoryController;
    public InventoryController InventoryController { get => inventoryController; }

    [SerializeField] private DamageSender damSender;
    public DamageSender DamSender { get => damSender; }

    [SerializeField] private Warp warp;
    public Warp Warp { get => warp; }

    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; }
    [SerializeField] private BurnController burnController;
    public BurnController BurnController { get => burnController; }
    #endregion

    #region Initialization
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSO();
        this.LoadPlayerDamReceiver();
        this.LoadPlayerMovement();
        this.LoadPlayerShooting();
        this.LoadPlayerImpact();
        this.LoadInventoryController();
        this.LoadDamSender();
        this.LoadWarp();
        this.LoadBurnController();
        this.LoadAnimator();
    }

    protected override void LoadValues()
    {
        this.playerDamReceiver.SetHPMax(playerSO.maxHP);
        this.playerMovement.SetSpeed(playerSO.speed);
        this.inventoryController.SetMaxSlot(playerSO.inventorySize);
        this.damSender.SetDamage(playerSO.damage);
        this.playerImpact.SetImpactRange(playerSO.attackRange);
        this.playerImpact.SetDelayTime(playerSO.attackDelay);
        this.inventoryController.ItemLooter.SetLootRange(playerSO.lootRange);
    }
    #endregion

    #region Component Loading Methods
    private void LoadPlayerSO()
    {
        if (this.playerSO == null)
        {
            this.playerSO = Resources.Load<PlayerSO>("_SO/PlayerSO/" + this.name);
        }
    }

    private void LoadPlayerDamReceiver()
    {
        if (this.playerDamReceiver == null)
        {
            this.playerDamReceiver = GetComponentInChildren<PlayerDamReceiver>();
        }
    }

    private void LoadPlayerMovement()
    {
        if (this.playerMovement == null)
        {
            this.playerMovement = GetComponentInChildren<PlayerMovement>();
        }
    }

    private void LoadPlayerShooting()
    {
        if (this.playerShooting == null)
        {
            this.playerShooting = GetComponentInChildren<PlayerShooting>();
        }
    }

    private void LoadPlayerImpact()
    {
        if (this.playerImpact == null)
        {
            this.playerImpact = GetComponentInChildren<PlayerImpact>();
        }
    }

    private void LoadInventoryController()
    {
        if (this.inventoryController == null)
        {
            this.inventoryController = GetComponentInChildren<InventoryController>();
        }
    }

    private void LoadDamSender()
    {
        if (this.damSender == null)
        {
            this.damSender = GetComponentInChildren<DamageSender>();
        }
    }

    private void LoadWarp()
    {
        if (this.warp == null)
        {
            this.warp = GetComponentInChildren<Warp>();
        }
    }

    private void LoadAnimator()
    {
        if (this.animator == null)
        {
            this.animator = GetComponentInChildren<Animator>();
        }
    }
    private void LoadBurnController()
    {
        if (this.burnController == null)
        {
            this.burnController = GetComponentInChildren<BurnController>();
        }
    }
    #endregion
}
