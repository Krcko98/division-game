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
        [SerializeField] private Dictionary<Vector2, GridSlotController> slots = new Dictionary<Vector2, GridSlotController>();

        public Dictionary<Vector2, GridSlotController> Slots { get => slots; }

        public void Init(GridControllerData data)
        {
            List<GridSlotController> slotList = new List<GridSlotController>();
            slots.Clear();

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
                createdSlots: out slotList 
            );

            foreach (GridSlotController slot in slotList)
            {
                slots.Add(slot.Pos, slot);
            }
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