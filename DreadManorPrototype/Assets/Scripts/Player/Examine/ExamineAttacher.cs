using DM;
using UnityEngine;

public class ExamineAttacher : MonoBehaviour
{
    [SerializeField] private ExamineController ExamineController;
    [SerializeField] private Light FirstPersonLight;
    [SerializeField] private Light ExamineLight;
    [SerializeField] private ExamineInfo ExamineInfo;
    [SerializeField] private float RotateSpeed = 120f;
    private IExaminable _examineObject = null;
    private Transform _examineTransform;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    private void Start()
    {
        ExamineController.OnExamineEvent += Attach;
        ExamineController.OnExamineEndEvent += Detach;
        enabled = false;
    }

    private void OnDestroy()
    {
        ExamineController.OnExamineEvent -= Attach;
        ExamineController.OnExamineEndEvent -= Detach;
    }

    private void Update()
    {
        Vector2 rotation = Time.deltaTime * RotateSpeed * ExamineController.RotateInput;
        _examineTransform.localRotation *= Quaternion.Euler(rotation.y, rotation.x, 0f);
    }

    public void Attach(IExaminable examineObject)
    {
        if (_examineObject != null)
        {
            Debug.LogError("ExamineAttacher: Can't attach when object is already attached");
            return;
        }

        if (Helpers.GetGameObject(examineObject, out GameObject gameObject))
        {
            _examineObject = examineObject;
            _examineTransform = gameObject.transform;
            ExamineInfo.ShowInfo(examineObject);
            _originalPosition = gameObject.transform.position;
            _originalRotation = gameObject.transform.rotation;
            gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
            UseExamineLight(true);
            enabled = true;
        }
    }

    public void Detach(IExaminable examineObject)
    {
        if (_examineObject != examineObject)
        {
            Debug.LogError("ExamineAttacher: Can't detach the wrong object");
            return;
        }

        if (Helpers.GetGameObject(examineObject, out GameObject gameObject))
        {
            _examineTransform.localRotation = new Quaternion();
            _examineObject = null;
            _examineTransform = null;
            gameObject.transform.SetPositionAndRotation(_originalPosition, _originalRotation);
            UseExamineLight(false);
            enabled = false;
        }
    }

    private void UseExamineLight(bool useExamine)
    {
        FirstPersonLight.gameObject.SetActive(!useExamine);
        ExamineLight.gameObject.SetActive(useExamine);
        ExamineInfo.gameObject.SetActive(useExamine);
    }
}
