using UnityEngine;
using UnityEngine.Events;

using DM;

public class FirstPersonInteracter : MonoBehaviour
{
    [SerializeField] private PlayerSettings Settings;
    [SerializeField] private FirstPersonController Controller;
    [SerializeField] private UnityEvent<IInteractable> OnHoverInteractable;

    void Start()
    {
        
    }

    
    void Update()
    {
        IInteractable interactable;
        RaycastHit hitInfo;
        MathHelpers.SphereCastInfo castInfo = new MathHelpers.SphereCastInfo {
            position = transform.position,
            direction = transform.forward,
            radius = Settings.InteractSphereRadius,
            range = Settings.InteractRange,
            layers = Settings.InteractLayerMask
        };

        bool hitInteracble = MathHelpers.SphereCastForInterface<IInteractable>(out interactable, out hitInfo, castInfo);
        
        if (hitInteracble)
        {
            Debug.Log(hitInfo.collider.gameObject.name);
        }
    }
}
