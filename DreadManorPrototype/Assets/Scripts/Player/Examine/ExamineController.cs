using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ExamineController : MonoBehaviour
{
    #region Events
    public event Action OnEnterEvent;
    public event Action OnExitEvent;
    [SerializeField] private UnityEvent<IExaminable> OnExamineEvent;
    #endregion

    private PlayerInput _inputActions;
    private IExaminable _examineObject;

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
        OnEnterEvent?.Invoke();
        if (_examineObject != null)
            OnExamineEvent?.Invoke(_examineObject);
        else
            throw new Exception("ExamineController can't examine an empty object");
    }

    private void OnDisable()
    {
        _inputActions.Examine.Disable();
    }

    private void OnExit(InputAction.CallbackContext context)
    {
        OnExitEvent?.Invoke();
    }

    public void AttachExamine(IExaminable examinable)
    {
        examinable = _examineObject;
    }
}