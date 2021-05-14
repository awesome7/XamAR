using XamAR.Core.Models;

namespace XamAR.Core.Events
{
    /// <summary>
    /// Contains Id of object that was pressed.
    /// </summary>
    public class PressedEventsArgs 
    {
        public AnchoredObject Object { get; }

        public PressedEventsArgs(AnchoredObject obj)
        {
            Object = obj;
        }
    }
}
