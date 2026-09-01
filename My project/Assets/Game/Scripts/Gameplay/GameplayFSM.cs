using System;
using Grid.Controller;
using Grid.Gameplay.Data;
using Grid.Gameplay.States;
using SM;

namespace Grid.Gameplay
{
    [Serializable]
    public class GameplayFSM : StateMachine
    {
        public static readonly string openState = "open_ready";
        public static readonly string inventoryFillState = "fill_inventory";
        public static readonly string dragState = "drag_element_selection";
        public static readonly string gridFillState = "grid_fill";
        public static readonly string keepFillState = "keep_fill";

        public GridController gridController;
        public GridInventoryController inventoryController;
        public ScoreController scoreController;
        public GameOverController gameOverController;

        public GameplayFSM(GameplayFSMData data)
        {
            gridController = data.gridController;
            inventoryController = data.inventoryController;
            scoreController = data.scoreController;
            gameOverController = data.gameOverController;
        }

        public override void Init(string defaultState = "")
        {
            states[inventoryFillState] = new GameplayFillInventoryState();
            states[openState] = new GameplayOpenState();
            states[dragState] = new GameplayDragState();
            states[gridFillState] = new GameplayFillGridState();

            base.Init(defaultState);

            ChangeState(inventoryFillState);
        }
    }
}