using Grid.Controller;
using Grid.Gameplay.Data;
using Grid.Gameplay.States;
using SM;
using System;
using UnityEngine;

namespace Grid.Gameplay
{
    [Serializable]
    public class GameStateFSM : StateMachine
    {
        public static readonly string startState = "start";
        public static readonly string finishState = "finish";
        public static readonly string restartState = "restart";

        public GridController gridController;

        public GameStateFSM(GameStateFSMData data)
        {
            gridController = data.gridController;
        }

        public override void Init(string defaultState = "")
        {
            states[startState] = new GameStartState();
            states[finishState] = new GameFinishState();
            states[restartState] = new GameRestartState();

            base.Init(defaultState);

            ChangeState(startState);
        }
    }
}