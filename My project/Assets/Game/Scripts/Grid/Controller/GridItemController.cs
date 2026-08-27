using Grid.Data;
using Grid.View;
using UnityEngine;

namespace Grid.Controller
{
    public class GridItemController : MonoBehaviour
    {
        [SerializeField] private GridItemView itemView;

        public void Init(GridItemControllerData data)
        {
            itemView.Init(new GridItemViewData(
                parent: data.parent,
                itemData: data.itemData
            ));
        }

        public void SetParent(RectTransform parent)
        {
            itemView.SetParent(parent);
        }
    }
}