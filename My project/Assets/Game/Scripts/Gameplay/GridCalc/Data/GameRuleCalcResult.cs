using System.Collections.Generic;

namespace Grid.Gameplay.Rules.Data
{
    //Result used for API return
    public class GameRuleCalcResult
    {
        public List<PosValue> updatedItems;
        public int score;
        public bool boardFull;

        public GameRuleCalcResult(List<PosValue> updatedItems, int score, bool gridFull)
        {
            this.updatedItems = updatedItems;
            this.score = score;
            this.boardFull = gridFull;
        }
    }
}
