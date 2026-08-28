using Grid.Controller;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid.Static
{
    public class EventDelegates
    {
        #region GridSlot
        public delegate void OnSlotDragBeginDelegate(GridSlotController slot, PointerEventData eventData);
        public delegate void OnSlotDragDelegate(GridSlotController slot, PointerEventData eventData);
        public delegate void OnSlotDragEndDelegate(GridSlotController slot, PointerEventData eventData);
        #endregion
    }
}