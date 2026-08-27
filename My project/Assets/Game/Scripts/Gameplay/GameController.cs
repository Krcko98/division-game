using Grid.Controller;
using UnityEngine;
using Grid.Data;
using Grid.Gameplay.Data;
using Grid.Factory;

namespace Grid.Gameplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GridController gridController;
        [SerializeField] private GridInventoryController gridInventoryController;

        private GameStateFSM gameFSM;
        private GameplayFSM gameplayFSM;

        private void Awake()
        {

        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            GridFactory.LoadAllResources();

            gridController.Init(new GridControllerData());
            gridInventoryController.Init(new GridInventoryControllerData(
                inventorySize: 3    
            ));
            
            gameFSM = new GameStateFSM(
                new GameStateFSMData(
                    gridController: gridController
                )
            );
            gameFSM.Init();

            gameplayFSM = new GameplayFSM(
                new GameplayFSMData(
                    gridController: gridController,
                    inventoryController: gridInventoryController
                )
            );

            gameplayFSM.Init();
        }

        private void OnDisable()
        {
            GridFactory.UnloadAllResources();
        }
    }
}