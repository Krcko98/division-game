using Grid.Controller;
using Grid.Static;
using UnityEngine;
using static Grid.Data.GridSlotControllerData;
using static Grid.Static.EventDelegates;

namespace Grid.Data
{
    public class GridInventoryViewData
    {
        public GridSlotController slotPref;
        public GridSlotKeepController slotKeepPref;
        public EventDelegates.OnSlotDragBeginDelegate onDragBegin;
        public EventDelegates.OnSlotDragEndDelegate onDragEnd;
        public EventDelegates.OnSlotDragDelegate onDrag;
        public FunctionalityData functionalityData;

        public int inventorySize;

        public GridInventoryViewData(
            GridSlotController slotPref,
            GridSlotKeepController slotKeepPref,
            EventDelegates.OnSlotDragBeginDelegate onDragBegin, 
            EventDelegates.OnSlotDragEndDelegate onDragEnd,
            EventDelegates.OnSlotDragDelegate onDrag,
            FunctionalityData functionalityData,
            int inventorySize
        )
        {
            this.slotPref = slotPref;
            this.slotKeepPref = slotKeepPref;
            this.onDragBegin = onDragBegin;
            this.onDragEnd = onDragEnd;
            this.onDrag = onDrag;
            this.functionalityData = functionalityData;
            this.inventorySize = inventorySize;
        }
    }
}