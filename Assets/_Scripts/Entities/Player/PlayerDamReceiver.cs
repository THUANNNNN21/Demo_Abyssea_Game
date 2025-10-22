using UnityEngine;

public class PlayerDamReceiver : DamageReceiver
{
    [SerializeField] private PlayerController playerController;
    protected override void LoadValues()
    {
        this.SetHPMax(500);
    }
    private void LoadController()
    {
        if (this.playerController == null)
        {
            this.playerController = GetComponentInParent<PlayerController>();
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadController();
    }
    protected override void OnDead()
    {
        base.OnDead();
        Time.timeScale = 0f; // Pause the game
        Debug.Log("Ship destroyed! Game paused.");
    }
}

