using RUS95.SpinWinEventApp.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RUS95.SpinWinEventApp.Systems
{
    public class WheelTapInputHandler : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LayerMask _wheelLayer;

        private ISpinInputReceiver _spinReceiver;

        private bool _isInputEnabled = true;

        #endregion

        #region Public Methods

        public void Initialize(ISpinInputReceiver spinReceiver)
        {
            _spinReceiver = spinReceiver;
        }

        public void SetInputEnabled(bool enabled)
        {
            _isInputEnabled = enabled;
        }

        #endregion

        #region Unity Callbacks

        private void Update()
        {
            if (!_isInputEnabled) return;

            if (IsTapStarted(out Vector2 position))
            {
                HandleTap(position);
            }
        }

        #endregion

        #region Input Handling

        private bool IsTapStarted(out Vector2 position)
        {
            position = default;

            // Touch (Android)
            if (Touchscreen.current != null &&
                Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                position = Touchscreen.current.primaryTouch.position.ReadValue();
                return true;
            }

            // Mouse (Editor testing)
            if (Mouse.current != null &&
                Mouse.current.leftButton.wasPressedThisFrame)
            {
                position = Mouse.current.position.ReadValue();
                return true;
            }

            return false;
        }

        #endregion

        #region Private Methods

        private void HandleTap(Vector2 screenPosition)
        {
            Debug.Log("Tap detected at: " + screenPosition);
            Vector2 worldPoint = _mainCamera.ScreenToWorldPoint(screenPosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 5f, _wheelLayer);

            if (hit.collider == null)
            {
                Debug.Log("No hit detected on the wheel.");
                return;
            }
            else
            {
                Debug.Log("Hit detected on: " + hit.collider.name);
            }

                _spinReceiver?.OnSpinRequested();
        }

        #endregion
    }
}