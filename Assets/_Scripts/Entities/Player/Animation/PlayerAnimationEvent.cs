using UnityEngine;

public class PlayerAnimationEvent : MyMonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private PlayerController playerController;
    #endregion

    #region Properties
    public PlayerController PlayerController { get => playerController; }
    #endregion

    #region Unity Methods
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
    }
    #endregion

    #region Load Methods
    private void LoadPlayerController()
    {
        if (this.playerController != null) return;
        this.playerController = this.GetComponentInParent<PlayerController>();
        Debug.LogWarning($"PlayerAnimationEvent: Load PlayerController in {this.name}");
    }
    #endregion

    #region Animation Event Methods
    public void OnAttackAnimationPlay()
    {
        this.playerController.PlayerImpact.AttackAllEnemies();
    }

    public void OnAttackAnimationComplete()
    {
        this.playerController.PlayerImpact.CompleteAttack();
    }

    public void OnWarpAnimationComplete()
    {
        this.playerController.SkillController.Warp.WarpFinish();
    }

    public void OnWarpAnimationPlay()
    {
        this.playerController.SkillController.Warp.Warping();
    }
    #endregion
}
