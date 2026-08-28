using UnityEngine;

namespace Grid.Static
{
    public static class GlobalEventBus
    {
        public static EventDelegates.OnSlotDragBeginDelegate OnSlotInventoryDragBegin;
        public static EventDelegates.OnSlotDragDelegate OnSlotInventoryDrag;
        public static EventDelegates.OnSlotDragEndDelegate OnSlotInventoryDragEnd;

        public static void RemoveSubscriptions()
        {
            OnSlotInventoryDragBegin = null;
            OnSlotInventoryDrag = null;
            OnSlotInventoryDragEnd = null;
        }
    }
}