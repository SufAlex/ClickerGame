using JetBrains.Annotations;
using MonstersScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    [RequireComponent(typeof(PlayerInput))]
    public class ClickEventReceiver : MonoBehaviour
    {
        private Vector2 _userPointerPosition;
        [SerializeField] private Camera playerCamera;
        
        [UsedImplicitly]
        public void OnClick(InputAction.CallbackContext clickEventContext)
        {
            switch (clickEventContext.phase)
            {
                case InputActionPhase.Started:
                    //Debug.Log($"[Position] {_userPointerPosition}");
                    
                    var ray = playerCamera.ScreenPointToRay(_userPointerPosition);

                    if (Physics.Raycast(ray, out var hit)
                        && hit.transform.gameObject.GetComponent<IClickable>() != null)
                    {
                        Debug.Log($"[OnClick] Попал в монстра");
                        hit.transform.gameObject.GetComponent<IClickable>().OnClick();
                    }
                    break;
            }
        }
        
        [UsedImplicitly]
        public void OnPosition(InputAction.CallbackContext pointerPositionContext)
        {
            switch (pointerPositionContext.phase)
            {
                case InputActionPhase.Performed:
                    _userPointerPosition = pointerPositionContext.ReadValue<Vector2>();
                    break;
            }
        }
        
    }
}