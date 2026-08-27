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
        public FunctionalityData functionalityData;

        public class FunctionalityData
        {
            public bool canDragSlot = true;

            public FunctionalityData(bool canDragSlot)
            {
                this.canDragSlot = canDragSlot;
            }
        }

        public GridSlotControllerData(
            RectTransform parent, 
            Vector2 pos, 
            EventDelegates.OnSlotDragEndDelegate onSlotDragEnd, 
            EventDelegates.OnSlotDragBeginDelegate onSlotDragBegin,
            FunctionalityData functionalityData
        )
        {
            this.parent = parent;
            this.pos = pos;
            this.onSlotDragEnd = onSlotDragEnd;
            this.onSlotDragBegin = onSlotDragBegin;
            this.functionalityData = functionalityData;
        }
    }
}