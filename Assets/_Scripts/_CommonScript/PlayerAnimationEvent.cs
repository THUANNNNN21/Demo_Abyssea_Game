using UnityEngine;

public class PlayerAnimationEvent : MyMonoBehaviour
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
        if (this.playerController != null) return;
        else
        {
            this.playerController = this.GetComponentInParent<PlayerController>();
        }
    }
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
}
