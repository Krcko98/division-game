using Grid.Controller;
using Grid.Static;
using UnityEngine;
using static Grid.Data.GridSlotControllerData;
using static Grid.Static.EventDelegates;

namespace Grid.Data
{
    public class GridViewData
    {
        public GridSlotController slotPref;
        public EventDelegates.OnSlotDragBeginDelegate onDragBegin;
        public EventDelegates.OnSlotDragEndDelegate onDragEnd;
        public EventDelegates.OnSlotDragDelegate onDrag;
        public FunctionalityData functionalityData;

        public GridViewData(
            GridSlotController slotPref, 
            EventDelegates.OnSlotDragBeginDelegate onDragBegin, 
            EventDelegates.OnSlotDragEndDelegate onDragEnd,
            EventDelegates.OnSlotDragDelegate onDrag,
            FunctionalityData functionalityData
        )
        {
            this.slotPref = slotPref;
            this.onDragBegin = onDragBegin;
            this.onDragEnd = onDragEnd;
            this.onDrag = onDrag;
            this.functionalityData = functionalityData;
        }
    }
}