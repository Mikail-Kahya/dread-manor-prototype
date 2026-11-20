using System.Collections;
using UnityEngine;

namespace DM
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

        public static bool IsEqual(float val1, float val2, float epsilon = FLT_EPSILON)
        {
            return Mathf.Abs(val1 - val2) < epsilon;
        }
    }
}