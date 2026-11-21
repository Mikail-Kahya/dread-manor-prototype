using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour, IInteractable, IExaminable
{
    // Letter has script on a texture
    // Letter can be moved parallel to the camera in 3d
    // Letter can have a scrollable overlay
    [SerializeField, TextArea] private string Text;
    [SerializeField] TextMeshPro LetterText;

    public void Interact(GameObject interacter)
    {
        
    }
    public void Highlight(GameObject interacter)
    {
        Debug.Log("Highlight: " + gameObject.name);
    }

    public void UnHighlight(GameObject interacter)
    {
        Debug.Log("UnHighlight: " + gameObject.name);
    }

    public void Examine()
    {
        
    }

    private void OnValidate()
    {
        LetterText.text = Text;
    }
}
