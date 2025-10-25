using UnityEngine;

public class BurnController : MyMonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    [SerializeField] private GameObject model;
    public GameObject Model { get => model; }
    [SerializeField] private Burn burn;
    public Burn Burn { get => burn; }
    [SerializeField] private ActiveBurn activeBurn;
    public ActiveBurn ActiveBurn { get => activeBurn; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
        this.LoadActiveBurn();
        this.LoadBurn();
        this.LoadModel();
    }
    protected override void LoadValues()
    {
        base.LoadValues();
        this.burn.DisableSelf();
    }
    private void LoadPlayerController()
    {
        if (this.playerController != null) return;
        else
        {
            this.playerController = this.GetComponentInParent<PlayerController>();
        }
    }
    private void LoadBurn()
    {
        if (this.burn != null) return;
        else
        {
            this.burn = this.GetComponentInChildren<Burn>();
        }
    }
    private void LoadModel()
    {
        if (this.model != null) return;
        else
        {
            this.model = this.transform.Find("BurnFX").gameObject;
        }
    }
    private void LoadActiveBurn()
    {
        if (this.activeBurn != null) return;
        else
        {
            this.activeBurn = this.GetComponentInChildren<ActiveBurn>();
        }
    }
}
