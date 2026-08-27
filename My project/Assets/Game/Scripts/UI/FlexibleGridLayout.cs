using UnityEngine;
using UnityEngine.UI;

namespace Grid.Layout
{
    public class FlexibleGridLayout : LayoutGroup
    {
        public enum Fitter
        {
            Uniform,
            Width,
            Height
        }

        [SerializeField] private Fitter fitter;

        [SerializeField] private int rows;
        [SerializeField] private int columns;
        [SerializeField] private Vector2 cellSize;
        [SerializeField] private Vector2 spacing;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            float root = Mathf.Sqrt(rectChildren.Count);
            rows = Mathf.CeilToInt(root);
            columns = Mathf.CeilToInt(root);

            switch (fitter)
            {
                case Fitter.Width:
                rows = Mathf.CeilToInt(rectChildren.Count / (float)columns);
                break;
                case Fitter.Height:
                rows = Mathf.CeilToInt(rectChildren.Count / (float)rows);
                break;
            }

            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float cellWidth = (parentWidth - padding.left - padding.right - spacing.x * (columns - 1)) / (float)columns;
            float cellHeight = (parentHeight - padding.top - padding.bottom - spacing.y * (rows - 1)) / (float)rows;

            cellSize.x = cellWidth;
            cellSize.y = cellHeight;

            int columnCount = 0;
            int rowCount = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / columns;
                columnCount = i % columns;

                var rect = rectChildren[i];

                var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
                var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

                SetChildAlongAxis(rect, 0, xPos, cellSize.x);
                SetChildAlongAxis(rect, 1, yPos, cellSize.y);
            }
        }

        public override void CalculateLayoutInputVertical() { }

        public override void SetLayoutHorizontal() { }

        public override void SetLayoutVertical() { }
    }
}