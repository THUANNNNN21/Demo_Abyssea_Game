using UnityEngine;

public class PlayerMovement : MyMonoBehaviour
{
    [SerializeField] private PlayerController shipController;
    public PlayerController ShipController { get => shipController; }
    [Header("Movement Settings")]
    [SerializeField] float speed;
    public float Speed { get => speed; }

    private Vector2 moveInput;
    private float horizontal;
    private float vertical;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipController();
    }

    private void LoadShipController()
    {
        if (this.shipController == null)
        {
            this.shipController = GetComponentInParent<PlayerController>();
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
        moveInput = input;
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
    }

    private void RotateController()
    {
        if (horizontal > 0)
        {
            this.transform.parent.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal < 0)
        {
            this.transform.parent.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    [ContextMenu("Debug Move Input")]
    private void DebugMoveInput()
    {
        Debug.Log($"Move Input: {moveInput}, Horizontal: {horizontal}, Vertical: {vertical}");
    }
}
