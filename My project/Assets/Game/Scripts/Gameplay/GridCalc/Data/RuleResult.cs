namespace Grid.Gameplay.Rules.Data
{
    //Result for the rule we checked on items that contains calculated value and success result
    public class RuleResult
    {
        public int resultValue;
        public bool ruleFulfilled;

        public RuleResult(int resultValue, bool ruleFulfilled)
        {
            this.resultValue = resultValue;
            this.ruleFulfilled = ruleFulfilled;
        }
    }
}