using UnityEngine;
using TMPro;
using RUS95.SpinWinEventApp.Data;
using RUS95.SpinWinEventApp.Systems.Spin;

namespace RUS95.SpinWinEventApp.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SpinController _spinController;
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private TextMeshProUGUI _resultText;

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            _spinController.OnSpinCompleted += HandleSpinCompleted;
        }

        private void OnDisable()
        {
            _spinController.OnSpinCompleted -= HandleSpinCompleted;
        }

        #endregion

        #region Event Handlers

        private void HandleSpinCompleted(SpinResult result)
        {
            ShowResult(result);
        }

        #endregion

        #region Private Methods

        private void ShowResult(SpinResult result)
        {
            _resultPanel.SetActive(true);

            if (result.Segment.IsWin())
            {
                _resultText.text = "YOU WIN!";
            }
            else
            {
                _resultText.text = "TRY AGAIN";
            }
        }

        #endregion
    }
}