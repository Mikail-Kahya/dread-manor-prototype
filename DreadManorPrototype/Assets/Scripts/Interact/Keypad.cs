using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : MonoBehaviour, IInteractable
{
    [SerializeField] private BoxCollider Collider;
    [SerializeField] private TextMeshPro Display;
    [SerializeField] private int[] Code;
    [SerializeField] private UnityEvent OnCorrectCodeEvent;
    [SerializeField, Min(0.01f)] private float CodeCheckTime = 1f;

    private const int NR_COLS = 3;
    private const int NR_ROWS = 4;

    private Coroutine _displayRoutine;
    private int _currentResult; // Uses bitflags
    private int _codeIndex;

    public void Interact(GameObject interacter)
    {
        int key = GetKey(interacter);
        switch (key)
        {
            case -1:
                Debug.LogError(gameObject.name + ": The key could not be found");
                break;
            case 9:
                Cancel();
                break;
            case 10:
                AddKey(0);
                break;
            case 11:
                Confirm();
                break;
            default:
                AddKey(key + 1);
                break;
        }
    }

    public void Highlight(GameObject interacter)
    {
        Debug.Log("Highlight: " + gameObject.name);
    }

    public void UnHighlight(GameObject interacter)
    {
        Debug.Log("UnHighlight: " + gameObject.name);
    }

    private void OnValidate()
    {
        Display.text = string.Empty;
        foreach(int key in Code)
            AddKeyText(key);
    }

    private void Start()
    {
        Display.text = string.Empty;
    }

    private void AddKeyText(int key)
    {
        Display.text += key;
    }

    private void Confirm()
    {
        int codeLength = Code.Length;
        if (_codeIndex < codeLength)
            return;

        bool allCorrect = _currentResult == ((1 << codeLength) - 1);

        _displayRoutine = StartCoroutine(DisplayResult(allCorrect));
    }

    private IEnumerator DisplayResult(bool correct)
    {
        Display.text = new string(correct ? 'v' : 'x', Code.Length); ;
        Collider.enabled = false;
       
        yield return new WaitForSeconds(CodeCheckTime);

        Display.text = "";
        if (correct)
        {
            OnCorrectCodeEvent?.Invoke();
        }
        else
        {
            Collider.enabled = true;
            Cancel();
        }
        _displayRoutine = null;
    }

    private void Cancel()
    {
        _codeIndex = 0;
        _currentResult = 0;
        Display.text = string.Empty;
    }

    private void AddKey(int key)
    {
        if (_codeIndex >= Code.Length)
            return;

        bool isCorrect = Code[_codeIndex] == key;
        _currentResult = (_currentResult << 1) | Convert.ToInt32(isCorrect);
        _codeIndex++;
        AddKeyText(key);
    }

    private int GetKey(GameObject interacter) 
    {
        FirstPersonCamera camera = interacter.GetComponent<FirstPersonCamera>();

        if (camera == null)
        {
            Debug.LogError("Interacter does not have a camera.");
            return -1;
        }

        Ray ray = new Ray
        {
            direction = camera.Forward,
            origin = camera.Position
        };

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 localHit = hit.transform.InverseTransformPoint(hit.point);

            float halfWidth = Collider.size.x * 0.5f;
            float halfHeight = Collider.size.y * 0.5f;
            float x = Mathf.InverseLerp(
                Collider.center.x + halfWidth,
                Collider.center.x - halfWidth,
                localHit.x);
            float y = Mathf.InverseLerp(
                Collider.center.y + halfHeight,
                Collider.center.y - halfHeight,
                localHit.y);

            int column = Mathf.Clamp(Mathf.FloorToInt(x * NR_COLS), 0, 2);
            int row = Mathf.Clamp(Mathf.FloorToInt(y * NR_ROWS), 0, 3);

            return row * 3 + column;
        }
        return -1;
    }
}