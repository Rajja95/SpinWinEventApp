using RUS95.SpinWinEventApp.Systems;
using RUS95.SpinWinEventApp.Systems.Spin;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Core
{
    public class GameInitializer : MonoBehaviour
    {
        #region Fields

        [SerializeField] private WheelTapInputHandler _inputHandler;
        [SerializeField] private SpinController _spinController;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            InitializeSystems();
        }

        #endregion

        #region Private Methods

        private void InitializeSystems()
        {
            _inputHandler.Initialize(_spinController);
        }

        #endregion
    }
}