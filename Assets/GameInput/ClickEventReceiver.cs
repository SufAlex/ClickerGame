using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    [RequireComponent(typeof(PlayerInput))]
    public class ClickEventReceiver : MonoBehaviour
    {
        private Vector2 _userPointerPosition;
        
        [UsedImplicitly]
        public void OnClick(InputAction.CallbackContext clickEventContext)
        {
            switch (clickEventContext.phase)
            {
                case InputActionPhase.Performed:
                    Debug.Log($"[Position] {_userPointerPosition}");
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