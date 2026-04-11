namespace RUS95.SpinWinEventApp.Data
{
    public class SpinSegment
    {
        #region Properties

        public int Index { get; private set; }
        public SegmentType SegmentType { get; private set; }
        public float CenterAngle { get; private set; }
        public string DisplayText { get; private set; }

        #endregion

        #region Constructor

        public SpinSegment(int index, SegmentType type, float centerAngle, string displayText)
        {
            Index = index;
            SegmentType = type;
            CenterAngle = centerAngle;
            DisplayText = displayText;
        }

        #endregion

        #region Public Methods

        public bool IsWin()
        {
            return SegmentType == SegmentType.Win;
        }

        #endregion
    }
}