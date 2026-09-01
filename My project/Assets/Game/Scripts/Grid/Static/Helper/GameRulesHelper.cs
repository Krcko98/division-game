using System;
using System.Collections.Generic;
using System.Linq;
using Grid.Gameplay.Rules.Data;
using UnityEngine;
using Rule = Grid.Gameplay.Rules.Data.Rule;

namespace Grid.Static.Helper
{
    public static class GameRulesHelper
    {
        public enum RuleType
        {
            division = 0,
            COUNT
        }

        public static Rule GetRule(RuleType ruleType)
        {
            Func<PosValue, PosValue, RuleResult> rule = (PosValue item1, PosValue item2) =>
            {
                RuleResult result = new RuleResult(0, false, item1, item2);

                if(ruleType == RuleType.division)
                {
                    if (item2.value == 0) return result;

                    (PosValue val1, PosValue val2) resolve = item1.FindMax(item2);

                    result.resultValue = resolve.val1.value / resolve.val2.value;
                    result.ruleFulfilled = resolve.val1.value % resolve.val2.value == 0;
                    result.item1 = resolve.val1;
                    result.item2 = resolve.val2;
                }

                return result;
            };

            return new Rule(
                rule: rule
            );
        }

        public static string GridFormat(this int[][] item)
        {
            string format = "\n";

            for(int i=0; i<item.Length; i++)
            {
                string arrayString = "";

                for(int j=0; j<item[i].Length; j++)
                {
                    arrayString += string.Format("{0} ", item[i][j]);
                }

                format += string.Format("{0}{1}", arrayString, "\n");
            }
            return format;
        }

        public static bool GridContainsItem(this int[][] item, Vector2Int pos)
        {
            bool contains = false;

            if(
                pos.x >= 0 && pos.x < item.Length &&
                pos.y >= 0 && pos.y < item[0].Length
            )
            {
                contains = true;
            }

            return contains;
        }

        public static PosValue GetPosValue(this int[][] item, Vector2Int pos)
        {
            return new PosValue(pos, item[pos.x][pos.y]);
        }

        public static bool IsFull(this int[][] item)
        {
            for(int i=0; i<item.Length; i++)
            {
                for (int j = 0; j < item[i].Length; j++)
                {
                    if (item[i][j] <= 0) return false;
                }
            }

            return true;
        }

        public static RuleResult RuleProcessor(this Rule item, PosValue item1, PosValue item2)
        {
            return item.RuleFunc(item1, item2);
        }

        //Extension for dictionary that sets or ads based on availablity of keys in dict
        public static bool SetIfExistsOrAddByRule(this Dictionary<Vector2Int, PosValue> item, PosValue posValue)
        {
            //If we set the key and it exists return true
            if(item.ContainsKey(posValue.pos))
            {
                item[posValue.pos] = posValue;

                return true;
            }
            else
            {
                item.Add(posValue.pos, posValue);

                return false;
            }
        }

        //Extension for dictionary that adds a range of items
        public static void AddRange(this Dictionary<Vector2Int, PosValue> item, List<PosValue> posValues)
        {
            foreach(PosValue pos in posValues)
            {
                item.SetIfExistsOrAddByRule(pos);
            }    
        }

        public static void Copy(this PosValue item, PosValue copyFrom)
        {
            item.pos = copyFrom.pos;
            item.value = copyFrom.value;
        }

        public static (PosValue item1, PosValue item2) FindMax(this PosValue item1, PosValue item2)
        {
            if(item1.value > item2.value)
            {
                return new(item1, item2);
            }
            else
            {
                return new(item2, item1);
            }
        }

        public static int CalculateScore(Dictionary<Vector2Int, PosValue> items, int bestItemValue)
        {
            List<PosValue> updatedItems = new List<PosValue>();

            foreach(PosValue pos in items.Values)
            {
                updatedItems.Add(new PosValue(pos.pos, pos.value));
            }

            return updatedItems.Count * bestItemValue;
        }
    }
}