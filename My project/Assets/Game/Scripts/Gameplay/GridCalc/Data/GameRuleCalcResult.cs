using System.Collections.Generic;

namespace Grid.Gameplay.Rules.Data
{
    //Result used for API return
    public class GameRuleCalcResult
    {
        public List<PosValue> updatedItems;

        public GameRuleCalcResult(List<PosValue> updatedItems)
        {
            this.updatedItems = updatedItems;
        }
    }
}
