using UnityEngine;

public class Warp : Cooldown
{
    [SerializeField] private SkillController skillController;
    public SkillController SkillController { get => skillController; }
    private Vector3 warpDirection;
    [SerializeField] private float warpDistance = 5f;
    // [SerializeField] private int assignedSkillIndex = 1;
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
        Transform obj = this.skillController.PlayerController.transform;
        Vector3 newPos = obj.position;
        newPos += warpDirection.normalized * this.warpDistance;
        obj.position = newPos;
        this.ResetCooldown();
    }
    private void CheckWarpPosition()
    {
        if (!isReady) return;
        Vector3 mouseWorldPos = InputManager.Instance.GetMouseWorldPosition();
        Vector3 playerPos = this.transform.position;
        this.warpDirection = mouseWorldPos - playerPos;
    }
}
