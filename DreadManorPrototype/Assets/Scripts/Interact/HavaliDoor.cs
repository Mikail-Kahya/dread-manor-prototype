using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HavaliDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private TransformLerper GemSlottingLerp;
    [SerializeField] private UnityEvent OnOpenEvent;

    private Coroutine _gemSlottingRoutine;

    public void Interact(GameObject interacter)
    {
        if (interacter.TryGetComponent(out Inventory inventory) 
            && inventory.UseItem(ItemId.HavaliGem))
        {
            _gemSlottingRoutine = StartCoroutine(SlotGem());
        }
    }

    private IEnumerator SlotGem()
    {
        GemSlottingLerp.gameObject.SetActive(true);
        yield return new WaitForSeconds(GemSlottingLerp.AnimationDuration);
        OnOpenEvent?.Invoke();
        StopCoroutine(_gemSlottingRoutine);
        _gemSlottingRoutine = null;
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