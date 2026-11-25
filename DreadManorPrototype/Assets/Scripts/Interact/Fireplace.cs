using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Events;

public class Fireplace : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent OnWoodEvent;

    public void Interact(GameObject interacter)
    {
        if (interacter.TryGetComponent(out Inventory inventory) 
            && inventory.UseItem(ItemId.FireWood))
        {
            OnWoodEvent?.Invoke();
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