using System;
using UnityEngine;

namespace Player
{
    public enum InputDevice
    {
        mouse,
        gamepad,
        invalid
    }

    [Serializable]
    public struct CameraSpeed
    {
        public InputDevice device;
        public Vector2 speed;
    }
}