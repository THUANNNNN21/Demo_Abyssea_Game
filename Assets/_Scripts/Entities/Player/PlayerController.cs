using UnityEditor;
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

    [SerializeField] private PlayerImpact playerImpact;
    public PlayerImpact PlayerImpact { get => playerImpact; }

    [SerializeField] private InventoryController inventoryController;
    public InventoryController InventoryController { get => inventoryController; }

    [SerializeField] private DamageSender damSender;
    public DamageSender DamSender { get => damSender; }

    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; }
    [SerializeField] private ChangeModel changeModel;
    public ChangeModel ChangeModel { get => changeModel; }
    [SerializeField] private HealController healController;
    public HealController HealController { get => healController; }

    [SerializeField] private SkillController skillController;
    public SkillController SkillController { get => skillController; }
    #endregion

    #region Initialization
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSO();
        this.LoadPlayerDamReceiver();
        this.LoadDamSender();
        this.LoadPlayerMovement();
        this.LoadPlayerImpact();
        this.LoadInventoryController();
        this.LoadAnimator();
        this.LoadChangeModel();
        this.LoadSkillController();
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
        this.animator.SetFloat("MoveX", 1);
    }
    #endregion

    #region Component Loading Methods
    private void LoadPlayerSO()
    {
        if (this.playerSO != null) return;
        this.playerSO = Resources.Load<PlayerSO>("_SO/PlayerSO/" + this.name);
        Debug.LogWarning(this.gameObject.name + ": Load PlayerSO");
    }

    private void LoadPlayerDamReceiver()
    {
        if (this.playerDamReceiver != null) return;
        this.playerDamReceiver = GetComponentInChildren<PlayerDamReceiver>();
        Debug.LogWarning(this.gameObject.name + ": Load PlayerDamReceiver");
    }

    private void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = GetComponentInChildren<PlayerMovement>();
        Debug.LogWarning(this.gameObject.name + ": Load PlayerMovement");
    }


    private void LoadPlayerImpact()
    {
        if (this.playerImpact != null) return;
        this.playerImpact = GetComponentInChildren<PlayerImpact>();
        Debug.LogWarning(this.gameObject.name + ": Load PlayerImpact");
    }

    private void LoadInventoryController()
    {
        if (this.inventoryController != null) return;
        this.inventoryController = GetComponentInChildren<InventoryController>();
        Debug.LogWarning(this.gameObject.name + ": Load InventoryController");
    }

    private void LoadDamSender()
    {
        if (this.damSender != null) return;
        this.damSender = GetComponentInChildren<DamageSender>();
        Debug.LogWarning(this.gameObject.name + ": Load DamSender");
    }

    private void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = this.transform.Find("Model").GetComponent<Animator>();
        Debug.LogWarning(this.gameObject.name + ": Load Animator");
    }

    private void LoadChangeModel()
    {
        if (this.changeModel != null) return;
        this.changeModel = GetComponentInChildren<ChangeModel>();
        Debug.LogWarning(this.gameObject.name + ": Load ChangeModel");
    }

    private void LoadSkillController()
    {
        if (this.skillController != null) return;
        this.skillController = GetComponentInChildren<SkillController>();
        Debug.LogWarning(this.gameObject.name + ": Load SkillController");
    }
    #endregion
}
