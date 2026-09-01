using System;

namespace Grid.Gameplay.Rules.Data
{
    //Rule for the relationship between items to apply when we check for matches
    public class Rule
    {
        private Func<PosValue, PosValue, RuleResult> ruleFunc;

        public Func<PosValue, PosValue, RuleResult> RuleFunc { get => ruleFunc; }

        public Rule(Func<PosValue, PosValue, RuleResult> rule)
        {
            this.ruleFunc = rule;
        }
    }
}