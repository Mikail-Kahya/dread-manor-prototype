using System;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable, IExaminable
{
    [SerializeField] private ItemData Data;
    [SerializeField, TextArea] private string ExamineInfo;

    Inventory _inventory;

    public void Interact(GameObject interacter)
    {
        _inventory = interacter.GetComponent<Inventory>();
    }

    public void Highlight(GameObject interacter) { }

    public void UnHighlight(GameObject interacter) { }

    public void Examine()
    {
        
    }

    public void Release()
    {
        _inventory?.AddItem(Data);
        Destroy(gameObject);
    }

    public string GetInfo()
    {
        return ExamineInfo;
    }
}
