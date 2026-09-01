namespace Grid.Gameplay.Rules.Data
{
    //Result for the rule we checked on items that contains calculated value and success result
    public class RuleResult
    {
        public int resultValue;
        public bool ruleFulfilled;
        public PosValue item1;
        public PosValue item2;

        public RuleResult(int resultValue, bool ruleFulfilled, PosValue item1, PosValue item2)
        {
            this.resultValue = resultValue;
            this.ruleFulfilled = ruleFulfilled;
            this.item1 = item1;
            this.item2 = item2;
        }
    }
}