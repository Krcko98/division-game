using UnityEngine;

[CreateAssetMenu(fileName = "GameplayDataSO", menuName = "Scriptable Objects/GameplayDataSO")]
public class GameplayDataSO : ScriptableObject
{
    [SerializeField] private float inventoryLoadPushDelay;
    [SerializeField] private Vector2 draggedItemSize;
    [SerializeField] private GridItemDataSO inventoryItemData;
    [SerializeField] private GridItemDataSO dragItemData;

    public float InventoryLoadPushDelay { get => inventoryLoadPushDelay; }
    public Vector2 DraggedItemSize { get => draggedItemSize; }
    public GridItemDataSO DragItemData { get => dragItemData; }
    public GridItemDataSO InventoryItemData { get => inventoryItemData; }
}
