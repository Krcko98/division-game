using Grid.Controller;
using Grid.Data;
using Grid.Data.Item;
using Grid.Factory;
using Grid.Layout;
using Grid.Static;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Grid.Data.GridSlotControllerData;

namespace Grid.View
{
    public class GridInventoryView : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup horizontalLayout;
        [SerializeField] private RectTransform rootParent;
        [SerializeField] private CanvasGroup inventoryContainer;

        private GridSlotController slotPref;
        private GridSlotKeepController slotKeepPref;

        public void Init(
            GridInventoryViewData data, 
            out List<GridSlotController> createdSlots
        )
        {
            slotPref = data.slotPref;
            slotKeepPref = data.slotKeepPref;

            createGrid(
                createdSlots: out createdSlots,
                onDragBeginCallback: data.onDragBegin, 
                onDragEndCallback: data.onDragEnd,
                functionalityData: data.functionalityData,
                onDragCallback: data.onDrag,
                size: data.inventorySize
            );
        }

        private void createGrid(
            out List<GridSlotController> createdSlots,
            EventDelegates.OnSlotDragBeginDelegate onDragBeginCallback,
            EventDelegates.OnSlotDragEndDelegate onDragEndCallback,
            EventDelegates.OnSlotDragDelegate onDragCallback,
            FunctionalityData functionalityData,
            int size
        )
        {
            List<GridSlotController> slots = new List<GridSlotController>();

            //Fill all standard inventory slots
            for (int i = 0; i < size; i++)
            {
                GridSlotController slot = GridFactory.CreateGridSlot(slotPref);

                slot.gameObject.SetActive(true);
                slot.Init(new GridSlotControllerData(
                    parent: rootParent,
                    pos: new Vector2(0, i),
                    onSlotDragEnd: onDragEndCallback,
                    onSlotDragBegin: onDragBeginCallback,
                    onSlotDrag: onDragCallback,
                    functionalityData: functionalityData
                ));

                slots.Add(slot);
            }

            //Fill keep inventory slot
            GridSlotKeepController keepSlot = GridFactory.CreateGridSlotKeep(slotKeepPref);

            keepSlot.gameObject.SetActive(true);
            keepSlot.Init(new GridSlotKeepControllerData(
                gridSlotControllerData: new GridSlotControllerData(
                    parent: rootParent,
                    pos: new Vector2(0, size),
                    onSlotDragEnd: onDragEndCallback,
                    onSlotDragBegin: onDragBeginCallback,
                    onSlotDrag: onDragCallback,
                    functionalityData: functionalityData
                )
            ));

            slots.Add(keepSlot);

            createdSlots = slots;
        }

        #region Functionality
        public void AttachItem(GridSlotController slot, GridItemController item)
        {
            slot.AttachItem(item);
        }

        public void DetachItem(GridSlotController slot, RescaleData data, Action<GridSlotController> detachCallback = null)
        {
            slot.DetachItem(
                data: data,
                callback: detachCallback
            );
        }

        public void SetInteractivity(bool interactive)
        {
            inventoryContainer.interactable = interactive;
            inventoryContainer.blocksRaycasts = interactive;
        }
        #endregion
    }
}