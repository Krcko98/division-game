using UnityEngine;

[CreateAssetMenu(fileName = "GridItemDataSO", menuName = "Scriptable Objects/GridItemDataSO")]
public class GridItemDataSO : ScriptableObject
{
    [SerializeField] private float attachPunchDuration;
    [SerializeField] private Vector2 attachPunchScale;
    [SerializeField] private bool use;

    public float AttachPunchDuration { get => attachPunchDuration; }
    public Vector2 AttachPunchScale { get => attachPunchScale; }
    public bool Use { get => use; }
}
