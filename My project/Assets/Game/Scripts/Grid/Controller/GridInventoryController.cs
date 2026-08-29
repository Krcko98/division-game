using Grid.Data;
using Grid.Static;
using Grid.View;
using System.Collections.Generic;
using UnityEngine;
using static Grid.Data.GridSlotControllerData;
using System.Linq;
using UnityEngine.EventSystems;

namespace Grid.Controller
{
    public class GridInventoryController : MonoBehaviour
    {
        [SerializeField] private GridInventoryView gridInventoryView;

        //Used as a template for creating slots in runtime
        [SerializeField] private GridSlotController slotPref;
        [SerializeField] private GridSlotKeepController slotKeepPref;

        [SerializeField] private Dictionary<Vector2,GridSlotController> slots = new Dictionary<Vector2, GridSlotController>();

        private int freeSlots = 0;

        public void Init(GridInventoryControllerData data)
        {
            List<GridSlotController> slotList = new List<GridSlotController>();
            slots.Clear();

            gridInventoryView.Init(
                new GridInventoryViewData(
                    slotPref: slotPref,
                    slotKeepPref: slotKeepPref,
                    onDragBegin: slotDragBegin,
                    onDragEnd: slotDragEnd,
                    onDrag: slotDrag,
                    functionalityData: new FunctionalityData(
                        canDragSlot: false,
                        activeSlotID: data.inventorySize - 1
                    ),
                    inventorySize: data.inventorySize
                ),
                createdSlots: out slotList 
            );

            freeSlots = data.inventorySize;

            foreach(GridSlotController slot in slotList)
            {
                slots.Add(slot.Pos, slot);
            }
        }

        #region Functionality
        public void PushSlotItemToQueue(GridItemController item)
        {
            int pos = freeSlots - 1;
            gridInventoryView.PushSlotItem(slots[new Vector2(0,freeSlots-1)], item);
            freeSlots--;
        }

        public void PopItemInSlotFromQueue()
        {
            //gridInventoryView.PopSlotItem(slots[new Vector2(0, freeSlots - 1)]);
            freeSlots++;
        }

        public void SetInteractivity(bool interactive)
        {
            gridInventoryView.SetInteractivity(interactive);
        }
        #endregion

        #region Calc

        #endregion

        #region SlotDrag
        private void slotDragBegin(GridSlotController slot, PointerEventData eventData)
        {
            GlobalEventBus.OnSlotInventoryDragBegin?.Invoke(slot, eventData);
        }

        private void slotDragEnd(GridSlotController slot, PointerEventData eventData)
        {
            GlobalEventBus.OnSlotInventoryDragEnd?.Invoke(slot, eventData);
        }

        private void slotDrag(GridSlotController slot, PointerEventData eventData)
        {
            GlobalEventBus.OnSlotInventoryDrag?.Invoke(slot, eventData);
        }
        #endregion
    }
}