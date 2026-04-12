using RUS95.SpinWinEventApp.Core;

namespace RUS95.SpinWinEventApp.Systems
{
    public class FormState : IGameState
    {
        private readonly GameFlowController _controller;

        public FormState(GameFlowController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            var ui = _controller.GetUIManager();

            ui.ShowFormScreen(_controller.GetSessionData());

            ui.OnFormChanged += Validate;
            ui.OnSubmitClicked += HandleSubmit;
            ui.OnSkipClicked += HandleSkip;
        }

        public void Exit()
        {
            var ui = _controller.GetUIManager();

            ui.OnFormChanged -= Validate;
            ui.OnSubmitClicked -= HandleSubmit;
            ui.OnSkipClicked -= HandleSkip;
        }

        private void Validate(string name, string email)
        {
            bool isValid =
                !string.IsNullOrEmpty(name) &&
                email.EndsWith("@gmail.com");

            _controller.GetUIManager().SetSubmitButtonState(isValid);
        }

        private void HandleSubmit(string name, string email, string phone)
        {
            var data = _controller.GetSessionData();

            data.Name = name;
            data.Email = email;
            data.Phone = phone;

            _controller.SetState(new SubmittingState(_controller));
        }

        private void HandleSkip()
        {
            _controller.SetState(new ThankYouState(_controller));
        }
    }
}