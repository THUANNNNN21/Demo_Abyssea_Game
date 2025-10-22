using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [Header("Input Actions")]
    [SerializeField] private PlayerInputActions inputActions;

    private Vector3 _mouseWorldPos;
    public Vector3 MouseWorldPosition
    {
        get => _mouseWorldPos;
        set
        {
            _mouseWorldPos = value;
            _mouseWorldPos.z = 0;
        }
    }

    #region Events
    public static event Action<Vector2> OnMoveInput;
    public static event Action OnPickupItem;
    public static event Action<int> OnDropItem;
    public static event Action<int> OnUpgradeItem;
    public static event Action OnMouseClick;
    #endregion

    private bool isUpgradeModifierPressed = false;
    private bool isDropModifierPressed = false;

    private InputAction[] positionActions = new InputAction[9];

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        inputActions = new PlayerInputActions();
        SetupPositionActions();
    }

    private void SetupPositionActions()
    {
        for (int i = 0; i < 9; i++)
        {
            int position = i + 1; // 1-9
            string actionName = $"SelectPosition{position}";

            positionActions[i] = inputActions.FindAction(actionName);

            if (positionActions[i] == null)
            {
                Debug.LogWarning($"Action '{actionName}' not found in Input Actions");
            }
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();

        // Movement và pickup
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
        inputActions.Player.Pickup.performed += OnPickupPerformed;
        inputActions.Player.MouseClick.performed += OnMouseClickPerformed;

        // Upgrade modifier
        inputActions.Player.UpgradeModifier.started += OnUpgradeModifierStarted;
        inputActions.Player.UpgradeModifier.canceled += OnUpgradeModifierCanceled;

        inputActions.Player.DropModifier.started += OnDropModifierStarted;
        inputActions.Player.DropModifier.canceled += OnDropModifierCanceled;
        SubscribeToPositionActions();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
        inputActions.Player.Pickup.performed -= OnPickupPerformed;
        inputActions.Player.MouseClick.performed -= OnMouseClickPerformed;

        inputActions.Player.UpgradeModifier.started -= OnUpgradeModifierStarted;
        inputActions.Player.UpgradeModifier.canceled -= OnUpgradeModifierCanceled;

        inputActions.Player.DropModifier.started -= OnDropModifierStarted;
        inputActions.Player.DropModifier.canceled -= OnDropModifierCanceled;


        UnsubscribeFromPositionActions();

        inputActions.Disable();
    }

    private void SubscribeToPositionActions()
    {
        for (int i = 0; i < positionActions.Length; i++)
        {
            if (positionActions[i] != null)
            {
                int position = i + 1; // Capture position (1-9)
                positionActions[i].performed += (context) => OnSelectPosition(position);
            }
        }
    }

    private void UnsubscribeFromPositionActions()
    {
        for (int i = 0; i < positionActions.Length; i++)
        {
            if (positionActions[i] != null)
            {
                int position = i + 1;
                positionActions[i].performed -= (context) => OnSelectPosition(position);
            }
        }
    }

    #region Input Callbacks

    private void OnUpgradeModifierStarted(InputAction.CallbackContext context)
    {
        isUpgradeModifierPressed = true;
        Debug.Log("Upgrade mode activated - Hold and press 1-9 to select position");
    }

    private void OnUpgradeModifierCanceled(InputAction.CallbackContext context)
    {
        isUpgradeModifierPressed = false;
        Debug.Log("Upgrade mode deactivated");
    }
    private void OnDropModifierStarted(InputAction.CallbackContext context)
    {
        isDropModifierPressed = true;
        Debug.Log("Drop mode activated - Hold and press 1-9 to select position");
    }

    private void OnDropModifierCanceled(InputAction.CallbackContext context)
    {
        isDropModifierPressed = false;
        Debug.Log("Drop mode deactivated");
    }

    // ✅ Single callback cho tất cả positions
    private void OnSelectPosition(int position)
    {
        if (isUpgradeModifierPressed)
        {
            Debug.Log($"Upgrade item at position {position}");
            OnUpgradeItem?.Invoke(position);
        }
        else if (isDropModifierPressed)
        {
            Debug.Log($"Drop item at position {position}");
            OnDropItem?.Invoke(position);
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        OnMoveInput?.Invoke(moveInput);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(Vector2.zero);
    }

    private void OnPickupPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("E key pressed - Pickup requested");
        OnPickupItem?.Invoke();
    }
    private void OnMouseClickPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse clicked");
        OnMouseClick?.Invoke();
    }
    #endregion

    // #region Legacy Methods
    public Vector3 GetMouseWorldPosition()
    {
        this.MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return this.MouseWorldPosition;
    }
    // #endregion
}