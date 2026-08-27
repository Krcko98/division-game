using Grid.Controller;
using UnityEngine;

namespace Grid.Gameplay.Data
{
    public class GameplayFSMData
    {
        public GridController gridController;
        public GridInventoryController inventoryController;

        public GameplayFSMData(GridController gridController, GridInventoryController inventoryController)
        {
            this.gridController = gridController;
            this.inventoryController = inventoryController;
        }
    }
}