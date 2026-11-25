using System;
using UnityEngine;

[RequireComponent(typeof(FirstPersonController), typeof(ExamineController))]
public class ControllerSwapper : MonoBehaviour
{
    public enum Mapping { play, examine }

    #region Properties
    public FirstPersonController PlayController => _playController;
    public ExamineController ExamineController => _examineController;
    #endregion

    private FirstPersonController _playController;
    private ExamineController _examineController;
    private MonoBehaviour _currentController;
    private PlayerInputBinds _inputActions;

    private Action _cachedOnExit;

    [SerializeField] Mapping StartMapping = Mapping.play;


    private void Awake()
    {
        _inputActions = new PlayerInputBinds();
        _playController = GetComponent<FirstPersonController>();
        _examineController = GetComponent<ExamineController>();

        _playController.Subscribe(_inputActions);
        _examineController.Subscribe(_inputActions);

        _cachedOnExit = () => Swap(Mapping.play);
        _examineController.OnExitEvent += _cachedOnExit;
    }

    private void OnDestroy()
    {
        _examineController.OnExitEvent -= _cachedOnExit;

        _playController.Unsubscribe(_inputActions);
        _examineController.Unsubscribe(_inputActions);
    }

    private void Start()
    {
        Swap(StartMapping);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Swap(Mapping type)
    {
        if (_currentController != null && _currentController.isActiveAndEnabled)
            _currentController.enabled = false;

        switch (type) 
        {
            case Mapping.examine:
                _currentController = _examineController;
                _inputActions.Player.Disable();
                _inputActions.Examine.Enable();
                break;
            case Mapping.play:
            default:
                _currentController = _playController;
                _inputActions.Examine.Disable();
                _inputActions.Player.Enable();
                break;
        };

        _currentController.enabled = true;
    }
}