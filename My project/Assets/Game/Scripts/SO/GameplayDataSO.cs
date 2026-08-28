using UnityEngine;

[CreateAssetMenu(fileName = "GameplayDataSO", menuName = "Scriptable Objects/GameplayDataSO")]
public class GameplayDataSO : ScriptableObject
{
    [SerializeField] private float inventoryLoadPushDelay;
    [SerializeField] private Vector2 draggedItemSize;

    public float InventoryLoadPushDelay { get => inventoryLoadPushDelay; }
    public Vector2 DraggedItemSize { get => draggedItemSize; }
}
