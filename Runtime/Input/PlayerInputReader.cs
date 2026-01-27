using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace PersonalPackage.Input
{
    [CreateAssetMenu(fileName = "PlayerInputReader", menuName = "Scriptable Objects/PlayerInputReader")]
    public class PlayerInputReader : ScriptableObject, IPlayerInputReader, IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<bool> Jump = delegate { };

        private PlayerInputActions inputActions;

        public Vector2 Direction => inputActions.Player.Move.ReadValue<Vector2>();
        public bool IsJumpPressed => inputActions.Player.Jump.IsPressed();

        public void EnablePlayerInputActions()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInputActions();
                inputActions.Player.SetCallbacks(this);
            }

            inputActions.Enable();
        }

        public void DisablePlayerInputActions()
        {
            inputActions?.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Jump.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Jump.Invoke(false);
                    break;
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            // nope
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            // nope
        }

        public void OnMouseControlCamera(InputAction.CallbackContext context)
        {
            // nope
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            // nope
        }
    }
}
