using System;
using Grid.Data;
using Grid.Data.Item;
using Grid.Factory;
using Grid.Static;
using Grid.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid.Controller
{
    public class GridSlotController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private GridSlotView slotView;

        [SerializeField] private Vector2 pos;
        [SerializeField] private GridItemController attachedItem;

        private bool canDrag;

        private event EventDelegates.OnSlotDragBeginDelegate onSlotDragBegin;
        private event EventDelegates.OnSlotDragEndDelegate onSlotDragEnd;
        private event EventDelegates.OnSlotDragDelegate onSlotDrag;

        public bool CanDrag { get => canDrag; protected set => canDrag = value; }
        public Vector2 Pos { get => pos; }
        public GridItemController AttachedItem { get => attachedItem; }

        public GridSlotView SlotView { get => slotView; }
    
        public virtual void Init(GridSlotControllerData data)
        {
            pos = data.pos;
            canDrag = 
                data.functionalityData.activeSlotID == pos.y ? 
                true : 
                data.functionalityData.canDragSlot;

            onSlotDragBegin = data.onSlotDragBegin;
            onSlotDragEnd = data.onSlotDragEnd;
            onSlotDrag = data.onSlotDrag;

            slotView.Init(new GridSlotViewData(
                parent: data.parent
            ));
        }

        public void AttachItem(GridItemController item)
        {
            attachedItem = item;
            item.SetParent(slotView.RootParent);
        }

        public void DetachItem(RescaleData data, Action<GridSlotController> callback = null)
        {
            if(attachedItem == null) 
            {
                callback?.Invoke(null);
                return;
            }

            attachedItem.Rescale(
                data,
                (GridItemController item) => 
                {
                    attachedItem = null;
                    GridFactory.DespawnGridItem(item);     

                    callback?.Invoke(this);
                }
            );
        }

        public void Select(bool select)
        {
            slotView.SelectOutline(select);
        }

        private void OnDisable()
        {

        }

        #region Drag
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!canDrag) return;

            onSlotDragBegin?.Invoke(this, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!canDrag) return;

            onSlotDragEnd?.Invoke(this, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!canDrag) return;

            onSlotDrag?.Invoke(this, eventData);
        }
        #endregion
    }
}