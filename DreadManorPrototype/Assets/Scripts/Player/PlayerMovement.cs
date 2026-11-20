using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerSettings Settings;
    [SerializeField] private FirstPersonController Controller;
    [SerializeField] private StaminaGuage PlayerStamina;

    bool _isSprinting;
    bool _isCrouching;

    private void Awake()
    {
        PlayerStamina.SetStaminaSettings(Settings.Stamina, Settings.StaminaRegenDelay, Settings.StaminaRegenRate);
        Controller.OnSprintChanged += ToggleSprint;
    }

    private void OnDestroy()
    {
        Controller.OnSprintChanged -= ToggleSprint;
    }

    void FixedUpdate()
    {
        Vector2 moveInput = Controller.MoveInput;
        Vector3 movement = moveInput.y * transform.forward + moveInput.x * transform.right;
        transform.position += Time.fixedDeltaTime * GetSprintSpeed() * movement;
    }

    private float GetSprintSpeed()
    {
        if (_isCrouching)
            return Settings.CrouchSpeed;
        if (_isSprinting && PlayerStamina.IsDraining)
            return Settings.SprintSpeed;
        return Settings.WalkSpeed;
    }

    private void ToggleSprint(bool isSprinting)
    {
        _isSprinting = isSprinting;
        if (isSprinting)
            PlayerStamina.Drain();
        else
            PlayerStamina.Regen();
    }
}
