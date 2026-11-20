using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerSettings Settings;
    [SerializeField] private FirstPersonController Controller;

    void FixedUpdate()
    {
        if (Controller != null)
        {
            Vector2 moveInput = Controller.MoveInput;
            Vector3 movement = moveInput.y * transform.forward + moveInput.x * transform.right;
            transform.position += Time.fixedDeltaTime * Settings.WalkSpeed * movement;
        }
    }
}
