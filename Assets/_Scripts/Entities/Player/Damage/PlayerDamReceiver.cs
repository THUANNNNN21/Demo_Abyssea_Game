using UnityEngine;

public class PlayerDamReceiver : DamageReceiver
{
    #region Inspector Fields
    [SerializeField] private PlayerController playerController;
    #endregion

    #region Properties
    public PlayerController PlayerController => playerController;
    #endregion

    #region Unity Methods
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadController();
    }
    #endregion

    #region Load Methods
    private void LoadController()
    {
        if (this.playerController == null)
        {
            this.playerController = GetComponentInParent<PlayerController>();
        }
    }
    #endregion

    #region Override Methods
    protected override void OnDead()
    {
        base.OnDead();
        Debug.LogWarning("Player has died. Game Over.");
    }
    #endregion
}

