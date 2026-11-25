using System;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable, IExaminable
{
    [SerializeField] private ItemData Data;
    [SerializeField, TextArea] private string ExamineInfo;

    ExamineController _controller;

    public void Interact(GameObject interacter)
    {
        _controller = interacter.GetComponent<ExamineController>();
        _controller.OnNextEvent += Release;
    }

    public void Highlight(GameObject interacter) { }

    public void UnHighlight(GameObject interacter) { }

    public void Examine()
    {
        
    }

    public void Release()
    {
        _controller.OnNextEvent -= Release;

        if (_controller.TryGetComponent(out Inventory inventory))
        {
            inventory.AddItem(Data);
        }
        Destroy(gameObject);
    }

    public string GetInfo()
    {
        return ExamineInfo;
    }
}
