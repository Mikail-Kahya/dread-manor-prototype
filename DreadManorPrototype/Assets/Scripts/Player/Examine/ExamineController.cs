using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ExamineController : MonoBehaviour
{
    #region Events
    public event Action OnEnterEvent;
    public event Action OnExitEvent;
    public event Action <IExaminable> OnExamineEvent;
    public event Action<IExaminable> OnExamineEndEvent;
    public event Action<float> OnScrollEvent;
    #endregion

    private PlayerInput _inputActions;
    private IExaminable _examineObject;

    private void Awake()
    {
        _inputActions = new PlayerInput();

        _inputActions.Examine.Exit.performed += OnExit;
        _inputActions.Examine.Scroll.performed += OnScroll;
    }

    private void OnDestroy()
    {
        _inputActions.Examine.Exit.performed -= OnExit;
        _inputActions.Examine.Scroll.performed -= OnScroll;
    }

    private void OnEnable()
    {
        _inputActions.Examine.Enable();
        Cursor.lockState = CursorLockMode.Confined;
        OnEnterEvent?.Invoke();
        if (_examineObject != null)
            OnExamineEvent?.Invoke(_examineObject);
        else
            throw new Exception("ExamineController can't examine an empty object");
    }

    private void OnDisable()
    {
        _inputActions.Examine.Disable();
        if (_examineObject != null)
            OnExamineEndEvent?.Invoke(_examineObject);
        else
            throw new Exception("ExamineController can't end examination on an empty object");
    }
    public void AttachExamine(IExaminable examinable)
    {
        _examineObject = examinable;
    }

    private void OnExit(InputAction.CallbackContext context)
    {
        OnExitEvent?.Invoke();
    }

    private void OnScroll(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        OnScrollEvent?.Invoke(input.y);
    }
}