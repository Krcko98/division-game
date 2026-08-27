using System;
using System.Collections.Generic;
using UnityEngine;

namespace SM
{
    [Serializable]
    public abstract class StateMachine
    {
        protected string currentState = "";
        protected Dictionary<string, State> states = new Dictionary<string, State>();
        public Dictionary<string, object> data = new Dictionary<string, object>();

        public virtual void Init(string defaultState = "")
        {
            foreach((string name, State state) in states)
            {
                state.Init(this);
                if (name == currentState) ChangeState(name);
            }
            
            if(defaultState != string.Empty) ChangeState(defaultState);
        }

        public void Update()
        {
            if(currentState != string.Empty)
            {
                states[currentState].Update();
            }
        }

        public void ChangeState(string newState)
        {
            if(currentState != string.Empty)
            {
                states[currentState].Exit();
            }

            currentState = newState;

            if(currentState != string.Empty)
            {
                states[currentState].Enter();
            }
        }
    }
}
