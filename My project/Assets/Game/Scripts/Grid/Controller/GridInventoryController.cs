using Grid.Data;
using Grid.Static;
using Grid.View;
using System.Collections.Generic;
using UnityEngine;
using static Grid.Data.GridSlotControllerData;
using System.Linq;
using UnityEngine.EventSystems;
using Grid.Data.Item;
using Grid.Factory;
using Grid.Static.Helper;
using Grid.Gameplay;
using System;

namespace Grid.Controller
{
    public class GridInventoryController : MonoBehaviour
    {
        [SerializeField] private GridInventoryView gridInventoryView;

        //Used as a template for creating slots in runtime
        [SerializeField] private GridSlotController slotPref;
        [SerializeField] private GridSlotKeepController slotKeepPref;

        [SerializeField] private Dictionary<Vector2,GridSlotController> slots = new Dictionary<Vector2, GridSlotController>();

        private RescaleData inventoryPushRescaleData;

        private int freeSlots = 0;
        private int inventorySlots = 0;

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

            inventorySlots = data.inventorySize;
            freeSlots = inventorySlots;

            foreach(GridSlotController slot in slotList)
            {
                slots.Add(slot.Pos, slot);
            }
        }

        #region Functionality
        public void PushSlotItemToQueue(GridItemController item, Action pushedCallback = null)
        {
            if(freeSlots <= 0) return;

            GridSlotController activeSlot;
            int max = inventorySlots-1;

            for(int i=max; i>=0; i--)
            {
                activeSlot = slots[new Vector2(0, i)];

                if(activeSlot.AttachedItem == null) 
                {
                    if(i == 0)
                    {
                        gridInventoryView.AttachItem(activeSlot, item);
                        pushedCallback();
                    }
                    else continue;
                }
                else
                {
                    if(i == max) return;
                    else
                    {
                        moveSlotItems(
                            currentSlot: activeSlot, 
                            nextSlot: slots[new Vector2(0,  activeSlot.Pos.y + 1)],
                            currentSlotItem: item,
                            movedSlots: pushedCallback
                        );
                    }
                }
            }

            freeSlots--;
        }

        private void moveSlotItems(GridSlotController currentSlot, GridSlotController nextSlot, GridItemController currentSlotItem, Action movedSlots = null)
        {
            GridItemData itemData = currentSlot.AttachedItem.ItemData;

            //Take the data from currentSlotItem and create a new item and put it in the next slot
            GridItemController nextSlotNewItem = GridFactory.CreateGridItem(0);
            nextSlotNewItem.Init(new GridItemControllerData(
                parent: null,
                itemData: new GridItemData(
                    number: itemData.number,
                    color: GridItemHelper.FetchColor(itemData.number),
                    itemData: GameController.Instance.GameplayData.InventoryItemData
                )
            ));

            gridInventoryView.AttachItem(nextSlot, nextSlotNewItem);

            //Remove the current slots item
            gridInventoryView.DetachItem(
                slot: currentSlot,
                data: new RescaleData(
                    rescaleFromSize: Vector3.one,
                    rescaleToSize: Vector3.zero,
                    startDelay: 0,
                    duration: 0.3f
                ),
                (GridSlotController slot) =>
                {
                    gridInventoryView.AttachItem(currentSlot, currentSlotItem);
                    movedSlots?.Invoke();
                }
            );

        }

        public void PopItemInSlotFromQueue(RescaleData data)
        {
            gridInventoryView.DetachItem(slots[new Vector2(0, freeSlots - 1)], data);
            freeSlots--;
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