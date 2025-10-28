using UnityEngine;

public class PlayerWarp : Warp
{
    [SerializeField] private SkillController skillController;
    public SkillController SkillController { get => skillController; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
    }
    private void LoadPlayerController()
    {
        if (this.skillController == null)
        {
            this.skillController = this.GetComponentInParent<SkillController>();
        }
    }
    void Update()
    {
        this.CheckWarpPosition();
    }
    void FixedUpdate()
    {
        this.Timing();
    }
    public void StartWarp()
    {
        if (!this.isReady) return;
        this.skillController.PlayerController.Animator.SetBool("warp", true);
    }
    public void WarpFinish()
    {
        this.skillController.PlayerController.Animator.SetBool("warp", false);
    }
    public void Warping()
    {
        this.Warping(this.skillController.PlayerController.transform);
    }
    protected override void CheckWarpPosition()
    {
        if (!isReady) return;
        Vector3 mouseWorldPos = InputManager.Instance.GetMouseWorldPosition();
        Vector3 playerPos = this.transform.position;
        this.warpDirection = mouseWorldPos - playerPos;
    }
}
