using UnityEngine;

public class PlayerMovement : MyMonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private PlayerController playerController;
    [Header("Movement Settings")]
    [SerializeField] private float speed;
    #endregion

    #region Properties
    public PlayerController PlayerController => playerController;
    public float Speed => speed;
    #endregion

    #region Private Fields
    private Vector2 moveInput;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    #endregion

    #region Unity Methods
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerController();
        LoadRigidbody2D();
    }

    private void OnEnable()
    {
        InputManager.OnMoveInput += OnMoveInputReceived;
    }

    private void OnDisable()
    {
        InputManager.OnMoveInput -= OnMoveInputReceived;
    }

    private void FixedUpdate()
    {
        UpdateAxisFromInput();
        MovementByAxis();
        RotateController();
    }
    #endregion

    #region Load Methods
    private void LoadPlayerController()
    {
        if (playerController == null)
        {
            playerController = GetComponentInParent<PlayerController>();
        }
    }

    private void LoadRigidbody2D()
    {
        if (rb == null)
        {
            rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        }
    }
    #endregion

    #region Input Methods
    private void OnMoveInputReceived(Vector2 input)
    {
        moveInput = input;
    }

    private void UpdateAxisFromInput()
    {
        horizontal = moveInput.x;
        vertical = moveInput.y;
    }
    #endregion

    #region Movement Methods
    private void MovementByAxis()
    {
        Vector3 direction = new(horizontal, vertical, 0f);

        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();
        }

        Vector3 newPosition = transform.parent.position + speed * Time.fixedDeltaTime * direction;
        rb.MovePosition(newPosition);
    }

    private void RotateController()
    {
        if (!Mathf.Approximately(horizontal, 0))
        {
            if (horizontal > 0)
            {
                playerController.Animator.SetFloat("MoveX", 1);
            }
            else if (horizontal < 0)
            {
                playerController.Animator.SetFloat("MoveX", -1);
            }
        }
    }
    #endregion

    #region Public Methods
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    #endregion
}
