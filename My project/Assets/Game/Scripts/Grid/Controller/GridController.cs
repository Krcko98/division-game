using Grid.Data;
using Grid.Static;
using Grid.View;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Grid.Data.GridSlotControllerData;

namespace Grid.Controller
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GridView gridView;

        //Used as a template for creating slots in runtime
        [SerializeField] private GridSlotController slotPref;
        [SerializeField] private List<GridSlotController> slots = new List<GridSlotController>();

        public void Init(GridControllerData data)
        {
            gridView.Init(
                new GridViewData(
                    slotPref: slotPref,
                    onDragBegin: slotDragBegin,
                    onDragEnd: slotDragEnd,
                    onDrag: slotDrag,
                    functionalityData: new FunctionalityData(
                        canDragSlot: false,
                        activeSlotID: -1
                    )
                ),
                createdSlots: out slots 
            );
        }

        private void slotDragBegin(GridSlotController slot, PointerEventData eventData)
        {
            
        }

        private void slotDragEnd(GridSlotController slot, PointerEventData eventData)
        {

        }

        private void slotDrag(GridSlotController slot, PointerEventData eventData)
        {

        }
    }
}