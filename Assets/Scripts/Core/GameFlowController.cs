using RUS95.SpinWinEventApp.Core;
using RUS95.SpinWinEventApp.Data;
using RUS95.SpinWinEventApp.Systems.Spin;
using RUS95.SpinWinEventApp.UI;
using System.Collections;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems
{
    public class GameFlowController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SpinController _spinController;
        [SerializeField] private WheelTapInputHandler _inputHandler;
        [SerializeField] private UIManager _uiManager;

        [SerializeField] private float _resultDelay = 1.5f;
        [SerializeField] private float _submitDelay = 1.5f;

        private IGameState _currentState;
        private GameSessionData _sessionData;

        public float ResultDelay => _resultDelay;
        public float SubmitDelay => _submitDelay;

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

        public Coroutine RunDelayed(float delay, System.Action action)
        {
            return StartCoroutine(DelayRoutine(delay, action));
        }

        public SpinController GetSpinController() => _spinController;
        public WheelTapInputHandler GetInputHandler() => _inputHandler;
        public UIManager GetUIManager() => _uiManager;
        public GameSessionData GetSessionData() => _sessionData;

        #endregion

        #region Private Methods

        private IEnumerator DelayRoutine(float delay, System.Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        #endregion
    }
}