using UnityEngine;

namespace Grid.Gameplay.Rules.Data
{
    //Used for merging x,y pos in grid and the value
    public class PosValue
    {
        public Vector2Int pos;
        public int value;

        public PosValue(Vector2Int pos, int value)
        {
            this.pos = pos;
            this.value = value;
        }
    }
}