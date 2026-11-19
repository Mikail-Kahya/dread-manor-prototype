using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region DataAdjustable
    [SerializeField] private float MovementSpeed = 5;
    [SerializeField] private float JumpHeight = 1;
    #endregion

    [SerializeField] private FirstPersonController Controller;

    void FixedUpdate()
    {
        if (Controller != null)
        {
            Vector2 moveInput = Controller.MoveInput;
            Vector3 movement = moveInput.y * transform.forward + moveInput.x * transform.right;
            transform.position += Time.fixedDeltaTime * MovementSpeed * movement;
        }
    }
}
