using System;

namespace RUS95.SpinWinEventApp.Data
{
    [Serializable]
    public class GameSessionData
    {
        #region Properties

        public string Timestamp; // NEW (important)

        public bool IsWin; // make public field (JSON safe)

        public string ResultLabel; // store simple result instead of SpinResult

        public string Name;
        public string Email;
        public string Phone;

        #endregion

        #region Public Methods

        public void SetResult(SpinResult result)
        {
            // Keep original logic
            IsWin = result.Segment.IsWin();

            // Store safe string instead of complex object
            ResultLabel = result.Segment.DisplayText;

            // Set timestamp ONCE
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public void Reset()
        {
            Timestamp = string.Empty;
            IsWin = false;
            ResultLabel = string.Empty;

            Name = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }

        #endregion
    }
}