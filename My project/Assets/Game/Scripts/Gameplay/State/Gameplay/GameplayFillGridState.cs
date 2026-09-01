using Grid.Controller;
using Grid.Data;
using Grid.Data.Item;
using Grid.Factory;
using Grid.Gameplay.Rules.Data;
using Grid.Static.Helper;
using SM;
using System;
using UnityEngine;

namespace Grid.Gameplay.States
{
    public class GameplayFillGridState : State
    {
        protected GameplayFSM fsm;

        private GridSlotController dropSlot;
        private int dropSlotNumber;

        private string dropSlotKey = "dropSlot";
        private string dropSlotNumberKey = "dropSlotNumber";

        private RescaleData draggedSlotRescaleData = new RescaleData(
            rescaleFromSize: Vector3.zero,
            rescaleToSize: Vector3.one,
            startDelay: 0,
            duration: 0.3f
        );
        private RescaleData draggedItemRescaleData = new RescaleData(
            rescaleFromSize: Vector3.one,
            rescaleToSize: Vector3.zero,
            startDelay: 0,
            duration: 0.2f
        );

        private RescaleData slotAttachRescaleData = new RescaleData(
            rescaleFromSize: Vector3.zero,
            rescaleToSize: Vector3.one,
            startDelay: 0,
            duration: 0.3f
        );

        private RescaleData slotDetachRescaleData = new RescaleData(
            rescaleFromSize: Vector3.zero,
            rescaleToSize: Vector3.zero,
            startDelay: 0,
            duration: 0
        );

        public override void Init(StateMachine sm)
        {
            base.Init(sm);

            fsm = sm as GameplayFSM;
        }

        public override void Enter()
        {
            base.Enter();

            dropSlot = fsm.data[dropSlotKey] as GridSlotController;
            dropSlotNumber = (int)fsm.data[dropSlotNumberKey];

            GameController.Instance.GameRules.InsertItem(
                item: new PosValue(
                    pos: new Vector2Int((int)dropSlot.Pos.x, (int)dropSlot.Pos.y),
                    value: dropSlotNumber
                ),
                gridResolved: (GameRuleCalcResult result) =>
                {
                    UpdateGrid(result);
                }
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

        #region Event
        private void UpdateGrid(GameRuleCalcResult result)
        {
            fsm.scoreController.AddScore(result.score);
            GridSlotController slot;

            //Fill all slots with items that correspond to their new values and detach and remove all items that no longer exist
            foreach(PosValue pos in result.updatedItems)
            {
                slot = fsm.gridController.Slots[pos.pos];

                if(pos.value == 0)
                {
                    if(slot.AttachedItem != null)
                    {
                        slot.DetachItem(slotDetachRescaleData);
                    }
                }
                else
                {
                    if (slot.AttachedItem != null)
                    {
                        slot.DetachItem(slotDetachRescaleData, (GridSlotController slot) =>
                        {
                            GridItemController item = GridFactory.CreateGridItem(0);
                            item.Init(new GridItemControllerData(
                                parent: null,
                                itemData: new GridItemData(
                                    number: pos.value,
                                    color: GridItemHelper.FetchColor(pos.value),
                                    itemData: GameController.Instance.GameplayData.DragAttachItemData
                                )
                            ));

                            slot.AttachItem(item);
                            slot.AttachedItem.Rescale(
                                slotAttachRescaleData
                            );
                        });
                    }
                }
            }

            if(result.boardFull)
            {
                fsm.gameOverController.SetScore(fsm.scoreController.Score);
                fsm.gameOverController.Show();
            }

            //Our pointer is over slot that is selected
            /*if(slotHover != null)
            {
                //If item is not already on the selected slot we will create a new item
                //Otherwise, we just continue
                if(slotHover.AttachedItem == null)
                {
                    int num = spawnedItem.ItemData.number;

                    GridItemController item = GridFactory.CreateGridItem(0);
                    item.Init(new GridItemControllerData(
                        parent: null,
                        itemData: new GridItemData(
                            number: num,
                            color: GridItemHelper.FetchColor(num),
                            itemData: GameController.Instance.GameplayData.DragAttachItemData
                        )
                    ));

                    slotHover.AttachItem(item);
                    slotHover.AttachedItem.Rescale(
                        slotAttachRescaleData
                    );

                    //Start grid gameplay rule calc and gather data for resolving visual elements
                    if (!(slotHover as GridSlotKeepController))
                    {
                        GameController.Instance.GameRules.InsertItem(
                            item: new PosValue(
                                pos: new Vector2Int((int)slotHover.Pos.x, (int)slotHover.Pos.y),
                                value: num
                            ),
                            gridResolved: (GameRuleCalcResult result) =>
                            {
                                 
                            }
                        );
                    }

                    //If we are dragging a keep tile do not push anything just remove it from keep
                    if (!(draggingSlot as GridSlotKeepController))
                    {
                        fsm.inventoryController.PopItemInSlotFromQueue(
                            data: slotDetachRescaleData,
                            popCallback: (GridSlotController slot) =>
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

                                fsm.inventoryController.PushSlotItemToQueue(item);
                            }
                        );
                    }
                    else
                    {
                        draggingSlot.DetachItem(
                            slotDetachRescaleData
                        );
                    }
                }
            }

            draggingSlot.AttachedItem.Rescale(
                draggedSlotRescaleData
            );

            spawnedItem.Rescale(
                draggedItemRescaleData,
                (GridItemController item) => 
                {
                    GridFactory.DespawnGridItem(item);

                    spawnedItem = null;
                    draggingSlot = null;

                    fsm.ChangeState(GameplayFSM.openState);
                }
            );*/
        }
        #endregion

        #region Calc
        
        #endregion
    }
}