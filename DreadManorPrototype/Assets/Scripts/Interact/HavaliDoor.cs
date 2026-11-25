using UnityEngine;
using UnityEngine.Events;

public class HavaliDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent OnOpenEvent;

    public void Interact(GameObject interacter)
    {
        if (interacter.TryGetComponent(out Inventory inventory) 
            && inventory.UseItem(ItemId.HavaliGem))
        {
            OnOpenEvent?.Invoke();
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