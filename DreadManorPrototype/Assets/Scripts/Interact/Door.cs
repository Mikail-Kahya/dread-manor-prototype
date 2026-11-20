using UnityEngine;

namespace Assets.Scripts.Interact
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField, Tooltip("degrees")] private float OpenAngle = 90f;

        [SerializeField] private bool IsOpen = false;
        [SerializeField] private bool CanClose = true;

        public void Interact()
        {
            IsOpen = CanClose ? !IsOpen : true;
            SetDoorState(IsOpen);
        }

        public void Highlight()
        {
            Debug.Log("Highlight: " + gameObject.name);
        }

        public void UnHighlight()
        {
            Debug.Log("UnHighlight: " + gameObject.name);
        }

        private void OnValidate()
        {
            SetDoorState(IsOpen);
        }

        private void SetDoorState(bool isOpen)
        {
            transform.rotation = Quaternion.Euler(0f, isOpen ? OpenAngle : 0f, 0f);
        }
    }
}