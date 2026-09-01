using System;
using Grid.Data;
using Grid.Data.Item;
using Grid.View;
using UnityEngine;

namespace Grid.Controller
{
    public class GridItemController : MonoBehaviour
    {
        [SerializeField] private GridItemView itemView;
        private GridItemData itemData;
        public GridItemData ItemData { get => itemData; }

        public void Init(GridItemControllerData data)
        {
            itemData = data.itemData;

            itemView.Init(new GridItemViewData(
                parent: data.parent,
                itemData: data.itemData
            ));
        }

        public void SetParent(RectTransform parent)
        {
            itemView.SetParent(parent);
        }

        public void SetSize(Vector2 size)
        {
            itemView.SetSize(size);
        }

        public void SetAbsolutePosition(Vector2 pos)
        {
            itemView.SetAbsolutePosition(pos);
        }

        public void Rescale(RescaleData data, Action<GridItemController> finishedRescaleCallback = null)
        {
            itemView.Rescale(data, () => finishedRescaleCallback?.Invoke(this));
        }

        public void CompleteTweems()
        {
            itemView.CompleteTweens();
        }
    }
}