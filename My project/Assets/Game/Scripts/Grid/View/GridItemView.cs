using System;
using CorTasks.CoroutineExtension;
using CorTasks.CoroutineExtension.Presets;
using DG.Tweening;
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
            
            if(itemData.itemData.Use)
            {
                rect.transform.DOPunchScale(
                    duration: itemData.itemData.AttachPunchDuration,
                    punch: itemData.itemData.AttachPunchScale
                );
            }
        }

        public void SetAbsolutePosition(Vector2 pos)
        {
            rect.transform.position = pos;
        }

        public void SetSize(Vector2 size)
        {
            rect.sizeDelta = size;
        }

        public void Rescale(RescaleData data, Action finishedRescaleCallback = null)
        {
            rect.transform.localScale = data.rescaleFromSize;

            new Task(TaskPresets.DelayForSeconds(
                seconds: data.startDelay,
                callback: () =>
                {
                    if(rect == null) return;

                    if(finishedRescaleCallback == null)
                    {
                        rect.transform.DOScale(
                            endValue: data.rescaleToSize, 
                            duration: data.duration
                        );
                    }
                    else
                    {
                        TweenCallback tweenCallback = new TweenCallback(finishedRescaleCallback);
                        rect.transform.DOScale(
                            endValue: data.rescaleToSize, 
                            duration: data.duration
                        ).onComplete = tweenCallback;
                    }
                }
            ));
        }

        public void CompleteTweens()
        {
            rect.transform.DOComplete();
        }
    }
}