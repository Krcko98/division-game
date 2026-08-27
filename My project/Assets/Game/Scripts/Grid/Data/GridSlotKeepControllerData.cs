using Grid.Static;
using UnityEngine;

namespace Grid.Data
{
    public class GridSlotKeepControllerData : GridSlotControllerData
    {
        public GridSlotKeepControllerData(
            GridSlotControllerData gridSlotControllerData
        ) : base(
            parent: gridSlotControllerData.parent,
            pos: gridSlotControllerData.pos,
            onSlotDragEnd: gridSlotControllerData.onSlotDragEnd,
            onSlotDragBegin: gridSlotControllerData.onSlotDragBegin,
            functionalityData: gridSlotControllerData.functionalityData
        )
        {
            
        }
    }
}