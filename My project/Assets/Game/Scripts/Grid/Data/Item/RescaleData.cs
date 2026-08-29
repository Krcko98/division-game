using UnityEngine;

namespace Grid.Data.Item
{
    public class RescaleData
    {
        public Vector3 rescaleFromSize;
        public Vector3 rescaleToSize;
        public float startDelay;
        public float duration;

        public RescaleData(
            Vector3 rescaleFromSize,
            Vector3 rescaleToSize,
            float startDelay,
            float duration
        )
        {
            this.rescaleFromSize = rescaleFromSize;
            this.rescaleToSize = rescaleToSize;
            this.startDelay = startDelay;
            this.duration = duration;
        }
    }
}