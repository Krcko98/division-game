using System.Collections;
using UnityEngine;

namespace CorTasks.CoroutineExtension
{
    public class TaskManager : MonoBehaviour
    {
		static TaskManager Instance = null;

		void Awake()
        {
			if(Instance == null)
            {
				Instance = this;
				DontDestroyOnLoad(gameObject);
            }
			else
            {
				Destroy(gameObject);
            }
        }

		public static TaskState CreateTask(IEnumerator coroutine)
		{
			if (Instance == null)
			{
				GameObject go = new GameObject("TaskManager");
				Instance = go.AddComponent<TaskManager>();
			}
			return new TaskState(coroutine);
		}

		public class TaskState
		{
			public bool Running
			{
				get
				{
					return running;
				}
			}

			public bool Paused
			{
				get
				{
					return paused;
				}
			}

			public delegate void FinishedHandler(bool manual);
			public event FinishedHandler Finished;

			IEnumerator coroutine;
			bool running;
			bool paused;
			bool stopped;

			public TaskState(IEnumerator c)
			{
				coroutine = c;
			}

			public void Pause()
			{
				paused = true;
			}

			public void Unpause()
			{
				paused = false;
			}

			public void Start()
			{
				running = true;
				Instance.StartCoroutine(CallWrapper());
			}

			public void Stop()
			{
				stopped = true;
				running = false;
			}

			IEnumerator CallWrapper()
			{
				yield return null;
				IEnumerator e = coroutine;
				while (running)
				{
					if (paused)
						yield return null;
					else
					{
						if (e != null && e.MoveNext())
						{
							yield return e.Current;
						}
						else
						{
							running = false;
						}
					}
				}

				FinishedHandler handler = Finished;
				if (handler != null)
					handler(stopped);
			}
		}
	}
}