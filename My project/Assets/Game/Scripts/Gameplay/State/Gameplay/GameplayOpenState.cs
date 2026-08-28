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

            removeListeners();
        }

        private void removeListeners()
        {
            GlobalEventBus.OnSlotInventoryDragBegin -= OnDragBegin;
            GlobalEventBus.OnSlotInventoryDragEnd -= OnDragEnd;
            GlobalEventBus.OnSlotInventoryDrag -= OnDrag;
        }

        #region Event
        private void OnDragBegin(GridSlotController slot, PointerEventData eventData)
        {
            draggingSlot = slot;
            Debug.Log(slot);

            GridItemController item = GridFactory.CreateGridItem(spawnableItemID);
            item.Init(new GridItemControllerData(
                parent: null,
                new GridItemData(
                    draggingSlot.AttachedItem.ItemData.number,
                    draggingSlot.AttachedItem.ItemData.color
                )
            ));

            item.SetParent(GameController.Instance.MainCanvas.GetComponent<RectTransform>());
            item.SetSize(GameController.Instance.GameplayData.DraggedItemSize);
            item.SetAbsolutePosition(CanvasHelper.ClickPointToCanvasPoint(GameController.Instance.MainCanvas, eventData));

            spawnedItem = item;
        }

        private void OnDrag(GridSlotController slot, PointerEventData eventData)
        {
            if(draggingSlot != slot) return;

            spawnedItem.SetAbsolutePosition(CanvasHelper.ClickPointToCanvasPoint(GameController.Instance.MainCanvas, eventData));
        }

        private void OnDragEnd(GridSlotController slot, PointerEventData eventData)
        {
            GridFactory.DespawnGridItem(spawnedItem);

            spawnedItem = null;
            draggingSlot = null;
        }
        #endregion
    }
}