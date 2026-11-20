using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private PlayerSettings Settings;
    [SerializeField] private FirstPersonController Controller;
    [SerializeField] private Transform CameraTransform;

    private float _yaw; // 0 - 360
    private float _pitch;

    private void Awake()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        _pitch = euler.x;
        _yaw = euler.y;

        Controller.OnCrouchChanged += SetCrouchHeight;
    }

    private void OnDestroy()
    {
        Controller.OnCrouchChanged -= SetCrouchHeight;
    }

    void Update()
    {
        float deltaRot = Controller.Device == Player.InputDevice.mouse ? 1f : Time.deltaTime;
        Vector2 cameraSpeed = Settings.GetCameraSpeed(Controller.Device);

        Vector2 lookInput = Controller.LookInput;
        _pitch -= lookInput.y * cameraSpeed.x * deltaRot;
        _yaw += lookInput.x * cameraSpeed.y * deltaRot;

        _pitch = Mathf.Clamp(_pitch, -Settings.PitchClamp, Settings.PitchClamp);
        _yaw = _yaw % 360f;

        CameraTransform.localRotation = Quaternion.Euler(_pitch, 0, 0);
        transform.rotation = Quaternion.Euler(0, _yaw, 0);
    }

    private void SetCrouchHeight(bool isCrouching)
    {
        CameraTransform.localPosition = new Vector3(0f, isCrouching ? Settings.CrouchHeight : Settings.StandingHeight, 0f);
    }
}
