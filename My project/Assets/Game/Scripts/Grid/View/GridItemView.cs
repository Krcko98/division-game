using Grid.Data;
using UnityEngine;

namespace Grid.View
{
    public class GridItemView : MonoBehaviour
    {
        [SerializeField] private RectTransform rect;

        public void Init(GridItemViewData data)
        {
            transform.SetParent(data.parent);
            transform.localScale = Vector3.one;
            rect.anchorMin = Vector3.zero;
            rect.anchorMax = Vector3.one;
        }
    }
}