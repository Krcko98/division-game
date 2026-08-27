using SM;

namespace Grid.Gameplay.States
{
    public class GameFinishState : State
    {
        protected GameStateFSM fsm;

        public override void Init(StateMachine sm)
        {
            base.Init(sm);

            fsm = sm as GameStateFSM;
        }

        public override void Enter()
        {
            base.Enter();
            
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