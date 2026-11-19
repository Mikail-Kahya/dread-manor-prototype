using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    public enum InputDevice
    {
        mouse,
        gamepad,
        invalid
    }

    #region Properties
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public InputDevice Device { get; private set; }
    #endregion


    private PlayerInput _inputActions;

    private UnityEvent<int> OnCrouchEvent;


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
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();

        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= OnMove;
        _inputActions.Player.Look.performed -= OnLook;
    }

    private void LateUpdate()
    {
        if (Device == InputDevice.mouse)
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
            Mouse => InputDevice.mouse,
            Gamepad => InputDevice.gamepad,
            _ => InputDevice.invalid
        };
    }
}
