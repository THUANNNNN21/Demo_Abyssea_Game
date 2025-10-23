using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MyMonoBehaviour
{
    [Header("References Settings")]
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
    [SerializeField] private PlayerSO playerSO;
    public PlayerSO PlayerSO { get => playerSO; }
    [SerializeField] private PlayerDamReceiver playerDamReceiver;
    public PlayerDamReceiver PlayerDamReceiver { get => playerDamReceiver; }
    [SerializeField] private PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement { get => playerMovement; }
    [SerializeField] private InventoryController inventoryController;
    public InventoryController InventoryController { get => inventoryController; }
    [SerializeField] private DamageSender damSender;
    public DamageSender DamSender { get => damSender; }
    [SerializeField] private PlayerImpact playerImpact;
    public PlayerImpact PlayerImpact { get => playerImpact; }
    [SerializeField] private Warp warp;
    public Warp Warp { get => warp; }
    [SerializeField] private PlayerShooting playerShooting;
    public PlayerShooting PlayerShooting { get => playerShooting; }
    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSO();
        this.LoadPlayerDamReceiver();
        this.LoadInventoryController();
        this.LoadPlayerMovement();
        this.LoadDamSender();
        this.LoadPlayerImpact();
        this.LoadAnimator();
        this.LoadWarp();
        this.LoadPlayerShooting();
    }
    protected override void LoadValues()
    {
        this.playerDamReceiver.SetHPMax(playerSO.maxHP);
        this.playerMovement.SetSpeed(playerSO.speed);
        this.inventoryController.SetMaxSlot(playerSO.inventorySize);
        this.damSender.SetDamage(playerSO.damage);
        this.playerImpact.SetImpactRange(playerSO.attackRange);
        this.inventoryController.ItemLooter.SetLootRange(playerSO.lootRange);
        this.playerImpact.SetDelayTime(playerSO.attackDelay);
    }
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
    private void LoadInventoryController()
    {
        if (this.inventoryController == null)
        {
            this.inventoryController = GetComponentInChildren<InventoryController>();
        }
    }
    private void LoadPlayerMovement()
    {
        if (this.playerMovement == null)
        {
            this.playerMovement = GetComponentInChildren<PlayerMovement>();
        }
    }
    private void LoadDamSender()
    {
        if (this.damSender == null)
        {
            this.damSender = GetComponentInChildren<DamageSender>();
        }
    }
    private void LoadPlayerImpact()
    {
        if (this.playerImpact == null)
        {
            this.playerImpact = GetComponentInChildren<PlayerImpact>();
        }
    }
    private void LoadAnimator()
    {
        if (this.animator == null)
        {
            this.animator = GetComponentInChildren<Animator>();
        }
    }
    private void LoadWarp()
    {
        if (this.warp == null)
        {
            this.warp = GetComponentInChildren<Warp>();
        }
    }
    private void LoadPlayerShooting()
    {
        if (this.playerShooting == null)
        {
            this.playerShooting = GetComponentInChildren<PlayerShooting>();
        }
    }
}
