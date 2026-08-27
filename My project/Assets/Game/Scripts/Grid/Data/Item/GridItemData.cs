using UnityEngine;

namespace Grid.Data.Item
{
    public class GridItemData
    {
        public int number;
        public Color color;

        public GridItemData(int number, Color color)
        {
            this.number = number;
            this.color = color;
        }
    }
}