using System;
using System.Diagnostics;
using Grid.Gameplay.Rules.Data;
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
                RuleResult result = new RuleResult(0, false);

                if(ruleType == RuleType.division)
                {
                    result.resultValue = item1.value / item2.value;
                    result.ruleFulfilled = item1.value % item2.value == 0;
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
    }
}