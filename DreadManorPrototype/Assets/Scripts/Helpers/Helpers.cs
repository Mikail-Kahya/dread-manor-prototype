using UnityEngine;

namespace DM
{
    public static class Helpers
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

        public static float InverseLerp(float current, float min, float max) // Returns alpha the lerp
        {
            return (current - min) / (max - min);
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
            

            bool hitObject = Physics.SphereCast(castInfo.position, castInfo.radius, castInfo.direction, out validHit, castInfo.range, castInfo.layers);

            if (hitObject)
            {
                T target = validHit.collider.GetComponentInParent<T>();
                if (target != null)
                {
                    foundInterface = target;
                    return true;
                }
            }
            validHit = default;
            return false;
        }

        public static bool GetGameObject(IExaminable examinable, out GameObject gameObject)
        {
            if (examinable is MonoBehaviour examineObject)
            {
                gameObject = examineObject.gameObject;
                return true;
            }
            gameObject = null;
            return false;
        }
    }
}