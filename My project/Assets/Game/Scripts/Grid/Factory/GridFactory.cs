using Grid.Controller;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grid.Factory
{
    public static class GridFactory
    {
        public static Dictionary<string, object> gridObjects = new Dictionary<string, object>();

        public static readonly string slotKeepID = "slot_keep_";
        public static readonly string slotID = "slot_";
        public static readonly string itemID = "item_";

        //We need to load all important prefabs at the start so we can reuse on the fly
        //Or we can have events and have easy reskins
        public static void LoadAllResources()
        {
            List<GridSlotController> slots = Resources.LoadAll<GridSlotController>("Grid/Slots/Standard").ToList();
            List<GridItemController> items = Resources.LoadAll<GridItemController>("Grid/Items/").ToList();
            List<GridSlotKeepController> slotsKeep = Resources.LoadAll<GridSlotKeepController>("Grid/Slots/Keep").ToList();

            int i = 0;
            for (i = 0; i < slots.Count; i++)
            {
                gridObjects.Add(string.Format("{0}{1}", slotID, i), slots[i]);
            }

            for (i = 0; i < items.Count; i++)
            {
                gridObjects.Add(string.Format("{0}{1}", itemID, i), items[i]);
            }

            for (i = 0; i < slotsKeep.Count; i++)
            {
                gridObjects.Add(string.Format("{0}{1}", slotKeepID, i), slotsKeep[i]);
            }
        }

        public static void UnloadAllResources()
        {
            gridObjects.Clear();
        }

        public static GridSlotController CreateGridSlot(GridSlotController pref)
        {
            GridSlotController slot = Object.Instantiate(
                original: pref,
                parent: null,
                position: Vector3.zero,
                rotation: Quaternion.identity
            );

            return slot;
        }

        public static GridSlotKeepController CreateGridSlotKeep(GridSlotKeepController pref)
        {
            GridSlotKeepController slot = Object.Instantiate(
                original: pref,
                parent: null,
                position: Vector3.zero,
                rotation: Quaternion.identity
            );

            return slot;
        }

        /// <summary>
        /// Return the item prefab that we already loaded
        /// There must be at least one default item
        /// </summary>
        /// <param name="id">id of 0 is always available</param>
        /// <returns></returns>
        public static GridItemController CreateGridItem(int id)
        {
            GridItemController item = Object.Instantiate(
                original: gridObjects[MergeObjectID(itemID, id)] as GridItemController,
                parent: null,
                position: Vector3.zero,
                rotation: Quaternion.identity
            );

            return item;
        }

        public static void DespawnGridItem(GridItemController item)
        {
            Object.Destroy(item.gameObject);
        }

        public static string MergeObjectID(string idBase, int id)
        {
            return string.Format("{0}{1}", idBase, id);
        }
    }
}