using Grid.Gameplay.Rules.Data;
using Grid.Static.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<Vector2Int> neighborRules = new List<Vector2Int>()
        {
            up,
            down,
            left,
            right
        };

        private Rule gridRule;

        public void Init()
        {
            gridRule = GameRulesHelper.GetRule(GameRulesHelper.RuleType.division);
        }

        public void InsertItem(PosValue item, Action<GameRuleCalcResult> gridResolved)
        {
            setPosItemValue(item);

            CalculateGrid(
                item,
                gridResolved    
            );
        }

        #region Calc
        private void CalculateGrid(PosValue insertedItem = null, Action<GameRuleCalcResult> gridResolved = null)
        {
            Debug.Log("Start : " + grid.GridFormat());

            PosValue currentValue = new PosValue(Vector2Int.zero, 0);
            List<RuleResult> neighborResults = new List<RuleResult>();
            Dictionary<Vector2Int, PosValue> itemsTraversed = new Dictionary<Vector2Int, PosValue>();

            //First check the grid from the position the item was inserted to check neighbors
            if (insertedItem != null)
            {
                currentValue.Copy(insertedItem);

                neighborResults = checkNeighbors(currentValue);
                if (neighborResults.Count != 0)
                {
                    GameRuleCalcResult result = applyNeighborResultsToGrid(
                        neighborResults: neighborResults,
                        currentItem: currentValue,
                        exclusions: itemsTraversed
                    );

                    itemsTraversed.AddRange(neighborResults.ConvertAll((RuleResult rule) => rule.item2));

                    gridResolved?.Invoke(result);
                }
                else
                {
                    gridResolved?.Invoke(new GameRuleCalcResult(new List<PosValue>(), 0, grid.IsFull()));
                }
            }

            Debug.Log("Finished : " + grid.GridFormat());
        }

        /*private void calculateGrid()
        {
            //Traverse the grid @TODO:Simple algo, fix if possible
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 0) continue;

                    currentValue.pos = new Vector2Int(i, j);
                    currentValue.value = grid[i][j];

                    neighborResults = checkNeighbors(currentValue);

                    if (neighborResults.Count == 0) continue;
                    if (itemsTraversed.ContainsKey(currentValue.pos)) continue;

                    applyNeighborResultsToGrid(
                        neighborResults: neighborResults,
                        currentItem: currentValue,
                        exclusions: itemsTraversed
                    );

                    itemsTraversed.AddRange(neighborResults.ConvertAll((RuleResult rule) => rule.item2));
                }
            }
        }*/

        private List<RuleResult> checkNeighbors(PosValue currentValue)
        {
            List<RuleResult> neighborResults = new List<RuleResult>();

            //Go through all rules and check neighbors for rule matches
            foreach (Vector2Int neighborRule in neighborRules)
            {
                Vector2Int neighbor = currentValue.pos + neighborRule;
                if (!grid.GridContainsItem(neighbor)) continue;

                RuleResult result = gridRule.RuleProcessor(currentValue, grid.GetPosValue(neighbor));

                if (result.ruleFulfilled)
                {
                    neighborResults.Add(result);
                }
            }

            return neighborResults;
        }

        //Take all rule matched neighbors and set them to the grid calculating new values for neighbors and the currentItem
        private GameRuleCalcResult applyNeighborResultsToGrid(List<RuleResult> neighborResults, PosValue currentItem, Dictionary<Vector2Int, PosValue> exclusions)
        {
            if (neighborResults.Count == 0) return new GameRuleCalcResult(new List<PosValue>(), 0, false);

            PosValue smallestDividableNeighbor = new PosValue(Vector2Int.zero, 0);

            Dictionary<Vector2Int, PosValue> operatedOn = new Dictionary<Vector2Int, PosValue>();

            foreach (RuleResult result in neighborResults)
            {
                if(smallestDividableNeighbor.value < result.resultValue)
                {
                    smallestDividableNeighbor = new PosValue(result.item2.pos, result.resultValue);
                }

                //Set value of main neighbor
                operatedOn.SetIfExistsOrAddByRule(new PosValue(result.item1.pos, result.resultValue));

                //Set value of sub neighbor
                operatedOn.SetIfExistsOrAddByRule(new PosValue(result.item2.pos, 0));
            }

            if (operatedOn[currentItem.pos].value != 0)
            {
                operatedOn.SetIfExistsOrAddByRule(new PosValue(currentItem.pos, smallestDividableNeighbor.value));
            }

            int bestItemValue = getItemValue(smallestDividableNeighbor.pos);

            //Set our operation result on neighbors to the grid
            foreach (Vector2Int pos in operatedOn.Keys)
            {
                setPosItemValue(operatedOn[pos]);
            }

            Debug.Log("Settable value : " + grid.GridFormat());

            return new GameRuleCalcResult(
                updatedItems: operatedOn.Values.ToList(), 
                score: GameRulesHelper.CalculateScore(operatedOn, bestItemValue),
                gridFull: grid.IsFull()
            );
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

        public void setPosItemValue(PosValue value)
        {
            setItemValue(value.pos, value.value);
        }
    }
}