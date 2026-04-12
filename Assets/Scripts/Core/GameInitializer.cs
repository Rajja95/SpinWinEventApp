using UnityEngine;
using RUS95.SpinWinEventApp.Systems;
using RUS95.SpinWinEventApp.Systems.Spin;
using RUS95.SpinWinEventApp.UI;

namespace RUS95.SpinWinEventApp.Core
{
    public class GameInitializer : MonoBehaviour
    {
        #region Fields

        [Header("Core Systems")]
        [SerializeField] private GameFlowController _gameFlowController;

        [Header("Spin System")]
        [SerializeField] private SpinController _spinController;
        [SerializeField] private WheelTapInputHandler _inputHandler;

        [Header("UI")]
        [SerializeField] private UIManager _uiManager;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            InitializeSystems();
        }

        private void OnDestroy()
        {
            UnbindEvents();
        }

        #endregion

        #region Initialization

        private void InitializeSystems()
        {
            InitializeSpinSystem();
            BindUIEvents();
        }

        private void InitializeSpinSystem()
        {
            // Connect tap input ? spin controller
            _inputHandler.Initialize(_spinController);
        }

        private void BindUIEvents()
        {
            // UI button ? spin request
            _uiManager.OnSpinClicked += _spinController.OnSpinRequested;
        }

        private void UnbindEvents()
        {
            if (_uiManager != null)
            {
                _uiManager.OnSpinClicked -= _spinController.OnSpinRequested;
            }
        }

        #endregion
    }
}