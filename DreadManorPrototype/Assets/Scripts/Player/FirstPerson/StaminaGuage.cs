using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class StaminaGuage : MonoBehaviour
{
    private enum State { drain, delay, regen, idle }

    #region Events
    public event Action OnStaminaDrained;
    public event Action OnStaminaRecharging;
    public event Action OnStaminaRecharged;
    #endregion

    #region Properties
    public bool IsDraining => _state == State.drain;
    public float MaxStam => MaxStamina;
    #endregion

    [SerializeField] private float MaxStamina = 4f;
    [SerializeField] private float StaminaRegenDelay = 1f;
    [SerializeField] private float StaminaRegenRate = 1f;
    private float _stamina = 0f;
    private float _regenDelay = 0f;
    private State _state = State.idle;
    private bool _isDraining = false;

    void Start()
    {
        _stamina = MaxStam;
    }

    void Update()
    {
        switch (_state)
        {
            case State.drain:
                _stamina = Mathf.Max(_stamina - Time.deltaTime, 0f);
                if (DM.MathHelpers.IsEqual(_stamina, 0f))
                { 
                    _state = State.delay;
                    _regenDelay = 0f;
                    OnStaminaDrained?.Invoke();
                }
                break;
            case State.delay:
                _regenDelay = Mathf.Min(_regenDelay + Time.deltaTime, StaminaRegenDelay);
                if (DM.MathHelpers.IsEqual(_regenDelay, StaminaRegenDelay))
                {
                    _state = State.regen;
                    OnStaminaRecharging?.Invoke();
                }
                break;
            case State.regen:
                _stamina = Mathf.Min(_stamina + Time.deltaTime * StaminaRegenRate, MaxStam);
                if (DM.MathHelpers.IsEqual(_stamina, MaxStam))
                {
                    _state = _isDraining ? State.drain : State.idle;
                    OnStaminaRecharged?.Invoke();
                }
                break;
        }
    }

    public void SetStaminaSettings(float max, float regenDelay, float regenRate)
    {
        if (Mathf.Abs(_stamina - MaxStam) < DM.MathHelpers.FLT_EPSILON)
            _stamina = max;
        MaxStamina = max;
        StaminaRegenDelay = regenDelay;
        StaminaRegenRate = regenRate;
    }

    public void Drain()
    {
        _isDraining = true;
        if (_state == State.drain || _state == State.delay)
            return;

        _state = State.drain;
    }

    public void Regen()
    {
        _isDraining = false;
        if (_state != State.drain)
            return;

        _state = State.regen;
        OnStaminaRecharging?.Invoke();
    }
}
