using System;
using System.Collections.Generic;
using UnityEngine;

using InputDevice = FirstPersonController.InputDevice;

public class FirstPersonCamera : MonoBehaviour
{
    [Serializable]
    public struct CameraSpeed
    {
        public InputDevice device;
        public Vector2 speed;
    }

    #region DataAdjustable
    [SerializeField] private float PitchClamp = 90f;
    [SerializeField]
    private List<CameraSpeed> CameraSpeeds = new List<CameraSpeed>
    {
        new CameraSpeed { device = InputDevice.mouse, speed = new Vector2(0.1f, 0.1f) },
        new CameraSpeed { device = InputDevice.gamepad, speed = new Vector2(180f, 180f) }
    };
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
            float deltaRot = Controller.Device == InputDevice.mouse ? 1f : Time.deltaTime;
            Vector2 cameraSpeed = CameraSpeeds.Find(speed => speed.device == Controller.Device).speed;

            Vector2 lookInput = Controller.LookInput;
            _pitch -= lookInput.y * cameraSpeed.x * deltaRot;
            _yaw += lookInput.x * cameraSpeed.y * deltaRot;

            _pitch = Mathf.Clamp(_pitch, -PitchClamp, PitchClamp);
            _yaw = _yaw % 360f;
        }

        if (Camera != null) 
            Camera.localRotation = Quaternion.Euler(_pitch, 0, 0);

        transform.rotation = Quaternion.Euler(0, _yaw, 0);
    }
}
