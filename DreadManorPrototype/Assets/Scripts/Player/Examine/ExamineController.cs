using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExamineController : MonoBehaviour
{
    public Vector2 RotateInput { get; private set; }

    #region Events
    public event Action OnEnterEvent;
    public event Action OnExitEvent;
    public event Action <IExaminable> OnExamineEvent;
    public event Action<IExaminable> OnExamineEndEvent;
    public event Action<float> OnScrollEvent;
    public event Action OnNextEvent;
    public event Action OnPrevEvent;
    #endregion

    private IExaminable _examineObject;

    public void Subscribe(PlayerInputBinds inputBinds)
    {
        inputBinds.Examine.Exit.performed += OnExit;
        inputBinds.Examine.Scroll.performed += OnScroll;
        inputBinds.Examine.Next.performed += OnNext;
        inputBinds.Examine.Previous.performed += OnPrev;
        inputBinds.Examine.Rotate.performed += OnRotate;
    }

    public void Unsubscribe(PlayerInputBinds inputBinds)
    {
        inputBinds.Examine.Exit.performed -= OnExit;
        inputBinds.Examine.Scroll.performed -= OnScroll;
        inputBinds.Examine.Next.performed -= OnNext;
        inputBinds.Examine.Previous.performed -= OnPrev;
        inputBinds.Examine.Rotate.performed -= OnRotate;
    }

    private void OnEnable()
    {
        OnEnterEvent?.Invoke();
        if (_examineObject != null)
        {
            OnExamineEvent?.Invoke(_examineObject);
        }
        else
        {
            Debug.LogError("ExamineController can't examine an empty object");
        }
    }

    private void OnDisable()
    {
        RotateInput = Vector2.zero;

        if (_examineObject != null)
        {
            _examineObject.Release();
            OnExamineEndEvent?.Invoke(_examineObject);
            _examineObject = null;
        }
        else
        {
            Debug.LogError("ExamineController can't end examination on an empty object");
        }
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

    private void OnNext(InputAction.CallbackContext context)
    {
        OnNextEvent?.Invoke();
    }

    private void OnPrev(InputAction.CallbackContext context)
    {
        OnPrevEvent?.Invoke();
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        RotateInput = context.ReadValue<Vector2>();
    }
}