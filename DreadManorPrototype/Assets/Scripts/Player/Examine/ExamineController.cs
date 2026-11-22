using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ExamineController : MonoBehaviour
{
    #region Events
    public event Action OnEnterEvent;
    public event Action OnExitEvent;
    public event Action <GameObject> OnExamineEvent;
    public event Action<GameObject> OnExamineEndEvent;
    #endregion

    private PlayerInput _inputActions;
    private GameObject _examineObject;

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
        if (_examineObject != null)
            OnExamineEndEvent?.Invoke(_examineObject);
        else
            throw new Exception("ExamineController can't end examination on an empty object");
    }

    private void OnExit(InputAction.CallbackContext context)
    {
        OnExitEvent?.Invoke();
    }

    public void AttachExamine(IExaminable examinable)
    {
        if (examinable is MonoBehaviour examineObject)
            _examineObject = examineObject.gameObject;
        else
            _examineObject = null;
    }
}