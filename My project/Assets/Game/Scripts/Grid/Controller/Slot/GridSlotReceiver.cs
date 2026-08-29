using UnityEngine;

namespace Grid.Controller.Slot
{
    public class GridSlotReceiver : MonoBehaviour
    {
        [SerializeField] private GridSlotController gridSlot;

        public GridSlotController GridSlot { get => gridSlot; }
    }
}