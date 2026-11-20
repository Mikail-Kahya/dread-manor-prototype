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
    public event Action<bool> OnCrouchChanged;
    public event Action<bool> OnSprintChanged;
    #endregion

    private PlayerInput _inputActions;


    private void Awake()
    {
        _inputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        if( _inputActions == null )
        {
            _inputActions = new PlayerInput();
        }

        _inputActions.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;

        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMove;
        _inputActions.Player.Look.performed += OnLook;
        _inputActions.Player.Sprint.performed += OnSprint;
        _inputActions.Player.Sprint.canceled += OnSprint;
        _inputActions.Player.Crouch.performed += OnCrouch;
        _inputActions.Player.Crouch.canceled += OnCrouch;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();

        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= OnMove;
        _inputActions.Player.Look.performed -= OnLook;
        _inputActions.Player.Sprint.performed -= OnSprint;
        _inputActions.Player.Sprint.canceled -= OnSprint;
        _inputActions.Player.Crouch.performed -= OnCrouch;
        _inputActions.Player.Crouch.canceled -= OnCrouch;
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
        OnSprintChanged?.Invoke(context.ReadValueAsButton());
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        OnCrouchChanged?.Invoke(context.ReadValueAsButton());
    }
}
