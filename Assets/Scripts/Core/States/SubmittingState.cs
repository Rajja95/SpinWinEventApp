using RUS95.SpinWinEventApp.Core;
using RUS95.SpinWinEventApp.Systems.Persistence;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems
{
    public class SubmittingState : IGameState
    {
        private readonly GameFlowController _controller;
        private readonly CsvDataSaver _dataSaver;

        public SubmittingState(GameFlowController controller)
        {
            _controller = controller;
            _dataSaver = new CsvDataSaver();
        }

        public void Enter()
        {
            var ui = _controller.GetUIManager();
            var data = _controller.GetSessionData();

            ui.SetSubmitButtonState(false, "Submitting...");

            try
            {
                _dataSaver.Save(data);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to save data: " + ex.Message);
            }

            _controller.SetState(new ThankYouState(_controller));
        }

        public void Exit() { }
    }
}