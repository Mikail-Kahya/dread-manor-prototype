using UnityEngine;

public interface IInteractable
{
    public void Interact(GameObject interacter);
    public void Highlight(GameObject interacter);
    public void UnHighlight(GameObject interacter);
}
