using PanGame.Presentation;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShogiOnline.Presentation
{
    public class WatchBoardPresenter : MonoBehaviour
    {
        [SerializeField]
        private WatchBoardView _watchBoardView = null;

        [SerializeField]
        private ResultPresenter _resultPresenter = null;
        
        void Awake()
        {
            _watchBoardView.BackResultButton.onClick.AddListener(() =>
            {
                _resultPresenter.Reopen();
                Hide();
            });
        }

        void Hide()
        {
            _watchBoardView.gameObject.SetActive(false);
        }

        public void Show()
        {
            Time.timeScale = 0;
            _watchBoardView.gameObject.SetActive(true);
        }
        
    }
}
