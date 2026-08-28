using Grid.Data;
using Grid.Data.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.View
{
    public class GridItemView : MonoBehaviour
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private Image bg;
        [SerializeField] private TextMeshProUGUI text;

        private GridItemData itemData;

        public void Init(GridItemViewData data)
        {
            SetParent(data.parent);
            itemData = data.itemData;

            bg.color = itemData.color;
            text.text = string.Format("{0}", itemData.number);
        }

        public void SetParent(RectTransform parent)
        {
            transform.SetParent(parent);

            if(parent == null) return;

            transform.localScale = Vector3.one;
            rect.sizeDelta = parent.sizeDelta;
            rect.anchoredPosition = Vector3.zero;
        }

        public void SetAbsolutePosition(Vector2 pos)
        {
            rect.transform.position = pos;
        }

        public void SetSize(Vector2 size)
        {
            rect.sizeDelta = size;
        }
    }
}