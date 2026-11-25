using UnityEngine;

using DM;
using UnityEngine.Events;

// Check for interaction every frame to enable highlighting

public class FirstPersonInteracter : MonoBehaviour
{
    [SerializeField] private PlayerSettings Settings;
    [SerializeField] private ControllerSwapper ControllerSwapper;

    public IInteractable HoveredInterable {  get; private set; }

    private void Start()
    {
        ControllerSwapper.PlayController.OnInteractEvent += Interact;
    }

    private void OnDestroy()
    {
        ControllerSwapper.PlayController.OnInteractEvent -= Interact;
    }

    void Update()
    {
        Helpers.SphereCastInfo castInfo = new Helpers.SphereCastInfo
        {
            position = transform.position,
            direction = transform.forward,
            radius = Settings.InteractSphereRadius,
            range = Settings.InteractRange,
            layers = Settings.InteractLayerMask
        };

        bool hitInteracble = Helpers.SphereCastForInterface(out IInteractable interactable, out RaycastHit hitInfo, castInfo);

        if (hitInteracble)
        {
            if (interactable != HoveredInterable)
            {
                HoveredInterable?.UnHighlight(ControllerSwapper.gameObject);
                HoveredInterable = interactable;
                HoveredInterable.Highlight(ControllerSwapper.gameObject);
            }
        }
        else if (HoveredInterable != null)
        {
            HoveredInterable.UnHighlight(ControllerSwapper.gameObject);
            HoveredInterable = null;
        }
    }
    void Interact()
    {
        if (HoveredInterable != null)
        {
            HoveredInterable.Interact(ControllerSwapper.gameObject);

            if (HoveredInterable is IExaminable examinable)
            {
                ControllerSwapper.ExamineController.AttachExamine(examinable);
                ControllerSwapper.Swap(ControllerSwapper.Mapping.examine);
                examinable.Examine();
            }
        }
    }
}
