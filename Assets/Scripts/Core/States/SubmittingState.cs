using RUS95.SpinWinEventApp.Core;
using RUS95.SpinWinEventApp.Systems.Persistence;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems
{
    public class SubmittingState : IGameState
    {
        private readonly GameFlowController _controller;
        private readonly GameDataService _dataService;
        private Coroutine _delayRoutine;

        public SubmittingState(GameFlowController controller)
        {
            _controller = controller;
            _dataService = new GameDataService();
        }

        public void Enter()
        {
            var ui = _controller.GetUIManager();
            var data = _controller.GetSessionData();

            ui.SetSubmitButtonState(false, "Submitting...");

            try
            {
                _dataService.Save(data);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to save data: " + ex.Message);
            }

            _delayRoutine = _controller.RunDelayed(_controller.SubmitDelay, () =>
            {
                _controller.SetState(new ThankYouState(_controller));
            });
        }

        public void Exit() 
        {
            if (_delayRoutine != null)
            {
                _controller.StopCoroutine(_delayRoutine);
            }
        }
    }
}