using Grid.Data;
using TMPro;
using UnityEngine;

namespace Grid.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public void Init(ScoreViewData data)
        {

        }

        public void UpdateText(int score)
        {
            scoreText.text = string.Format("{0}", score);
        }
    }
}