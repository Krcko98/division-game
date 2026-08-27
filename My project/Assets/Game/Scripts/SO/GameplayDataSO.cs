using UnityEngine;

[CreateAssetMenu(fileName = "GameplayDataSO", menuName = "Scriptable Objects/GameplayDataSO")]
public class GameplayDataSO : ScriptableObject
{
    [SerializeField] private float inventoryLoadPushDelay;

    public float InventoryLoadPushDelay { get => inventoryLoadPushDelay; }
}
