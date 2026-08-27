using Grid.Controller;
using Grid.Data;
using Grid.Factory;
using Grid.Layout;
using Grid.Static;
using System.Collections.Generic;
using UnityEngine;
using static Grid.Data.GridSlotControllerData;

namespace Grid.View
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private FlexibleGridLayout gridLayout;
        [SerializeField] private RectTransform rootParent;

        private GridSlotController slotPref;

        public void Init(
            GridViewData data, 
            out List<GridSlotController> createdSlots
        )
        {
            slotPref = data.slotPref;

            createGrid(
                out createdSlots, 
                data.onDragBegin, 
                data.onDragEnd,
                data.functionalityData
            );
        }

        private void createGrid(
            out List<GridSlotController> createdSlots, 
            EventDelegates.OnSlotDragBeginDelegate onDragBeginCallback, 
            EventDelegates.OnSlotDragEndDelegate onDragEndCallback,
            FunctionalityData functionalityData
        )
        {
            List<GridSlotController> slots = new List<GridSlotController>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GridSlotController slot = GridFactory.CreateGridSlot(slotPref);

                    slot.gameObject.SetActive(true);
                    slot.Init(new GridSlotControllerData(
                        parent: rootParent,
                        pos: new Vector2(i,j),
                        onSlotDragEnd: onDragEndCallback,
                        onSlotDragBegin: onDragBeginCallback,
                        functionalityData: functionalityData
                    ));

                    slots.Add(slot);
                }
            }

            createdSlots = slots;
        }
    }
}