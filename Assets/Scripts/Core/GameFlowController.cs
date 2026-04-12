using RUS95.SpinWinEventApp.Core;
using RUS95.SpinWinEventApp.Data;
using RUS95.SpinWinEventApp.Systems.Spin;
using RUS95.SpinWinEventApp.UI;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems
{
    public class GameFlowController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SpinController _spinController;
        [SerializeField] private WheelTapInputHandler _inputHandler;
        [SerializeField] private UIManager _uiManager;

        private IGameState _currentState;
        private GameSessionData _sessionData;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            _sessionData = new GameSessionData();
        }

        private void Start()
        {
            SetState(new IdleState(this));
        }

        #endregion

        #region Public Methods

        public void SetState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public SpinController GetSpinController() => _spinController;
        public WheelTapInputHandler GetInputHandler() => _inputHandler;
        public UIManager GetUIManager() => _uiManager;
        public GameSessionData GetSessionData() => _sessionData;

        #endregion
    }
}