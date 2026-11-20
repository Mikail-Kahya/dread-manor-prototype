using System.Collections.Generic;
using UnityEngine;

using Player;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "DreadManor/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Movement")]
    [Tooltip("m/s")] public float WalkSpeed = 5f;
    [Tooltip("m/s")] public float SprintSpeed = 8f;
    [Tooltip("m/s")] public float CrouchSpeed = 2.5f;
    [Tooltip("seconds")] public float Stamina = 4f;
    [Tooltip("seconds")] public float StaminaRegenDelay = 1f;
    [Tooltip("s/s")] public float StaminaRegenRate = 1f;

    [Header("Look")]
    [SerializeField] private List<CameraSpeed> CameraSpeeds = new List<CameraSpeed>
    {
        new CameraSpeed { device = InputDevice.mouse, speed = new Vector2(0.1f, 0.1f) },
        new CameraSpeed { device = InputDevice.gamepad, speed = new Vector2(180f, 180f) },
        new CameraSpeed { device = InputDevice.invalid, speed = new Vector2(1f, 1f) },
    };
    [Tooltip("degrees")] public float PitchClamp = 85f;

    [Header("Crouching")]
    [Tooltip("meters")] public float StandingHeight = 1.7f;
    [Tooltip("meters")] public float StandingColliderHeight = 1.8f;
    [Tooltip("meters")] public float CrouchHeight = 1f;
    [Tooltip("meters")] public float CrouchColliderHeight = 1.1f;


    public Vector2 GetCameraSpeed(InputDevice device)
    {
        return CameraSpeeds.Find(x => x.device == device).speed;
    }
}