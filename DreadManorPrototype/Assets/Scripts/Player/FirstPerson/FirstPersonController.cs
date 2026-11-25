using System;
using UnityEngine;
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


    public void Subscribe(PlayerInputBinds inputBinds)
    {
        inputBinds.Player.Move.performed += OnMove;
        inputBinds.Player.Move.canceled += OnMove;
        inputBinds.Player.Look.performed += OnLook;
        inputBinds.Player.Sprint.performed += OnSprint;
        inputBinds.Player.Sprint.canceled += OnSprint;
        inputBinds.Player.Crouch.performed += OnCrouch;
        inputBinds.Player.Crouch.canceled += OnCrouch;
        inputBinds.Player.Interact.performed += OnInteract;
    }

    public void Unsubscribe(PlayerInputBinds inputBinds)
    {
        inputBinds.Player.Move.performed -= OnMove;
        inputBinds.Player.Move.canceled -= OnMove;
        inputBinds.Player.Look.performed -= OnLook;
        inputBinds.Player.Sprint.performed -= OnSprint;
        inputBinds.Player.Sprint.canceled -= OnSprint;
        inputBinds.Player.Crouch.performed -= OnCrouch;
        inputBinds.Player.Crouch.canceled -= OnCrouch;
        inputBinds.Player.Interact.performed -= OnInteract;
    }

    private void OnDisable()
    {
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
