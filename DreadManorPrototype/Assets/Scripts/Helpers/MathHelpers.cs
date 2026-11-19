using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class MathHelpers
    {
        public const float FLT_EPSILON = 0.0001f;
        public static bool IsNearlyZero(Vector3 vec, float epsilon = FLT_EPSILON)
        {
            return vec.sqrMagnitude < epsilon;
        }
        public static bool IsNearlyZero(Vector2 vec, float epsilon = FLT_EPSILON)
        {
            return vec.sqrMagnitude < epsilon;
        }
    }
}