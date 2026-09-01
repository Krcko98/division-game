using Grid.Data;
using Grid.View;
using UnityEngine;

namespace Grid.Controller
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private ScoreView scoreView;
        [SerializeField] private int score;

        public int Score { get => score; }

        public void Init(ScoreControllerData data)
        {
            score = 0;

            scoreView.Init(
                new ScoreViewData(
                    
                )
            );

            AddScore(score);
        }

        public void SetScore(int score)
        {
            scoreView.UpdateText(score);
        }

        public void AddScore(int scoreAdd)
        {
            this.score += scoreAdd;
            SetScore(score);
        }
    }
}