using DM;
using UnityEngine;

public class ExamineAttacher : MonoBehaviour
{
    [SerializeField] private ExamineController ExamineController;
    [SerializeField] private Light FirstPersonLight;
    [SerializeField] private Light ExamineLight;
    [SerializeField] private ExamineInfo ExamineInfo;
    private IExaminable _examineObject = null;
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

    public void Attach(IExaminable examineObject)
    {
        if (_examineObject != null)
        {
            Debug.LogError("ExamineAttacher: Can't attach when object is already attached");
            return;
        }

        GameObject gameObject;
        if (Helpers.GetGameObject(examineObject, out gameObject) == false)
            return;

        _examineObject = examineObject;
        ExamineInfo.ShowInfo(examineObject);
        _originalPosition = gameObject.transform.position;
        _originalRotation = gameObject.transform.rotation;
        gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        UseExamineLight(true);
    }

    public void Detach(IExaminable examineObject)
    {
        if (_examineObject != examineObject)
        {
            Debug.LogError("ExamineAttacher: Can't detach the wrong object");
            return;
        }

        GameObject gameObject;
        if (Helpers.GetGameObject(examineObject, out gameObject) == false)
            return;

        _examineObject = null;
        gameObject.transform.SetPositionAndRotation(_originalPosition, _originalRotation);
        UseExamineLight(false);
    }

    private void UseExamineLight(bool useExamine)
    {
        FirstPersonLight.gameObject.SetActive(!useExamine);
        ExamineLight.gameObject.SetActive(useExamine);
        ExamineInfo.gameObject.SetActive(useExamine);
    }
}
