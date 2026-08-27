using Grid.Factory;
using Grid.Static;
using SM;

namespace Grid.Gameplay.States
{
    public class GameplayFillInventoryState : State
    {
        protected GameplayFSM fsm;

        public override void Init(StateMachine sm)
        {
            base.Init(sm);

            fsm = sm as GameplayFSM;
        }

        public override void Enter()
        {
            base.Enter();

            fsm.inventoryController.PushSlotItemToQueue(GridFactory.CreateGridItem(0));
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}