using RUS95.SpinWinEventApp.Core;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems
{
    public class SubmittingState : IGameState
    {
        private readonly GameFlowController _controller;

        public SubmittingState(GameFlowController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            var ui = _controller.GetUIManager();

            ui.SetSubmitButtonState(false, "Submitting...");

            // TODO: Save data locally (next task)
            Debug.Log("Saving data...");

            _controller.SetState(new ThankYouState(_controller));
        }

        public void Exit() { }
    }
}