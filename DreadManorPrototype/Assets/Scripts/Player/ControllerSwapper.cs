using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FirstPersonController), typeof(ExamineController))]
public class ControllerSwapper : MonoBehaviour
{
    public enum Controller { play, examine }

    #region Properties
    public FirstPersonController PlayController => _playController;
    public ExamineController ExamineController => _examineController;
    #endregion

    private FirstPersonController _playController;
    private ExamineController _examineController;
    private MonoBehaviour _currentController;

    private Action _cachedOnExit;


    private void Awake()
    {
        _playController = GetComponent<FirstPersonController>();
        _examineController = GetComponent<ExamineController>();

        if (_playController.isActiveAndEnabled)
            _currentController = _playController;
        else if (_examineController.isActiveAndEnabled)
            _currentController = _examineController;

        _cachedOnExit = () => Swap(Controller.play);
        _examineController.OnExitEvent += _cachedOnExit;
    }

    private void OnDestroy()
    { 
        _examineController.OnExitEvent -= _cachedOnExit;
    }

    public void Swap(Controller type)
    {
        if (_currentController != null && _currentController.isActiveAndEnabled)
            _currentController.enabled = false;

        _currentController = type switch
        {
            Controller.play => _playController,
            Controller.examine => _examineController
        };

        _currentController.enabled = true;
    }
}