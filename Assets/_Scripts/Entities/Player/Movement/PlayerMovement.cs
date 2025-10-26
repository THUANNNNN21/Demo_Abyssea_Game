using UnityEngine;

public class PlayerMovement : MyMonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    [Header("Movement Settings")]
    [SerializeField] float speed;
    public float Speed { get => speed; }

    private Vector2 moveInput;
    private float horizontal;
    private float vertical;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
    }

    private void LoadPlayerController()
    {
        if (this.playerController == null)
        {
            this.playerController = GetComponentInParent<PlayerController>();
        }
    }

    private void OnEnable()
    {
        InputManager.OnMoveInput += OnMoveInputReceived;
    }

    private void OnDisable()
    {
        InputManager.OnMoveInput -= OnMoveInputReceived;
    }

    void FixedUpdate()
    {
        this.UpdateAxisFromInput();
        this.MovementByAxis();
        this.RotateController();
    }
    private void OnMoveInputReceived(Vector2 input)
    {
        this.moveInput = input;
    }

    private void UpdateAxisFromInput()
    {
        this.horizontal = moveInput.x;
        this.vertical = moveInput.y;
    }

    private void MovementByAxis()
    {
        Vector3 direction = new(horizontal, vertical, 0f);

        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();
        }

        this.transform.parent.position += speed * Time.fixedDeltaTime * direction;
        Debug.DrawRay(this.transform.parent.position, direction, Color.red);
    }

    private void RotateController()
    {
        if (!Mathf.Approximately(horizontal, 0))
        {
            if (horizontal > 0)
            {
                this.playerController.Animator.SetFloat("MoveX", 1);
            }
            else if (horizontal < 0)
            {
                this.playerController.Animator.SetFloat("MoveX", -1);
            }
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
}
