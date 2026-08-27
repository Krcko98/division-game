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
        public FunctionalityData functionalityData;

        public GridViewData(
            GridSlotController slotPref, 
            EventDelegates.OnSlotDragBeginDelegate onDragBegin, 
            EventDelegates.OnSlotDragEndDelegate onDragEnd,
            FunctionalityData functionalityData
        )
        {
            this.slotPref = slotPref;
            this.onDragBegin = onDragBegin;
            this.onDragEnd = onDragEnd;
            this.functionalityData = functionalityData;
        }
    }
}