using Grid.Data;
using Grid.Static;
using Grid.View;
using UnityEngine;
using UnityEngine.EventSystems;
using static Grid.Data.GridSlotControllerData;

namespace Grid.Controller
{
    public class GridSlotController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private GridSlotView slotView;

        [SerializeField] private Vector2 pos;
        [SerializeField] private GridItemController attachedItem;

        [SerializeField] private FunctionalityData functionalityData;

        private event EventDelegates.OnSlotDragBeginDelegate onSlotDragBegin;
        private event EventDelegates.OnSlotDragEndDelegate onSlotDragEnd;

        public Vector2 Pos { get => pos; }
        public GridItemController AttachedItem { get => attachedItem; }

        public GridSlotView SlotView { get => slotView; }
    
        public virtual void Init(GridSlotControllerData data)
        {
            pos = data.pos;
            functionalityData = data.functionalityData;

            onSlotDragBegin = data.onSlotDragBegin;
            onSlotDragEnd = data.onSlotDragEnd;

            slotView.Init(new GridSlotViewData(
                parent: data.parent
            ));
        }

        public void AttachItem(GridItemController item)
        {
            attachedItem = item;
            item.Init(new GridItemControllerData(
                parent: slotView.RootParent
            ));
        }

        private void OnDisable()
        {

        }

        #region Drag
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!functionalityData.canDragSlot) return;

            onSlotDragBegin?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!functionalityData.canDragSlot) return;

            onSlotDragEnd?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!functionalityData.canDragSlot) return;


        }
        #endregion
    }
}