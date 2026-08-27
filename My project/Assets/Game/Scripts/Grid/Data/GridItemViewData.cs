using Grid.Data.Item;
using UnityEngine;

namespace Grid.Data
{
    public class GridItemViewData
    {
        public RectTransform parent;
        public GridItemData itemData;

        public GridItemViewData(RectTransform parent, GridItemData itemData)
        {
            this.parent = parent;
            this.itemData = itemData;
        }
    }
}