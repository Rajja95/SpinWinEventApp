using NUnit.Framework.Interfaces;
using RUS95.SpinWinEventApp.Core;
using RUS95.SpinWinEventApp.Data;

namespace RUS95.SpinWinEventApp.Systems
{
    public class SpinState : IGameState
    {
        private readonly GameFlowController _controller;

        public SpinState(GameFlowController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            var ui = _controller.GetUIManager();

            ui.SetSpinButtonState(false, "Spinning...");
            _controller.GetInputHandler().enabled = false;

            _controller.GetSpinController().OnSpinCompleted += HandleSpinCompleted;
        }

        public void Exit()
        {
            _controller.GetSpinController().OnSpinCompleted -= HandleSpinCompleted;
        }

        private void HandleSpinCompleted(SpinResult result)
        {
            _controller.GetSessionData().SetResult(result);
            _controller.SetState(new ResultState(_controller));
        }
    }
}