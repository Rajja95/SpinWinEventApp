namespace RUS95.SpinWinEventApp.Data
{
    public class SpinResult
    {
        #region Properties

        public SpinSegment Segment { get; private set; }
        public float TargetAngle { get; private set; }

        #endregion

        #region Constructor

        public SpinResult(SpinSegment segment)
        {
            Segment = segment;
            TargetAngle = segment.CenterAngle;
        }

        #endregion
    }
}