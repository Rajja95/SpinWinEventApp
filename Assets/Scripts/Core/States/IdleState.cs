using RUS95.SpinWinEventApp.Core;
using UnityEngine.InputSystem.LowLevel;

namespace RUS95.SpinWinEventApp.Systems
{
    public class IdleState : IGameState
    {
        private readonly GameFlowController _controller;

        public IdleState(GameFlowController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            var ui = _controller.GetUIManager();

            ui.ShowIdleScreen();
            ui.SetSpinButtonState(true, "SPIN");

            _controller.GetInputHandler().enabled = true;
            _controller.GetSpinController().OnSpinStarted += HandleSpinStarted;
        }

        public void Exit()
        {
            _controller.GetSpinController().OnSpinStarted -= HandleSpinStarted;
        }

        private void HandleSpinStarted()
        {
            _controller.SetState(new SpinState(_controller));
        }
    }
}