using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExamineController : MonoBehaviour
{
    [SerializeField] private Transform ExamineAnchorTransform;

    public event Action OnExitEvent;

    private PlayerInput _inputActions;


    private void Awake()
    {
        _inputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        _inputActions.Examine.Enable();
        Cursor.lockState = CursorLockMode.Locked;

        _inputActions.Examine.Exit.performed += OnExit;
    }

    private void OnDisable()
    {
        _inputActions.Examine.Disable();
    }

    private void OnExit(InputAction.CallbackContext context)
    {
        OnExitEvent?.Invoke();
    }
}