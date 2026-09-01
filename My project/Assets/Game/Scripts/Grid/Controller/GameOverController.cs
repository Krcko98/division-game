using Grid.Data;
using Grid.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grid.Controller
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private GameOverView gameOverView;

        public void Init(GameOverControllerData data)
        {
            gameOverView.Init(
                new GameOverViewData(
                    restartClicked: restartClicked
                )
            );
        }

        private void restartClicked()
        {
            SceneManager.LoadScene(0);
        }

        public void SetScore(int score)
        {
            gameOverView.SetScore(score);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}