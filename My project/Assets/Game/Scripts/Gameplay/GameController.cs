using Grid.Controller;
using UnityEngine;
using Grid.Data;
using Grid.Gameplay.Data;
using Grid.Factory;
using Grid.Static;

namespace Grid.Gameplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GridController gridController;
        [SerializeField] private GridInventoryController gridInventoryController;
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private GameplayDataSO gameplayData;

        public static GameController Instance = null;

        private GameStateFSM gameFSM;
        private GameplayFSM gameplayFSM;

        public GameplayDataSO GameplayData { get => gameplayData; }
        public Canvas MainCanvas { get => mainCanvas; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
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
            GlobalEventBus.RemoveSubscriptions();
        }
    }
}