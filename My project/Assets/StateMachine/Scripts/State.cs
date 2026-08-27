using UnityEngine;

namespace SM
{
    public abstract class State
    {
        protected StateMachine sm;

        public virtual void Init(StateMachine sm) 
        {
            this.sm = sm;
        }

        public virtual void Enter(){}
        public virtual void Update(){}

        public virtual void Exit(){}
    }
}