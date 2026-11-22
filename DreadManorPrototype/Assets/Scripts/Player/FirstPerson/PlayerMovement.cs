using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerSettings Settings;
    [SerializeField] private FirstPersonController Controller;
    [SerializeField] private StaminaGuage PlayerStamina;
    [SerializeField] private CapsuleCollider Collider;

    private bool _isCrouching;

    private void Awake()
    {
        PlayerStamina.SetStaminaSettings(Settings.Stamina, Settings.StaminaRegenDelay, Settings.StaminaRegenRate);
        Controller.OnSprintChangedEvent += ToggleSprint;
        Controller.OnCrouchChangedEvent += ToggleCrouch;
    }

    private void OnDestroy()
    {
        Controller.OnSprintChangedEvent -= ToggleSprint;
        Controller.OnCrouchChangedEvent -= ToggleCrouch;
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
        if (PlayerStamina.IsDraining)
            return Settings.SprintSpeed;
        return Settings.WalkSpeed;
    }

    private void ToggleSprint(bool isSprinting)
    {
        if (isSprinting)
            PlayerStamina.Drain();
        else
            PlayerStamina.Regen();
    }

    private void ToggleCrouch(bool isCrouching)
    {
        _isCrouching = isCrouching;
        float newHeight = isCrouching ? Settings.CrouchColliderHeight : Settings.StandingColliderHeight;
        float currentHeight = Collider.height;
        Collider.height = newHeight;
        Collider.center += new Vector3(0f, (newHeight - currentHeight) * 0.5f, 0f);
    }
}
