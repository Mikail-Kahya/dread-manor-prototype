using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    #region Properties
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public Player.InputDevice Device { get; private set; }
    #endregion

    #region Events
    public event Action<bool> OnCrouchChangedEvent;
    public event Action<bool> OnSprintChangedEvent;
    public event Action OnInteractEvent;
    #endregion

    private PlayerInput _inputActions;


    private void Awake()
    {
        _inputActions = new PlayerInput();

        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMove;
        _inputActions.Player.Look.performed += OnLook;
        _inputActions.Player.Sprint.performed += OnSprint;
        _inputActions.Player.Sprint.canceled += OnSprint;
        _inputActions.Player.Crouch.performed += OnCrouch;
        _inputActions.Player.Crouch.canceled += OnCrouch;
        _inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDestroy()
    {
        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= OnMove;
        _inputActions.Player.Look.performed -= OnLook;
        _inputActions.Player.Sprint.performed -= OnSprint;
        _inputActions.Player.Sprint.canceled -= OnSprint;
        _inputActions.Player.Crouch.performed -= OnCrouch;
        _inputActions.Player.Crouch.canceled -= OnCrouch;
        _inputActions.Player.Interact.performed -= OnInteract;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
        LookInput = Vector3.zero;
        MoveInput = Vector3.zero;
    }

    private void LateUpdate()
    {
        if (Device == Player.InputDevice.mouse)
            LookInput = Vector2.zero;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
    private void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
        Device = context.control.device switch
        {
            Mouse => Player.InputDevice.mouse,
            Gamepad => Player.InputDevice.gamepad,
            _ => Player.InputDevice.invalid
        };
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        OnSprintChangedEvent?.Invoke(context.ReadValueAsButton());
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        OnCrouchChangedEvent?.Invoke(context.ReadValueAsButton());
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        OnInteractEvent?.Invoke();
    }
}
