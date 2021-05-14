using System;
using System.Numerics;

namespace XamAR.Core.Models.Position
{
    public interface IPositionSource
    {
        /// <summary>
        /// Position in real-world system. Center is at device's center
        /// <para>Refreshed on RefreshPositionRealWorld.</para>
        /// </summary>
        /// <remarks>
        /// (0,1,0) is North
        /// (1,0,0) is East
        /// </remarks>
        Vector3 RealWorldPosition { get; }

        /// <summary>
        /// Refreshes RealWorldPosition.
        /// </summary>
        void RefreshPositionRealWorld(WorldConverter converter);


        /// <summary>
        /// Returns position in AR world.
        /// </summary>
        Vector3 GetPositionInARWorld(WorldConverter converter);

        //NOTE For now it seems returning real world is enough.
        //NOTE If better performance is needed, provide that
        //NOTE different worlds are used, depending on Positionsource,
        //NOTE so returned vector is directly in required world 
        //NOTE (without extra calculations).
        //NOTE * For now, only RelativeToNorth needs to be calculated
        //NOTE   from device's world to real, and then reversed.

        /// <summary>
        /// Returns position in real-world, converted to 
        /// device's world.
        /// </summary>
        /// <returns></returns>
        //Vector3 GetPositionInDeviceSystem();

    }

    public abstract class PositionSourceBase : IPositionSource
    {
        public Vector3 RealWorldPosition 
        {
            get
            {
                if (_recalculateFlag)
                {
                    _recalculateFlag = false;
                    _realWorldPosition = GetRealWorld(_converter);
                }

                return _realWorldPosition;
            }
        }

        private WorldConverter _converter = null;

        private bool _recalculateFlag = true;
        private Vector3 _realWorldPosition = new Vector3();
        public Vector3 GetPositionInARWorld(WorldConverter converter)
        {
            throw new NotImplementedException();
        }

        public void RefreshPositionRealWorld(WorldConverter converter)
        {
            this._converter = converter;
            _recalculateFlag = true;
        }

        protected abstract Vector3 GetRealWorld(WorldConverter converter);
    }
}
