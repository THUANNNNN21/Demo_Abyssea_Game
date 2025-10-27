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

    // ✅ Skill events
    public static event Action<int> OnItemSelected;
    public static event Action OnItemCast;
    public static event Action OnItemDeselected; // New event for deselection
    #endregion

    // ✅ Skill state - persistent selection
    private bool isItemSelected = false;
    private int selectedItemIndex = -1;

    public bool IsItemSelected => isItemSelected;
    public int SelectedItemIndex => selectedItemIndex;

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
                // Debug.LogWarning($"Action '{actionName}' not found in Input Actions");
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

        // Mouse click
        inputActions.Player.MouseClick.performed += OnMouseClickPerformed;

        // Upgrade modifier
        inputActions.Player.UpgradeModifier.started += OnUpgradeModifierStarted;
        inputActions.Player.UpgradeModifier.canceled += OnUpgradeModifierCanceled;

        // Drop modifier
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
    }

    private void OnUpgradeModifierCanceled(InputAction.CallbackContext context)
    {
        isUpgradeModifierPressed = false;
    }

    private void OnDropModifierStarted(InputAction.CallbackContext context)
    {
        isDropModifierPressed = true;
    }

    private void OnDropModifierCanceled(InputAction.CallbackContext context)
    {
        isDropModifierPressed = false;
    }

    // ✅ Updated skill selection - persistent until manually changed
    private void OnSelectPosition(int position)
    {
        if (isUpgradeModifierPressed)
        {
            OnUpgradeItem?.Invoke(position);
        }
        else if (isDropModifierPressed)
        {
            OnDropItem?.Invoke(position);
        }
        // ✅ Item selection for positions 1-4
        else if (position >= 1 && position <= 4)
        {
            int itemIndex = position - 1; // Convert to 0-based index

            if (isItemSelected && selectedItemIndex == itemIndex)
            {
                // ✅ Deselect only if same item pressed again
                isItemSelected = false;
                selectedItemIndex = -1;
                OnItemDeselected?.Invoke();
            }
            else
            {
                isItemSelected = true;
                selectedItemIndex = itemIndex;
                OnItemSelected?.Invoke(selectedItemIndex);
            }
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
        OnPickupItem?.Invoke();
    }

    // ✅ Updated mouse click - NO auto-deselect
    private void OnMouseClickPerformed(InputAction.CallbackContext context)
    {
        if (isItemSelected)
        {
            OnItemCast?.Invoke();
        }
        else
        {
            OnMouseClick?.Invoke();
        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        this.MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return this.MouseWorldPosition;
    }
    #endregion
}