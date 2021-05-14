using System;
using System.Diagnostics;
using XamAR.Core;
using XamAR.Core.Display;

namespace XamAR.Diagnostics
{
    public class EntityUpdateServiceSkipFrames : EntityUpdateService
    {
        private short _calculationRates;

        private short _framerateFrames;
        private int _skippedFrames;
        private readonly Stopwatch _sw = new Stopwatch();

        public EntityUpdateServiceSkipFrames(ObjectManagerService objectManager, IDisplayService displayService)
            : base(objectManager, displayService)
        {
            _sw.Start();
        }

        public int FrameRate { get; private set; }

        public int CalculationRate { get; private set; }

        public int SkipFrames { get; set; }

        /// <summary>
        ///     Frame or calculation rate changed.
        /// </summary>
        public event Action RateChanged;

        protected override void Update()
        {
            // Framerate calculations.
            _framerateFrames++;
            if (_sw.ElapsedMilliseconds > 1000)
            {
                FrameRate = _framerateFrames;
                CalculationRate = _calculationRates;
                _framerateFrames = 0;
                _calculationRates = 0;
                if (FrameRate != _framerateFrames)
                {
                    RateChanged?.Invoke();
                }

                _sw.Restart();
            }

            // Skipping desired number of frames.
            _skippedFrames++;
            if (_skippedFrames > SkipFrames)
            {
                _skippedFrames = 0;
                base.Update();
                _calculationRates++;
            }
        }
    }
}
