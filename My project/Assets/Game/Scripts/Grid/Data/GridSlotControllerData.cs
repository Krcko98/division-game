using Grid.Static;
using UnityEngine;

namespace Grid.Data
{
    public class GridSlotControllerData
    {
        public RectTransform parent;
        public Vector2 pos;
        public EventDelegates.OnSlotDragBeginDelegate onSlotDragBegin;
        public EventDelegates.OnSlotDragEndDelegate onSlotDragEnd;
        public EventDelegates.OnSlotDragDelegate onSlotDrag;
        public FunctionalityData functionalityData;

        public class FunctionalityData
        {
            public bool canDragSlot;
            public int activeSlotID;

            public FunctionalityData(bool canDragSlot, int activeSlotID)
            {
                this.canDragSlot = canDragSlot;
                this.activeSlotID = activeSlotID;
            }
        }

        public GridSlotControllerData(
            RectTransform parent, 
            Vector2 pos, 
            EventDelegates.OnSlotDragEndDelegate onSlotDragEnd, 
            EventDelegates.OnSlotDragBeginDelegate onSlotDragBegin,
            EventDelegates.OnSlotDragDelegate onSlotDrag,
            FunctionalityData functionalityData
        )
        {
            this.parent = parent;
            this.pos = pos;
            this.onSlotDragEnd = onSlotDragEnd;
            this.onSlotDragBegin = onSlotDragBegin;
            this.onSlotDrag = onSlotDrag;
            this.functionalityData = functionalityData;
        }
    }
}