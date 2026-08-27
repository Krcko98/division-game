using UnityEngine;

namespace Grid.Data
{
    public class GridSlotViewData
    {
        public RectTransform parent;

        public GridSlotViewData(RectTransform parent)
        {
            this.parent = parent;
        }
    }
}