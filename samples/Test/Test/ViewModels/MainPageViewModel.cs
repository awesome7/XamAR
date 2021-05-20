using System.Collections.Generic;
using System.Linq;
using XamAR.Core;
using XamAR.Diagnostics;

namespace Test.ViewModels
{
    public class MainPageViewModel : NotifyBase
    {
        private bool _drawPositions = true;
        private bool _drawDirections = true;
        private int _skipFrames;
        private int _frameRate;
        private int _calculationRate;
        //private readonly EntityUpdateServiceSkipFrames _service;

        public static List<int> SkipFrameItems = Enumerable.Range(0, 10).ToList();

        public bool DrawPositions
        {
            get => _drawPositions;
            set => OnPropertyChanged(() => _drawPositions = value);
        }

        public bool DrawDirections
        {
            get => _drawDirections;
            set => OnPropertyChanged(() => _drawDirections = value);
        }

        /// <summary>
        /// How many frames to skip.
        /// </summary>
        public int SkipFrames
        {
            get => _skipFrames;
            set => OnPropertyChanged(() =>
            {
                _skipFrames = value;
                //_service.SkipFrames = value;
            });
        }

        public int FrameRate
        {
            get => _frameRate;
            set => OnPropertyChanged(() => _frameRate = value);
        }

        public int CalculationRate
        {
            get => _calculationRate;
            set => OnPropertyChanged(() => _calculationRate = value);
        }

        public MainPageViewModel()
        {
            //_service = DI.Container.Resolve<EntityUpdateServiceSkipFrames>();
            //_service.RateChanged += () =>
            //{
            //    CalculationRate = _service.CalculationRate;
            //    FrameRate = _service.FrameRate;
            //};
        }
    }
}
