using Grid.Controller;
using Grid.Data;
using Grid.Factory;
using SM;
using Grid.Data.Item;
using UnityEngine;
using Grid.Static.Helper;
using CorTasks;
using CorTasks.CoroutineExtension;
using CorTasks.CoroutineExtension.Presets;
using System.Collections;
using System;

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
                    () => fsm.inventoryController.SetInteractivity(true)
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

        private void pushItem()
        {
            int num = GameplayHelper.FetchRandomNumber();

            GridItemController item = GridFactory.CreateGridItem(0);
            item.Init(new GridItemControllerData(
                parent: null,
                itemData: new GridItemData(
                    number: num,
                    color: GridItemHelper.FetchColor(num)
                )
            ));

            fsm.inventoryController.PushSlotItemToQueue(item);
        }

        #region Wait
        private IEnumerator pushItems(float delay, int amount, Action callback)
        {
            int i=amount;

            while(i>0)
            {
                yield return new WaitForSeconds(delay);

                pushItem();

                i--;
            }

            callback?.Invoke();
        }
        #endregion
    }
}