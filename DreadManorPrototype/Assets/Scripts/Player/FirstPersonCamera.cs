using System;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    #region DataAdjustable
    [SerializeField] private float PitchClamp = 90f;
    [SerializeField] private Vector2 CameraSpeedMouse = Vector2.one;
    [SerializeField] private Vector2 CameraSpeedController = new Vector2(180f, 180f);
    #endregion

    [SerializeField] private FirstPersonController Controller;
    [SerializeField] private Transform Camera;

    private float _yaw; // 0 - 360
    private float _pitch;

    private void Awake()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        _pitch = euler.x;
        _yaw = euler.y;
    }


    void Update()
    {
        if (Controller != null)
        {
            float deltaRot = Controller.IsUsingMouse ? 1f : Time.deltaTime;
            Vector2 cameraSpeed = Controller.IsUsingMouse ? CameraSpeedMouse : CameraSpeedController;

            Vector2 lookInput = Controller.LookInput;
            _pitch -= lookInput.y * cameraSpeed.x * deltaRot;
            _yaw += lookInput.x * cameraSpeed.y * deltaRot;

            _pitch = Mathf.Clamp(_pitch, -PitchClamp, PitchClamp);
            _yaw = _yaw % 360f;
        }

        if (Camera != null) Camera.localRotation = Quaternion.Euler(_pitch, 0, 0);

        transform.rotation = Quaternion.Euler(0, _yaw, 0);
    }
}
