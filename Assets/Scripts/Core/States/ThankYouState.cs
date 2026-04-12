using RUS95.SpinWinEventApp.Core;
using UnityEngine;
using System.Collections;

namespace RUS95.SpinWinEventApp.Systems
{
    public class ThankYouState : IGameState
    {
        private readonly GameFlowController _controller;

        public ThankYouState(GameFlowController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            _controller.GetUIManager().ShowThankYouScreen();
            _controller.StartCoroutine(Countdown());
        }

        public void Exit() { }

        private IEnumerator Countdown()
        {
            var ui = _controller.GetUIManager();

            for (int i = 3; i > 0; i--)
            {
                ui.SetCountdownText($"Play again in {i}...");
                yield return new WaitForSeconds(1f);
            }

            _controller.GetSessionData().Reset();
            _controller.SetState(new IdleState(_controller));
        }
    }
}