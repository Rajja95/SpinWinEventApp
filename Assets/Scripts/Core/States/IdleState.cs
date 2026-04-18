using RUS95.SpinWinEventApp.Core;
using System.Diagnostics;
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
            ui.SetSpinButtonState(true, "TAP TO SPIN");

            ui.SetAdminButtonState(true);

            _controller.GetInputHandler().enabled = true;
            _controller.GetSpinController().OnSpinStarted += HandleSpinStarted;

            ui.OnAdminClicked += HandleAdminClicked;
        }

        public void Exit()
        {
            var ui = _controller.GetUIManager();
            _controller.GetSpinController().OnSpinStarted -= HandleSpinStarted;

            ui.OnAdminClicked -= HandleAdminClicked;
        }

        private void HandleSpinStarted()
        {
            _controller.GetUIManager().SetAdminButtonState(false);
            _controller.SetState(new SpinState(_controller));
        }

        private void HandleAdminClicked()
        {
            _controller.SetState(new AdminAuthState(_controller));
        }
    }
}