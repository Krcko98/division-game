using Grid.Static;
using UnityEngine;

namespace Grid.Data
{
    public class GridItemControllerData
    {
        public RectTransform parent;

        public GridItemControllerData(
            RectTransform parent
        )
        {
            this.parent = parent;
        }
    }
}