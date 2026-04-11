using RUS95.SpinWinEventApp.Core;
using RUS95.SpinWinEventApp.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems.Spin
{
    public class SpinController : MonoBehaviour , ISpinInputReceiver
    {
        #region Fields

        [Header("References")]
        [SerializeField] private Transform _wheelTransform;

        [Header("Spin Settings")]
        [SerializeField] private float _spinDuration = 3f;
        [SerializeField] private int _extraSpins = 5;
        [SerializeField] private AnimationCurve _spinCurve;

        private ISpinStrategy _spinStrategy;
        private List<SpinSegment> _segments;

        private bool _isSpinning;
        private float _startAngle;
        private float _targetAngle;
        private float _elapsedTime;

        private SpinResult _currentResult;

        [SerializeField] private List<SpinSegmentData> _segmentConfigs;
        [SerializeField] private float _angleOffset = 90f;

        private bool _canSpin = true;

        #endregion

        #region Events

        public event Action OnSpinStarted;
        public event Action<SpinResult> OnSpinCompleted;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            if (!_isSpinning)
                return;

            UpdateSpin();
        }

        #endregion

        #region Initialization

        private void Initialize()
        {
            CreateSegments();
            _spinStrategy = new SegmentBasedSpinStrategy(_segments);
        }

        private void CreateSegments()
        {
            _segments = new List<SpinSegment>();

            int count = _segmentConfigs.Count;
            float anglePerSegment = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float centerAngle = i * anglePerSegment + (anglePerSegment / 2f);

                var config = _segmentConfigs[i];

                _segments.Add(new SpinSegment(
                    i,
                    config.SegmentType,
                    centerAngle,
                    config.DisplayText
                ));
            }
        }

        #endregion

        #region Public Methods

        public void StartSpin()
        {
            if (_isSpinning)
                return;

            _currentResult = _spinStrategy.GenerateResult();

            _startAngle = GetNormalizedAngle(_wheelTransform.localEulerAngles.z);

            float finalAngle = CalculateFinalAngle(_currentResult.TargetAngle);

            _targetAngle = _startAngle + (_extraSpins * 360f) + finalAngle;

            _elapsedTime = 0f;
            _isSpinning = true;

            OnSpinStarted?.Invoke();
        }

        public void OnSpinRequested()
        {
            if (!_canSpin) return;

            StartSpin(); // your existing logic
        }

        public void OnSpinDrag(float delta)
        {
            if (!_canSpin) return;

            // Optional: allow manual rotation before spin
            transform.Rotate(0f, 0f, -delta);
        }

        #endregion

        #region Private Methods

        private void UpdateSpin()
        {
            _elapsedTime += Time.deltaTime;

            float t = _elapsedTime / _spinDuration;
            t = Mathf.Clamp01(t);

            float curveT = Mathf.Clamp01(_spinCurve.Evaluate(t)); // IMPORTANT FIX

            float angle = Mathf.Lerp(_startAngle, _targetAngle, curveT);

            SetWheelRotation(angle);

            if (t >= 1f)
            {
                CompleteSpin();
            }
        }

        private void CompleteSpin()
        {
            _isSpinning = false;

            //SetWheelRotation(_targetAngle);

            OnSpinCompleted?.Invoke(_currentResult);
        }

        private void SetWheelRotation(float angle)
        {
            _wheelTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        private float CalculateFinalAngle(float segmentAngle)
        {
            // Invert for clockwise spin
            return 360f - segmentAngle + _angleOffset;
        }

        private float GetNormalizedAngle(float angle)
        {
            return angle % 360f;
        }

        #endregion
    }
}