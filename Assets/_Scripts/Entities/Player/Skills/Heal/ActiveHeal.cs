using Unity.VisualScripting;
using UnityEngine;

public class ActiveHeal : Cooldown
{
    [SerializeField] private HealController healController;
    public HealController HealController { get => healController; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHealController();
    }
    private void LoadHealController()
    {
        if (this.healController == null)
        {
            this.healController = GetComponentInParent<HealController>();
        }
    }
    void FixedUpdate()
    {
        this.Timing();
    }
    public void StartHeal()
    {
        if (this.isReady)
        {
            this.Active();
            this.ResetCooldown();
        }
    }
    private void Active()
    {
        this.healController.Heal.HealPlayer();
        this.healController.HealFX.SetActive(true);
    }
    public void StopHeal()
    {
        this.healController.HealFX.SetActive(false);
    }
}
