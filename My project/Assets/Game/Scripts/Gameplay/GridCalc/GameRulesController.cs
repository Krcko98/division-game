using System.Collections.Generic;
using Grid.Gameplay.Rules.Data;
using Grid.Static.Helper;
using UnityEngine;

namespace Grid.Gameplay.Rules
{
    public class GameRulesController
    {
        public static Vector2Int up = new Vector2Int(-1, 0);
        public static Vector2Int down = new Vector2Int(1, 0);
        public static Vector2Int left = new Vector2Int(0, -1);
        public static Vector2Int right = new Vector2Int(0, 1);

        //Main grid used for comparison, loading active numbers and all calculations
        private int[][] grid = new int[3][]
        {
            new int[3] { 0, 0, 0 },
            new int[3] { 0, 0, 0 },
            new int[3] { 0, 0, 0 }
        };
    
        //List of rules saying what Neighboor means
        private List<Vector2> neighboorsRule = new List<Vector2>()
        {
            up,
            down,
            left,
            right
        };

        public void Init()
        {
            Debug.Log(grid[1+up.x][1+up.y]);
            Debug.Log(grid[1+down.x][1+down.y]);
            Debug.Log(grid[1+left.x][1+left.y]);
            Debug.Log(grid[1+right.x][1+right.y]);


        }

        public GameRuleCalcResult InsertItem(PosValue item)
        {
            setItemValue(item.pos, item.value);

            applyRulesToGrid();

            return null;
        }

        #region Calc
        private void applyRulesToGrid()
        {
            Debug.Log(grid.GridFormat());
        }
        #endregion

        public int getItemValue(Vector2Int pos)
        {
            return grid[pos.x][pos.y];
        }

        public void setItemValue(Vector2Int pos, int value)
        {
            grid[pos.x][pos.y] = value;
        }
    }
}