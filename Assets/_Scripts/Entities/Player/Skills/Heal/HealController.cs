using UnityEngine;

public class HealController : MyMonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    [SerializeField] private Heal heal;
    public Heal Heal { get => heal; }
    [SerializeField] private ActiveHeal activeHeal;
    public ActiveHeal ActiveHeal { get => activeHeal; }
    [SerializeField] private GameObject healFX;
    public GameObject HealFX { get => healFX; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
        this.LoadActiveHeal();
        this.LoadHeal();
        this.LoadHealFX();
    }
    private void LoadPlayerController()
    {
        if (this.playerController != null) return;
        else
        {
            this.playerController = this.GetComponentInParent<PlayerController>();
            Debug.LogWarning(this.gameObject.name + ": Load PlayerController");
        }
    }
    private void LoadHeal()
    {
        if (this.heal != null) return;
        else
        {
            this.heal = this.GetComponentInChildren<Heal>();
            Debug.LogWarning(this.gameObject.name + ": Load Heal");
        }
    }
    private void LoadActiveHeal()
    {
        if (this.activeHeal != null) return;
        else
        {
            this.activeHeal = this.GetComponentInChildren<ActiveHeal>();
            Debug.LogWarning(this.gameObject.name + ": Load ActiveHeal");
        }
    }
    private void LoadHealFX()
    {
        if (this.healFX != null) return;
        else
        {
            this.healFX = this.transform.Find("HealFX").gameObject;
            Debug.LogWarning(this.gameObject.name + ": Load HealFX");
        }
        this.healFX.SetActive(false);
    }
}
