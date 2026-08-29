using UnityEngine;

namespace Grid.Data.Item
{
    public class GridItemData
    {
        public int number;
        public Color color;
        public GridItemDataSO itemData;

        public GridItemData(int number, Color color, GridItemDataSO itemData)
        {
            this.number = number;
            this.color = color;
            this.itemData = itemData;
        }
    }
}