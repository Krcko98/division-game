using System;

namespace Grid.Gameplay.Rules.Data
{
    //Rule for the relationship between items to apply when we check for matches
    public class Rule
    {
        public Func<PosValue, PosValue, RuleResult> rule;

        public Rule(Func<PosValue, PosValue, RuleResult> rule)
        {
            this.rule = rule;
        }
    }
}