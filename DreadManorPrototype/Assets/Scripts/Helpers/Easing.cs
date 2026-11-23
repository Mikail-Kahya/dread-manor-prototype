using UnityEngine;

namespace DM
{
    // Use of https://easings.net
    public static class Easing
    {
        public enum EasingType
        {
            Linear,

            InSine,
            OutSine,
            InOutSine,

            InQuad,
            OutQuad,
            InOutQuad,

            InCubic,
            OutCubic,
            InOutCubic,

            InQuart,
            OutQuart,
            InOutQuart,

            InQuint,
            OutQuint,
            InOutQuint,

            InExpo,
            OutExpo,
            InOutExpo,

            InCirc,
            OutCirc,
            InOutCirc,

            InBack,
            OutBack,
            InOutBack,

            InElastic,
            OutElastic,
            InOutElastic,

            InBounce,
            OutBounce,
            InOutBounce
        }

        public static float Ease(float x, EasingType type)
        {
            return type switch
            {
                EasingType.Linear => x,
                EasingType.InSine => InSine(x),
                EasingType.OutSine => OutSine(x),
                EasingType.InOutSine => InOutSine(x),
                EasingType.InQuad => InQuad(x),
                EasingType.OutQuad => OutQuad(x),
                EasingType.InOutQuad => InOutQuad(x),
                EasingType.InCubic => InCubic(x),
                EasingType.OutCubic => OutCubic(x),
                EasingType.InOutCubic => InOutCubic(x),
                EasingType.InQuart => InQuart(x),
                EasingType.OutQuart => OutQuart(x),
                EasingType.InOutQuart => InOutQuart(x),
                EasingType.InQuint => InQuint(x),
                EasingType.OutQuint => OutQuint(x),
                EasingType.InOutQuint => InOutQuint(x),
                EasingType.InExpo => InExpo(x),
                EasingType.OutExpo => OutExpo(x),
                EasingType.InOutExpo => InOutExpo(x),
                EasingType.InCirc => InCirc(x),
                EasingType.OutCirc => OutCirc(x),
                EasingType.InOutCirc => InOutCirc(x),
                EasingType.InBack => InBack(x),
                EasingType.OutBack => OutBack(x),
                EasingType.InOutBack => InOutBack(x),
                EasingType.InElastic => InElastic(x),
                EasingType.OutElastic => OutElastic(x),
                EasingType.InOutElastic => InOutElastic(x),
                EasingType.InBounce => InBounce(x),
                EasingType.OutBounce => OutBounce(x),
                EasingType.InOutBounce => InOutBounce(x),
                _ => x
            };
        }

