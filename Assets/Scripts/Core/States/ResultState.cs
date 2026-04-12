using RUS95.SpinWinEventApp.Core;

namespace RUS95.SpinWinEventApp.Systems
{
    public class ResultState : IGameState
    {
        private readonly GameFlowController _controller;

        public ResultState(GameFlowController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            var data = _controller.GetSessionData();
            var ui = _controller.GetUIManager();

            ui.ShowResultScreen(data);

            ui.OnContinueClicked += HandleContinue;
        }

        public void Exit()
        {
            _controller.GetUIManager().OnContinueClicked -= HandleContinue;
        }

        private void HandleContinue()
        {
            _controller.SetState(new FormState(_controller));
        }
    }
}