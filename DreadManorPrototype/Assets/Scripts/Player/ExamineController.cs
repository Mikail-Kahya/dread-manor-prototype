using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExamineController : MonoBehaviour
{
    public event Action OnExitEvent;

    [SerializeField] private Transform ExamineAnchorTransform;

    private PlayerInput _inputActions;


    private void Awake()
    {
        _inputActions = new PlayerInput();

        _inputActions.Examine.Exit.performed += OnExit;
    }

    private void OnDestroy()
    {
        _inputActions.Examine.Exit.performed -= OnExit;
    }

    private void OnEnable()
    {
        _inputActions.Examine.Enable();
        Cursor.lockState = CursorLockMode.Locked;
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