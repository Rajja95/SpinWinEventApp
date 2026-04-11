using UnityEngine;
using RUS95.SpinWinEventApp.Core;

namespace RUS95.SpinWinEventApp.Systems
{
    public class WheelTapInputHandler : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LayerMask _wheelLayer;

        private ISpinInputReceiver _spinReceiver;

        #endregion

        #region Public Methods

        public void Initialize(ISpinInputReceiver spinReceiver)
        {
            _spinReceiver = spinReceiver;
        }

        #endregion

        #region Unity Callbacks

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTap(Input.mousePosition);
            }
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

            _spinReceiver?.OnSpinRequested();
        }

        #endregion
    }
}