using System;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Data
{
    public enum SegmentType
    {
        Win,
        Lose
    }

    [Serializable]
    public class SpinSegmentData
    {
        #region Fields

        public SegmentType SegmentType;
        public string DisplayText;
        public int RewardValue;

        #endregion
    }
}