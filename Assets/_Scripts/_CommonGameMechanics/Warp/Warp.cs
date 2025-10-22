using UnityEngine;

public class Warp : Cooldown
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    [SerializeField] private bool isWarping = false;
    private Vector3 warpDirection;
    [SerializeField] private float warpDistance = 5f;
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
    private void OnEnable()
    {
        InputManager.OnMouseClick += this.OnMouseClickReceived;
    }
    private void OnDisable()
    {
        InputManager.OnMouseClick -= this.OnMouseClickReceived;
    }
    void Update()
    {
        this.CheckWarpPosition();
    }
    void FixedUpdate()
    {
        this.Timing();
    }
    private void OnMouseClickReceived()
    {
        this.StartWarp();
    }
    private void StartWarp()
    {
        if (!this.isReady) return;
        if (isWarping) return;
        this.isWarping = true;
        this.playerController.Animator.SetTrigger("warp");
    }
    public void WarpFinish()
    {
        this.isWarping = false;
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
        if (isWarping) return;
        if (!isReady) return;
        Vector3 mouseWorldPos = InputManager.Instance.GetMouseWorldPosition();
        Vector3 playerPos = this.transform.position;
        this.warpDirection = mouseWorldPos - playerPos;
    }
}
