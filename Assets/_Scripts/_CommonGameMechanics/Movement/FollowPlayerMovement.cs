using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class FollowPlayerMovement : EntityFollowTarget
{
    [Header("Follow Player Movement Settings")]
    [SerializeField] protected PlayerController playerController;
    public PlayerController PlayerController { get => playerController; set => playerController = value; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
        this.GetTarget();
    }
    private void LoadPlayerController()
    {
        if (this.playerController == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                this.playerController = player.GetComponent<PlayerController>();
            }
        }
    }
    protected virtual void GetTarget()
    {
        if (PlayerController != null)
        {
            this.target = PlayerController.transform;
        }
    }
}
