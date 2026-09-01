using System;
using System.Collections;
using UnityEngine;

namespace CorTasks.CoroutineExtension.Presets
{
    public static class TaskPresets
    {
        /// <summary>
        /// Wait until the end of the current frame
        /// </summary>
        /// <param name="callback">Method to be called after coroutine has finished</param>
        /// <returns></returns>
        public static IEnumerator DelayEndOfFrame(Action callback = null)
        {
            yield return new WaitForEndOfFrame();

            if (callback != null)
            {
                callback();
            }
        }

        /// <summary>
        /// Wait until the new frame
        /// </summary>
        /// <param name="callback">Method to be called after coroutine has finished</param>
        /// <returns></returns>
        public static IEnumerator DelayStartOfNewFrame(Action callback = null)
        {
            yield return null;

            if (callback != null)
            {
                callback();
            }
        }

        /// <summary>
        /// Wait for N frames
        /// </summary>
        /// <param name="frames">Wait for this amount of frames</param>
        /// <param name="callback">Method to be called after coroutine has finished</param>
        /// <returns></returns>
        public static IEnumerator DelayForNFrames(int frames, Action callback = null)
        {
            int n = 0;

            while(n < frames)
            {
                yield return null;
                n++;
            }

            if(callback != null)
            {
                callback();
            }
        }

        public static IEnumerator DelayForSeconds(float seconds, Action callback = null)
        {
            WaitForSeconds waitSeconds = new WaitForSeconds(seconds);
            
            yield return waitSeconds;

            callback?.Invoke();
        }
    }
}