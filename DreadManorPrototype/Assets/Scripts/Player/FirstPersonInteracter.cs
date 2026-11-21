using UnityEngine;
using UnityEngine.Events;

using DM;

// Check for interaction every frame to enable highlighting

public class FirstPersonInteracter : MonoBehaviour
{
    [SerializeField] private PlayerSettings Settings;
    [SerializeField] private FirstPersonController Controller;

    public IInteractable HoveredInterable {  get; private set; }

    private void Awake()
    {
        Controller.OnInteractEvent += Interact;
    }

    private void OnDestroy()
    {
        Controller.OnInteractEvent -= Interact;
    }

    void Update()
    {
        IInteractable interactable;
        RaycastHit hitInfo;
        MathHelpers.SphereCastInfo castInfo = new MathHelpers.SphereCastInfo
        {
            position = transform.position,
            direction = transform.forward,
            radius = Settings.InteractSphereRadius,
            range = Settings.InteractRange,
            layers = Settings.InteractLayerMask
        };

        bool hitInteracble = MathHelpers.SphereCastForInterface(out interactable, out hitInfo, castInfo);

        if (hitInteracble)
        {
            if (interactable != HoveredInterable)
            {
                HoveredInterable?.UnHighlight(Controller.gameObject);
                HoveredInterable = interactable;
                HoveredInterable.Highlight(Controller.gameObject);
            }
        }
        else if (HoveredInterable != null)
        {
            HoveredInterable.UnHighlight(Controller.gameObject);
            HoveredInterable = null;
        }
    }
    void Interact()
    {
        HoveredInterable?.Interact(Controller.gameObject);
    }
}
