using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid.Static.Helper
{
    public static class CanvasHelper
    {
        public static Vector2 ClickPointToCanvasPoint(Canvas canvas, PointerEventData eventData)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, 
                eventData.position, canvas.worldCamera, 
                out pos
            );
            return canvas.transform.TransformPoint(pos);
        }
    }
}