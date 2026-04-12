namespace RUS95.SpinWinEventApp.Data
{
    public class GameSessionData
    {
        #region Properties

        public bool IsWin { get; private set; }
        public SpinResult SpinResult { get; private set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        #endregion

        #region Public Methods

        public void SetResult(SpinResult result)
        {
            SpinResult = result;
            IsWin = result.Segment.IsWin();
        }

        public void Reset()
        {
            SpinResult = null;
            IsWin = false;
            Name = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }

        #endregion
    }
}