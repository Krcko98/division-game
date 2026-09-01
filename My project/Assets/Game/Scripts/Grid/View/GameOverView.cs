using Grid.Controller;
using Grid.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.View
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private ScoreController scoreController;

        public void Init(GameOverViewData data)
        {
            restartButton.onClick.AddListener(() => data.restartClicked?.Invoke());
        }

        public void SetScore(int score)
        {
            scoreController.SetScore(score);
        }
    }
}