        public static float Linear(float x) => x;
        public static float InSine(float x) => 1 - Mathf.Cos((x * Mathf.PI) * 0.5f);
        public static float OutSine(float x) => Mathf.Sin((x * Mathf.PI) * 0.5f);
        public static float InOutSine(float x) => -(Mathf.Cos(Mathf.PI* x) - 1) * 0.5f;
        public static float InQuad(float x) => x * x;
        public static float OutQuad(float x)
        {
            float x2 = 1 - x;
            return 1 - x2 * x2;
        }
        public static float InOutQuad(float x)
        {
            if (x < 0.5f)
                return 2 * x * x;
            
            float x2 = -2 * x + 2;
            return 1 - x2 * x2 * 0.5f;
        }
        public static float InCubic(float x) => x * x * x;
        public static float OutCubic(float x)
        {
            float x2 = 1 - x;
            return 1 - x2 * x2 * x2;
        }
        public static float InOutCubic(float x)
        {
            if (x < 0.5f)
                return 4 * x * x * x;

            float x2 = -2 * x + 2;
            return 1 - x2 * x2 * x2 * 0.5f;
        }
        public static float InQuart(float x) => x * x * x * x;
        public static float OutQuart(float x)
        {
            float x2 = 1 - x;
            return 1 - x2 * x2 * x2 * x2;
        }
        public static float InOutQuart(float x)
        {
            if (x < 0.5f)
                return 8 * x * x * x * x;

            float x2 = -2 * x + 2;
            return 1 - x2 * x2 * x2 * x2 * 0.5f;
        }
        public static float InQuint(float x) => x * x * x * x * x;
        public static float OutQuint(float x)
        {
            float x2 = 1 - x;
            return 1 - x2 * x2 * x2 * x2 * x2;
        }
        public static float InOutQuint(float x)
        {
            if (x < 0.5f)
                return 4 * x * x * x * x * x;

            float x2 = -2 * x + 2;
            return 1 - x2 * x2 * x2 * x2 * x2 * 0.5f;
        }
        public static float InExpo(float x)
        {
            if (x == 0)
                return 0;
            return Mathf.Pow(2, 10 * x - 10);
        }
        public static float OutExpo(float x)
        {
            if (x == 1)
                return 1;
            return 1 - Mathf.Pow(2, -10 * x);
        }
        public static float InOutExpo(float x)
        {
            if (x == 0)
                return 0;
            if (x == 1)
                return 1;
            if (x < 0.5f)
                return Mathf.Pow(2, 20 * x - 10) * 0.5f;
            return (2 - Mathf.Pow(2, -20 * x + 10)) * 0.5f;
        }
        public static float InCirc(float x)
        {
            return 1 - Mathf.Sqrt(1 - x * x);
        }
        public static float OutCirc(float x)
        {
            float x2 = x - 1;
            return Mathf.Sqrt(1 - x2 * x2);
        }
        public static float InOutCirc(float x)
        {
            if (x < 0.5f)
                return (1 - Mathf.Sqrt(1 - 4 * x * x)) * 0.5f;

            float t = -2 * x + 2;
            return (Mathf.Sqrt(1 - t * t) + 1) * 0.5f;
        }
        public static float InBack(float x)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1;

            return c3 * x * x * x - c1 * x * x;
        }
        public static float OutBack(float x)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1;
            float x2 = x - 1;

            return 1 + c3 * x2 * x2 * x2 + c1 * x2 * x2;
        }
        public static float InOutBack(float x)
        {
            const float c1 = 1.70158f;
            const float c2 = c1 * 1.525f;

            if (x < 0.5f)
            {
                float x2 = 2 * x;
                return (x2 * x2 * ((c2 + 1) * 2 * x - c2)) * 0.5f;
            }
            else
            {
                float x2 = 2 * x - 2;
                return (x2 * x2 * ((c2 + 1) * (x * 2 - 2) + c2) + 2) * 0.5f;
            }
        }
        public static float InElastic(float x)
        {
            const float c4 = (2 * Mathf.PI) / 3;

            if (x == 0)
                return 0;
            if (x == 1)
                return 1;
            return -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
        }
        public static float OutElastic(float x)
        {
            const float c4 = (2 * Mathf.PI) / 3;
            if (x == 0)
                return 0;
            if (x == 1)
                return 1;
            return Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
        }
        public static float InOutElastic(float x)
        {
            const float c5 = (2 * Mathf.PI) / 4.5f;

            if (x == 0)
                return 0;
            if (x == 1)
                return 1;
            if (x < 0.5f)
                return -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) * 0.5f;
            return (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) * 0.5f + 1;
        }
        public static float InBounce(float x) => 1 - OutBounce(1 - x);
        public static float OutBounce(float x)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;


            if (x < 1f / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2f / d1)
            {
                float t = x - 1.5f / d1;
                return n1 * t * t + 0.75f;
            }
            else if (x < 2.5f / d1)
            {
                float t = x - 2.25f / d1;
                return n1 * t * t + 0.9375f;
            }
            else
            {
                float t = x - 2.625f / d1;
                return n1 * t * t + 0.984375f;
            }
        }
        public static float InOutBounce(float x)
        {
            if (x < 0.5f)
                return (1 - OutBounce(1 - 2 * x)) * 0.5f;
            return (1 + OutBounce(2 * x - 1)) * 0.5f;
        }
    }
}