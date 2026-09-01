using System;

namespace Grid.Data
{
    public class GameOverViewData
    {
        public Action restartClicked;

        public GameOverViewData(Action restartClicked)
        {
            this.restartClicked = restartClicked;
        }
    }
}