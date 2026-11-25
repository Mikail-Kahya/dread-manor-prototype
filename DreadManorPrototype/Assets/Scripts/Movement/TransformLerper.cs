using DM;
using UnityEngine;

// Move between two states using easing over a duration
// Don't place the start and end as a child of the owner of this script
public class TransformLerper : MonoBehaviour
{
    public enum Direction { Backward = -1, Forward = 1 }

    [Header("References")]
    [SerializeField] private Transform Target;
    [SerializeField] private Transform Begin;
    [SerializeField] private Transform End;

    [Header("Movement")]
    [SerializeField, Min(0.001f)] private float Duration = 2f;
    [SerializeField] private Easing.EasingType EasingType = Easing.EasingType.Linear;
    [SerializeField, Range(0f, 1f)] private float StartAlpha = 0f;
    [SerializeField] private Direction StartDirection = Direction.Forward;
    [SerializeField] private bool CanReverse = true;
    [SerializeField] private bool CanLoop = true;

    private float _currentAlpha = 0f;
    private Direction _direction;
    private bool _finished = false;

    private void OnValidate()
    {
        if (Target != null && IsInvalidTarget())
        {
            Debug.LogError(
                "The Target cannot be the TransformLerper object or one of its children.",
                this);
            Target = null;
        }

        if (Begin != null && End != null && Target != null)
            SetTransform(StartAlpha);
    }

    private void Start()
    {
        _direction = StartDirection;
        _currentAlpha = StartAlpha;
    }

    private void Update()
    {
        int direction = (int)_direction;
        _currentAlpha = Mathf.Clamp01(_currentAlpha + direction * Time.deltaTime / Duration);
        SetTransform(_currentAlpha);

        if (IsFinished(_direction))
        {
            if (CanReverse)
                _direction = (Direction)(-direction);
            else
                _currentAlpha = _direction == Direction.Forward ? 0f : 1f;

            if (CanLoop == false)
            {
                enabled = false;
                return;
            }
        }
    }

    public void SetDirection(Direction direction) => _direction = direction;
    public void SetAlpha(float alpha) => _currentAlpha = Mathf.Clamp01(alpha);

    private bool IsFinished(Direction direction)
    {
        if (direction == Direction.Forward && Helpers.IsEqual(_currentAlpha, 1f))
            return true;
        if (direction == Direction.Backward && Helpers.IsEqual(_currentAlpha, 0f))
            return true;
        return false;
    }

    private void SetTransform(float alpha)
    {
        if (Target == null || Begin == null || End == null)
            return;

        float easedAlpha = Easing.Ease(alpha, EasingType);

        Target.position = Vector3.Lerp(
            Begin.position,
            End.position,
            easedAlpha);

        Target.rotation = Quaternion.Lerp(
            Begin.rotation,
            End.rotation,
            easedAlpha);
    }

    [ContextMenu("Create Begin and End")]
    private void CreateReferenceTransforms()
    {
        if (Begin == null)
            Begin = CreateReference("Begin");

        if (End == null)
            End = CreateReference("End");
    }

    private Transform CreateReference(string objectName)
    {
        GameObject reference = new GameObject(objectName);
        reference.transform.SetParent(transform, false);
        return reference.transform;
    }

    private bool IsInvalidTarget()
    {
        return Target == Begin || Target == End || Begin.IsChildOf(Target) || End.IsChildOf(Target);
    }


#if UNITY_EDITOR
    private Vector3 _EDITOR_beginPos;
    private Quaternion _EDITOR_beginRot;

    private Vector3 _EDITOR_endPos;
    private Quaternion _EDITOR_endRot;

    private void OnDrawGizmos()
    {
        if (Begin == null || End == null)
            return;

        bool beginChanged =
            Begin.position != _EDITOR_beginPos ||
            Begin.rotation != _EDITOR_beginRot;

        bool endChanged =
            End.position != _EDITOR_endPos ||
            End.rotation != _EDITOR_endRot;

        if (beginChanged || endChanged)
        {
            SetTransform(StartAlpha);
            CacheReferenceTransforms();
        }
    }

    private void CacheReferenceTransforms()
    {
        _EDITOR_beginPos = Begin.position;
        _EDITOR_beginRot = Begin.rotation;

        _EDITOR_endPos = End.position;
        _EDITOR_endRot = End.rotation;
    }
#endif
}