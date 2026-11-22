using UnityEngine;

public class ExamineAttacher : MonoBehaviour
{
    [SerializeField] private ExamineController ExamineController;
    [SerializeField] private Light FirstPersonLight;
    [SerializeField] private Light ExamineLight;
    private GameObject _examineObject = null;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    private void Start()
    {
        ExamineController.OnExamineEvent += Attach;
        ExamineController.OnExamineEndEvent += Detach;
    }

    private void OnDestroy()
    {
        ExamineController.OnExamineEvent -= Attach;
        ExamineController.OnExamineEndEvent -= Detach;
    }

    public void Attach(GameObject examineObject)
    {
        if (_examineObject != null)
        {
            Debug.LogError("ExamineAttacher: Can't attach when object is already attached");
            return;
        }

        _originalPosition = examineObject.transform.position;
        _originalRotation = examineObject.transform.rotation;
        _examineObject = examineObject;
        _examineObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        UseExamineLight(true);
    }

    public void Detach(GameObject examineObject)
    {
        if (_examineObject != examineObject)
        {
            Debug.LogError("ExamineAttacher: Can't detach the wrong object");
            return;
        }

        _examineObject.transform.SetPositionAndRotation(_originalPosition, _originalRotation);
        _examineObject = null;
        UseExamineLight(false);
    }

    private void UseExamineLight(bool useExamine)
    {
        FirstPersonLight.gameObject.SetActive(!useExamine);
        ExamineLight.gameObject.SetActive(useExamine);
    }
}
