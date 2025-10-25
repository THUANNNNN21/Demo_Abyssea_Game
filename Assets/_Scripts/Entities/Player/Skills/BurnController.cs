using UnityEngine;

public class BurnController : MyMonoBehaviour
{
    [SerializeField] private SkillController skillController;
    public SkillController SkillController { get => skillController; }
    [SerializeField] private GameObject fx;
    public GameObject Fx { get => fx; }
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
        this.LoadFx();
    }
    protected override void LoadValues()
    {
        base.LoadValues();
        this.Fx.SetActive(false);
    }
    private void LoadPlayerController()
    {
        if (this.skillController != null) return;
        else
        {
            this.skillController = this.GetComponentInParent<SkillController>();
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
    private void LoadFx()
    {
        if (this.fx != null) return;
        else
        {
            this.fx = this.transform.Find("BurnFX").gameObject;
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
