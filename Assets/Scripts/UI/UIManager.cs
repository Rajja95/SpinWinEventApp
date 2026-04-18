using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RUS95.SpinWinEventApp.Data;

namespace RUS95.SpinWinEventApp.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Fields

        [Header("Screens")]
        [SerializeField] private GameObject _idleScreen;
        [SerializeField] private GameObject _resultScreen;
        [SerializeField] private GameObject _formScreen;
        [SerializeField] private GameObject _thankYouScreen;
        [SerializeField] private GameObject _adminScreen;

        #region Idle

        [Header("Idle Screen")]
        [SerializeField] private Button _spinButton;
        [SerializeField] private Button _adminButton;
        [SerializeField] private TextMeshProUGUI _spinButtonText;

        #endregion

        #region Result

        [Header("Result Screen")]
        [SerializeField] private Image _resultImage;
        [SerializeField] private TextMeshProUGUI _resultTitle;
        [SerializeField] private TextMeshProUGUI _resultSubText;
        [SerializeField] private Button _continueButton;

        [Header("Result Config")]
        [SerializeField] private Sprite _winSprite;
        [SerializeField] private Sprite _loseSprite;
        [SerializeField] private string _winTitle = "YOU WIN!";
        [SerializeField] private string _loseTitle = "TRY AGAIN";
        [SerializeField] private string _winSub = "Congratulations!";
        [SerializeField] private string _loseSub = "Better luck next time";

        #endregion

        #region Form

        [Header("Form Screen")]
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private TMP_InputField _emailInput;
        [SerializeField] private TMP_InputField _phoneInput;
        [SerializeField] private GameObject _winOnlyObject;

        [SerializeField] private Button _submitButton;
        [SerializeField] private TextMeshProUGUI _submitButtonText;
        [SerializeField] private Button _skipButton;

        [Header("Button Colors")]
        [SerializeField] private Image _submitButtonImage;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        #endregion

        #region Thank You

        [Header("Thank You Screen")]
        [SerializeField] private TextMeshProUGUI _countdownText;

        #endregion

        #region Admin

        [SerializeField] private GameObject _adminLoginGroup;   // password UI
        [SerializeField] private GameObject _adminPanelGroup;   // download buttons etc
        [SerializeField] private TMP_InputField _passwordInput;
        [SerializeField] private TextMeshProUGUI _errorText;
        [SerializeField] private Button _adminSubmitButton;
        [SerializeField] private Button _adminCancelButton;
        [SerializeField] private Button _adminDownloadButton;
        [SerializeField] private Color _errorColor = Color.red;
        [SerializeField] private Color _successColor = Color.green;

        #endregion

        #endregion

        #region Events

        public event Action OnSpinClicked;
        public event Action OnContinueClicked;
        public event Action<string, string> OnFormChanged;
        public event Action<string, string, string> OnSubmitClicked;
        public event Action OnSkipClicked;
        public event Action OnAdminClicked;
        public event Action<string> OnAdminSubmit;
        public event Action OnAdminCancel;
        public event Action OnAdminDownloadClicked;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            BindUI();
        }

        #endregion

        #region Initialization

        private void BindUI()
        {
            _spinButton.onClick.AddListener(() => OnSpinClicked?.Invoke());
            _continueButton.onClick.AddListener(() => OnContinueClicked?.Invoke());
            _submitButton.onClick.AddListener(HandleSubmitClicked);
            _skipButton.onClick.AddListener(() => OnSkipClicked?.Invoke());

            _nameInput.onValueChanged.AddListener(_ => NotifyFormChanged());
            _emailInput.onValueChanged.AddListener(_ => NotifyFormChanged());

            _adminSubmitButton.onClick.AddListener(() =>
            {
                OnAdminSubmit?.Invoke(_passwordInput.text);
            });

            _adminCancelButton.onClick.AddListener(() =>
            {
                OnAdminCancel?.Invoke();
            });

            _adminButton.onClick.AddListener(() =>
            {
                OnAdminClicked?.Invoke();
            });

            _adminDownloadButton.onClick.AddListener(() =>
            {
                OnAdminDownloadClicked?.Invoke();
            });
        }

        #endregion

        #region Screen Control

        public void ShowIdleScreen()
        {
            HideAll();
            _idleScreen.SetActive(true);
        }

        public void ShowResultScreen(GameSessionData data)
        {
            HideAll();
            _resultScreen.SetActive(true);

            if (data.IsWin)
            {
                _resultImage.sprite = _winSprite;
                _resultTitle.text = _winTitle;
                _resultSubText.text = _winSub;
            }
            else
            {
                _resultImage.sprite = _loseSprite;
                _resultTitle.text = _loseTitle;
                _resultSubText.text = _loseSub;
            }
        }

        public void ShowFormScreen(GameSessionData data)
        {
            HideAll();
            _formScreen.SetActive(true);

            ResetForm();
            SetSubmitButtonState(false);

            SetSubmitButtonState(false, "Enter Draw");
            _winOnlyObject?.SetActive(data.IsWin);
        }

        public void ShowThankYouScreen()
        {
            HideAll();
            _thankYouScreen.SetActive(true);
        }

        private void HideAll()
        {
            _idleScreen.SetActive(false);
            _resultScreen.SetActive(false);
            _formScreen.SetActive(false);
            _adminScreen.SetActive(false);
            _thankYouScreen.SetActive(false);
        }

        public void ShowAdminScreen()
        {
            HideAll();
            _adminScreen.SetActive(true);

            _adminLoginGroup.SetActive(true);
            _adminPanelGroup.SetActive(false);

            _passwordInput.text = "";
            _errorText.text = "";
        }

        public void ShowAdminPanel()
        {
            _errorText.text = "";
            _adminLoginGroup.SetActive(false);
            _adminPanelGroup.SetActive(true);
        }

        #endregion

        #region Button States

        public void SetSpinButtonState(bool enabled, string text)
        {
            _spinButton.interactable = enabled;
            _spinButtonText.text = text;
        }

        public void SetSubmitButtonState(bool enabled, string text = null)
        {
            _submitButton.interactable = enabled;

            if (!string.IsNullOrEmpty(text))
                _submitButtonText.text = text;

            _submitButtonImage.color = enabled ? _activeColor : _inactiveColor;
        }

        #endregion

        #region Form

        private void NotifyFormChanged()
        {
            OnFormChanged?.Invoke(_nameInput.text, _emailInput.text);
        }

        private void HandleSubmitClicked()
        {
            SetSubmitButtonState(false, "Submitting...");

            OnSubmitClicked?.Invoke(
                _nameInput.text,
                _emailInput.text,
                _phoneInput.text
            );
        }

        private void ResetForm()
        {
            _nameInput.text = string.Empty;
            _emailInput.text = string.Empty;
            _phoneInput.text = string.Empty;
        }

        #endregion

        #region Thank You

        public void SetCountdownText(string text)
        {
            _countdownText.text = text;
        }

        #endregion

        #region Admin

        public void ShowError(string message)
        {
            _errorText.color = _errorColor;
            _errorText.text = message;
        }

        public void ShowSuccess(string message)
        {
            _errorText.color = _successColor;
            _errorText.text = message;
        }

        #endregion
    }
}