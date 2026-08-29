using Grid.Controller;
using Grid.Data;
using Grid.Data.Item;
using Grid.Factory;
using Grid.Static;
using Grid.Static.Helper;
using SM;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid.Gameplay.States
{
    public class GameplayOpenState : State
    {
        protected GameplayFSM fsm;

        private GridSlotController draggingSlot = null;
        private GridItemController spawnedItem = null;
        private string spawnedItemKey = "spawnedItem";
        private string draggingSlotKey = "draggingSlot";

        private RescaleData draggedSlotRescaleData = new RescaleData(
            rescaleFromSize: Vector3.one,
            rescaleToSize: Vector3.zero,
            startDelay: 0,
            duration: 0.3f
        );
        private RescaleData draggedItemRescaleData = new RescaleData(
            rescaleFromSize: Vector3.zero,
            rescaleToSize: Vector3.one,
            startDelay: 0,
            duration: 0.5f
        );

        private int spawnableItemID = 1;

        public override void Init(StateMachine sm)
        {
            base.Init(sm);

            fsm = sm as GameplayFSM;
        }

        public override void Enter()
        {
            base.Enter();
            
            GlobalEventBus.OnSlotInventoryDragBegin += OnDragBegin;
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
            GlobalEventBus.OnSlotInventoryDragBegin -= OnDragBegin;
        }

        #region Event
        private void OnDragBegin(GridSlotController slot, PointerEventData eventData)
        {
            if(draggingSlot != null) return;

            draggingSlot = slot;

            if(!fsm.data.TryAdd(draggingSlotKey, draggingSlot))
            {
                fsm.data[draggingSlotKey] = draggingSlot;
            }

            //Start all needed effects on the slot that we started dragging on
            slot.AttachedItem.Rescale(
                draggedSlotRescaleData
            );

            //Create and setup new draggable item that has free movement.
            //We will use this item to manipulate across states easily with fsm.data
            GridItemController item = GridFactory.CreateGridItem(spawnableItemID);
            item.Init(new GridItemControllerData(
                parent: null,
                new GridItemData(
                    number: draggingSlot.AttachedItem.ItemData.number,
                    color: draggingSlot.AttachedItem.ItemData.color,
                    itemData: GameController.Instance.GameplayData.DragItemData
                )
            ));

            item.SetParent(GameController.Instance.MainCanvas.GetComponent<RectTransform>());
            item.SetSize(GameController.Instance.GameplayData.DraggedItemSize);
            item.SetAbsolutePosition(CanvasHelper.ClickPointToCanvasPoint(GameController.Instance.MainCanvas, eventData));

            //Start all needed effects on the spawned item, after we set parent and all init data
            item.Rescale(
                draggedItemRescaleData
            );

            spawnedItem = item;

            if(!fsm.data.TryAdd(spawnedItemKey, spawnedItem))
            {
                fsm.data[spawnedItemKey] = spawnedItem;
            }

            fsm.ChangeState(GameplayFSM.dragState);
        }
        #endregion
    }
}