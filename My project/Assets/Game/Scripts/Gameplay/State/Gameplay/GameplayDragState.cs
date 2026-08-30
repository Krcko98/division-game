using Grid.Controller;
using Grid.Controller.Slot;
using Grid.Data;
using Grid.Data.Item;
using Grid.Factory;
using Grid.Gameplay.Rules.Data;
using Grid.Static;
using Grid.Static.Helper;
using SM;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid.Gameplay.States
{
    public class GameplayDragState : State
    {
        protected GameplayFSM fsm;

        private GridSlotController draggingSlot;
        private GridItemController spawnedItem;

        private GridSlotController slotHover;
        private GridSlotController previousSlotHover;

        private string spawnedItemKey = "spawnedItem";
        private string draggingSlotKey = "draggingSlot";

        private RescaleData draggedSlotRescaleData = new RescaleData(
            rescaleFromSize: Vector3.zero,
            rescaleToSize: Vector3.one,
            startDelay: 0,
            duration: 0.3f
        );
        private RescaleData draggedItemRescaleData = new RescaleData(
            rescaleFromSize: Vector3.one,
            rescaleToSize: Vector3.zero,
            startDelay: 0,
            duration: 0.2f
        );

        private RescaleData slotAttachRescaleData = new RescaleData(
            rescaleFromSize: Vector3.zero,
            rescaleToSize: Vector3.one,
            startDelay: 0,
            duration: 0.3f
        );

        private RescaleData slotDetachRescaleData = new RescaleData(
            rescaleFromSize: Vector3.one,
            rescaleToSize: Vector3.zero,
            startDelay: 0,
            duration: 0.3f
        );

        public override void Init(StateMachine sm)
        {
            base.Init(sm);

            fsm = sm as GameplayFSM;
        }

        public override void Enter()
        {
            base.Enter();
            
            GlobalEventBus.OnSlotInventoryDragEnd += OnDragEnd;
            GlobalEventBus.OnSlotInventoryDrag += OnDrag;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();

            draggingSlot = null;
            spawnedItem = null;

            removeListeners();
        }

        private void removeListeners()
        {
            GlobalEventBus.OnSlotInventoryDragEnd -= OnDragEnd;
            GlobalEventBus.OnSlotInventoryDrag -= OnDrag;
        }

        #region Event
        private void OnDrag(GridSlotController slot, PointerEventData eventData)
        {
            draggingSlot = fsm.data[draggingSlotKey] as GridSlotController;
            spawnedItem = fsm.data[spawnedItemKey] as GridItemController;

            if(draggingSlot != slot) return;

            slotHoverSelection(eventData.pointerCurrentRaycast);

            spawnedItem.SetAbsolutePosition(CanvasHelper.ClickPointToCanvasPoint(GameController.Instance.MainCanvas, eventData));
        }

        private void OnDragEnd(GridSlotController slot, PointerEventData eventData)
        {
            spawnedItem = fsm.data[spawnedItemKey] as GridItemController;

            slotHover = getGridSlotFromRaycast(eventData.pointerCurrentRaycast);

            deselectSlots();

            //Our pointer is over slot that is selected
            if(slotHover != null)
            {
                //If item is not already on the selected slot we will create a new item
                //Otherwise, we just continue
                if(slotHover.AttachedItem == null)
                {
                    int num = spawnedItem.ItemData.number;

                    GridItemController item = GridFactory.CreateGridItem(0);
                    item.Init(new GridItemControllerData(
                        parent: null,
                        itemData: new GridItemData(
                            number: num,
                            color: GridItemHelper.FetchColor(num),
                            itemData: GameController.Instance.GameplayData.DragAttachItemData
                        )
                    ));

                    slotHover.AttachItem(item);
                    slotHover.AttachedItem.Rescale(
                        slotAttachRescaleData
                    );

                    GameController.Instance.GameRules.InsertItem(new PosValue(
                        pos: new Vector2Int((int)slotHover.Pos.x, (int)slotHover.Pos.y),
                        value: num
                    ));

                    fsm.inventoryController.PopItemInSlotFromQueue(slotDetachRescaleData);
                }
            }

            draggingSlot.AttachedItem.Rescale(
                draggedSlotRescaleData
            );

            spawnedItem.Rescale(
                draggedItemRescaleData,
                (GridItemController item) => 
                {
                    GridFactory.DespawnGridItem(item);

                    spawnedItem = null;
                    draggingSlot = null;

                    fsm.ChangeState(GameplayFSM.openState);
                }
            );
        }
        #endregion

        private GridSlotController getGridSlotFromRaycast(RaycastResult result)
        {
            return result.gameObject?.GetComponent<GridSlotReceiver>()?.GridSlot;
        }

        private void slotHoverSelection(RaycastResult result)
        {
            slotHover = getGridSlotFromRaycast(result);

            if(slotHover != null)
            {
                slotHover.Select(true);

                if(previousSlotHover != slotHover && previousSlotHover != null) previousSlotHover.Select(false);
            }
            else
            {
                if(previousSlotHover != slotHover && previousSlotHover != null) previousSlotHover.Select(false);
            }
            previousSlotHover = slotHover;
        }

        private void deselectSlots()
        {
            slotHover?.Select(false);
            previousSlotHover?.Select(false);
        }
    }
}