using UnityEngine;
using UnityEngine.UI;

namespace PanGame.Presentation
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField]
        private Button _okButton;

        public Button OkButton => _okButton;
        
        [SerializeField]
        private Button _watchBoardButton;
        
        public Button WatchBoardButton => _watchBoardButton;

        [SerializeField]
        private Text _scoreText;
        [SerializeField]
        private Text _bestScoreText;

        public Text ScoreText => _scoreText;

        public Text BestScoreText => _bestScoreText;
    }
}
