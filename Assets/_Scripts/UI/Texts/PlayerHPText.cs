using UnityEngine;

public class PlayerHPText : BaseText
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
    }
    private void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.LogWarning("Load PlayerController: " + playerController.name, gameObject);
    }
    void FixedUpdate()
    {
        this.UpdateHPText();
    }
    private void UpdateHPText()
    {
        string HP = this.playerController.PlayerDamReceiver.Health.ToString();
        string HPMax = this.playerController.PlayerDamReceiver.HealthMax.ToString();
        this.uiText.text = "HP: " + HP + " / " + HPMax;
    }
}
