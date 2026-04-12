using System.Collections.Generic;
using UnityEngine;
using RUS95.SpinWinEventApp.Data;

namespace RUS95.SpinWinEventApp.Systems.Spin
{
    public class SegmentBasedSpinStrategy : ISpinStrategy
    {
        #region Fields

        private readonly List<SpinSegment> _allSegments;
        private readonly List<SpinSegment> _winSegments;
        private readonly List<SpinSegment> _loseSegments;

        #endregion

        #region Constructor

        public SegmentBasedSpinStrategy(List<SpinSegment> segments)
        {
            _allSegments = segments;

            _winSegments = new List<SpinSegment>();
            _loseSegments = new List<SpinSegment>();

            CacheSegments();
        }

        #endregion

        #region Public Methods

        public SpinResult GenerateResult()
        {
            // 50/50 chance (can be replaced later)
            bool isWin = Random.value >= 0.5f;

            SpinSegment selectedSegment = GetRandomSegment(isWin);

            return new SpinResult(selectedSegment);
        }

        #endregion

        #region Private Methods

        private void CacheSegments()
        {
            foreach (var segment in _allSegments)
            {
                if (segment.IsWin())
                {
                    _winSegments.Add(segment);
                }
                else
                {
                    _loseSegments.Add(segment);
                }
            }
        }

        private SpinSegment GetRandomSegment(bool isWin)
        {
            List<SpinSegment> targetList = isWin ? _winSegments : _loseSegments;

            // Safety check (VERY IMPORTANT)
            if (targetList == null || targetList.Count == 0)
            {
                Debug.LogWarning("No segments found for selected type. Falling back to all segments.");

                return _allSegments[Random.Range(0, _allSegments.Count)];
            }

            int index = Random.Range(0, targetList.Count);
            return targetList[index];
        }

        #endregion
    }
}