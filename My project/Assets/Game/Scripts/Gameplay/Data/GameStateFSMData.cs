using Grid.Controller;
using UnityEngine;

namespace Grid.Gameplay.Data
{
    public class GameStateFSMData
    {
        public GridController gridController;

        public GameStateFSMData(GridController gridController)
        {
            this.gridController = gridController;
        }
    }
}