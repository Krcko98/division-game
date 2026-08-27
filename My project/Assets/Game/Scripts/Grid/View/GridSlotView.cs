using Grid.Data;
using UnityEngine;

namespace Grid.View
{
    public class GridSlotView : MonoBehaviour
    {
        [SerializeField] private RectTransform rootParent;

        public RectTransform RootParent { get => rootParent; }

        public void Init(GridSlotViewData data)
        {
            transform.SetParent(data.parent);
            transform.localScale = Vector3.one;
        }
    }
}