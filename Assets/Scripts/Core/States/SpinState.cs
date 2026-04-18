using NUnit.Framework.Interfaces;
using RUS95.SpinWinEventApp.Core;
using RUS95.SpinWinEventApp.Data;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems
{
    public class SpinState : IGameState
    {
        private readonly GameFlowController _controller;
        private Coroutine _delayRoutine;

        public SpinState(GameFlowController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            var ui = _controller.GetUIManager();
            _controller.GetInputHandler().SetInputEnabled(false);

            ui.SetSpinButtonState(false, "Spinning...");
            _controller.GetInputHandler().enabled = false;

            _controller.GetSpinController().OnSpinCompleted += HandleSpinCompleted;
        }

        public void Exit()
        {
            _controller.GetSpinController().OnSpinCompleted -= HandleSpinCompleted;

            if (_delayRoutine != null)
            {
                _controller.StopCoroutine(_delayRoutine);
            }
        }

        private void HandleSpinCompleted(SpinResult result)
        {
            _controller.GetSessionData().SetResult(result);

            // Add delay before showing result
            _delayRoutine = _controller.RunDelayed(_controller.ResultDelay, () =>
            {
                _controller.SetState(new ResultState(_controller));
            });
        }
    }
}