using CorTasks.CoroutineExtension;
using Grid.Controller;
using Grid.Data;
using Grid.Data.Item;
using Grid.Factory;
using Grid.Static.Helper;
using SM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            fsm.inventoryController.SetInteractivity(false);
            
            Task task = new Task(
                pushItems(
                    delay: GameController.Instance.GameplayData.InventoryLoadPushDelay, 
                    amount: 3,
                    callback: () => {
                        fsm.inventoryController.SetInteractivity(true);

                        fsm.ChangeState(GameplayFSM.openState);
                    }
                )
            );
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void pushItem(Action pushedCallback = null)
        {
            int num = GameplayHelper.FetchRandomNumber();

            GridItemController item = GridFactory.CreateGridItem(0);
            item.Init(new GridItemControllerData(
                parent: null,
                itemData: new GridItemData(
                    number: num,
                    color: GridItemHelper.FetchColor(num),
                    itemData: GameController.Instance.GameplayData.InventoryItemData
                )
            ));

            fsm.inventoryController.PushSlotItemToQueue(item, pushedCallback);
        }

        #region Wait
        private IEnumerator pushItems(float delay, int amount, Action callback)
        {
            int i=amount;
            bool pushFinished = false;
            Action waitForPush = () => pushFinished = true;

            while (i > 0)
            {
                pushItem(waitForPush);

                callback += waitForPush.Invoke;
                yield return new WaitUntil(() => pushFinished);
                yield return new WaitForSeconds(delay);

                callback -= waitForPush.Invoke;
                pushFinished = false;

                i--;
            }

            callback?.Invoke();
        }
        #endregion
    }
}