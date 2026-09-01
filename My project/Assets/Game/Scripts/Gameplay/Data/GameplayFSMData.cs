using Grid.Controller;
using UnityEngine;

namespace Grid.Gameplay.Data
{
    public class GameplayFSMData
    {
        public GridController gridController;
        public GridInventoryController inventoryController;
        public ScoreController scoreController;
        public GameOverController gameOverController;

        public GameplayFSMData(GridController gridController, GridInventoryController inventoryController, ScoreController scoreController, GameOverController gameOverController)
        {
            this.gridController = gridController;
            this.inventoryController = inventoryController;
            this.scoreController = scoreController;
            this.gameOverController = gameOverController;
        }
    }
}