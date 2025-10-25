using UnityEngine;

public class ActiveBurn : Cooldown
{
    [SerializeField] private BurnController burnController;
    public BurnController BurnController { get => burnController; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBurnController();
    }
    private void LoadBurnController()
    {
        if (this.burnController == null)
        {
            this.burnController = GetComponentInParent<BurnController>();
        }
    }
    void FixedUpdate()
    {
        this.Timing();
    }
    public void StartBurn()
    {
        if (this.isReady)
        {
            if (this.burnController == null) return;
            {
                this.Active();
                this.ResetCooldown();
            }
        }
    }
    private void Active()
    {
        this.burnController.Fx.SetActive(true);
        this.burnController.Burn.StartBurn();
    }
}
