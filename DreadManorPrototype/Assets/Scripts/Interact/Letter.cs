using System;
using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour, IInteractable, IExaminable
{
    // Letter has script on a texture
    // Letter can be moved parallel to the camera in 3d
    // Letter can have a scrollable overlay
    [SerializeField, TextArea] private string Text;
    [SerializeField] TextMeshPro LetterText;

    ExamineController _controller;

    public void Interact(GameObject interacter)
    {
        _controller = interacter.GetComponent<ExamineController>();

        _controller.OnNextEvent += NextPage;
        _controller.OnPrevEvent += PrevPage;
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

    public void Release()
    {
        _controller.OnNextEvent -= NextPage;
        _controller.OnPrevEvent -= PrevPage;
    }

    public string GetInfo()
    {
        return Text;
    }

    private void OnValidate()
    {
        LetterText.text = Text;
    }

    private void NextPage()
    {
        LetterText.pageToDisplay = Math.Min(LetterText.pageToDisplay + 1, LetterText.textInfo.pageCount);
    }

    private void PrevPage()
    {
        LetterText.pageToDisplay = Math.Max(LetterText.pageToDisplay - 1, 0);
    }
}
