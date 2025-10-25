using UnityEngine;

public class Warp : Cooldown
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
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
        if (this.playerController == null)
        {
            this.playerController = this.GetComponentInParent<PlayerController>();
        }
    }
    // private void OnEnable()
    // {
    //     InputManager.OnItemCast += this.OnMouseClickReceived;
    // }
    // private void OnDisable()
    // {
    //     InputManager.OnItemCast -= this.OnMouseClickReceived;
    // }
    void Update()
    {
        this.CheckWarpPosition();
    }
    void FixedUpdate()
    {
        this.Timing();
    }
    private void OnMouseClickReceived(int skillIndex)
    {
        // if (skillIndex == assignedSkillIndex)
        // {
        //     Debug.Log($"PlayerWarping: Casting skill {skillIndex + 1}");
        //     this.StartWarp();
        // }
        // else
        // {
        //     Debug.Log($"PlayerWarping: Ignoring skill {skillIndex + 1} (not assigned to this warper)");
        // }
    }
    public void StartWarp()
    {
        if (!this.isReady) return;
        this.playerController.Animator.SetBool("warp", true);
    }
    public void WarpFinish()
    {
        this.playerController.Animator.SetBool("warp", false);
    }
    public void Warping()
    {
        Transform obj = this.transform.parent;
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
