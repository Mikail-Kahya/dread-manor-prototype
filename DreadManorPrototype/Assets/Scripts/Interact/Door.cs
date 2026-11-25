using UnityEngine;

[RequireComponent(typeof(TransformLerper))]
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private bool CanClose = true;
    
    private TransformLerper _lerper;
    private bool _isOpen = false;

    void Start()
    {
        _lerper = GetComponent<TransformLerper>();
    }

    public void Interact(GameObject interacter)
    {
        if (_isOpen && CanClose == false)
            return;

        if (_lerper.enabled == false)
        {
            _isOpen = !_isOpen;
            _lerper.enabled = true;
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
}