using Grid.Data.Item;
using Grid.Static;
using UnityEngine;

namespace Grid.Data
{
    public class GridItemControllerData
    {
        public RectTransform parent;
        public GridItemData itemData;

        public GridItemControllerData(
            RectTransform parent,
            GridItemData itemData
        )
        {
            this.parent = parent;
            this.itemData = itemData;
        }
    }
}