using Grid.Controller;
using UnityEngine;

namespace Grid.Static
{
    public class EventDelegates
    {
        #region GridSlot
        public delegate void OnSlotDragBeginDelegate(GridSlotController slot);
        public delegate void OnSlotDragDelegate(GridSlotController slot);
        public delegate void OnSlotDragEndDelegate(GridSlotController slot);
        #endregion
    }
}