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

        public struct SphereCastInfo
        {
            public Vector3 position;
            public Vector3 direction;
            public float radius;
            public float range;
            public LayerMask layers; 
        }

        public static bool SphereCastForInterface<T>(out T foundInterface, out RaycastHit validHit, SphereCastInfo castInfo) where T : class
        {
            foundInterface = null;
            validHit = default;

            RaycastHit[] hits = Physics.SphereCastAll(castInfo.position, castInfo.radius, castInfo.direction, castInfo.range, castInfo.layers);

            System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
               
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out T target))
                {
                    foundInterface = target;
                    validHit = hit;
                    return true;
                }
            }

            return false;
        }
    }
}