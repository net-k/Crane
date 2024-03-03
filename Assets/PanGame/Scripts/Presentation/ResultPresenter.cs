using System;
using CatBreeding.Scripts.Infrastructure.Framework.Ad.AdMob;
using FruitShop.Presentation;
using ShogiOnline.Presentation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PanGame.Presentation
{
    public class ResultPresenter : MonoBehaviour
    {
        [SerializeField] 
        private AdMobInterstitial _adMobInterstitial = null;

        [SerializeField] 
        private WatchBoardPresenter _watchBoardPresenter = null;
        
        [SerializeField]
        private ResultView _view = null;

        [SerializeField]
        private MainManager _mainManager = null;

        [SerializeField]
        private RewardDialogPresenter _rewardDialogPresenter = null;
        
        enum State
        {
            Default,
            BackTitle,
            WatchBoard
        }

        private State _state;

        private void Awake()
        {
            _state = State.Default;
            
            _view.OkButton.onClick.AddListener(() =>
            {
                _state = State.BackTitle;
               #if false
               if (_adMobInterstitial.IsLoaded())
               {
                    SoundManager.Instance.StopBGM();
                    _adMobInterstitial.OnAdClose += OnInterstitialClosed;
                    _adMobInterstitial.Show();
               }
               else
               #endif
               {
                    OnTitleBackButton();
               }

               Close();
            });
            
            _view.WatchBoardButton.onClick.AddListener(() =>
            {
                _state = State.WatchBoard;
                #if false
                if (_adMobInterstitial.IsLoaded())
                {
                     // SoundManager.Instance.StopBGM();
                     _adMobInterstitial.OnAdClose += OnInterstitialClosed;
                     _adMobInterstitial.Show();
                }
                else
                #endif
                {
                    ShowWatchBoard();
                }
            });
        }
        
        private void ShowWatchBoard()
        {
            _watchBoardPresenter.Show();
            Close();
        }

        
        private void OnInterstitialClosed()
		{
			_adMobInterstitial.OnAdClose -= OnInterstitialClosed;
            
            switch (_state)
            {
                case State.Default:
                    break;
                case State.BackTitle:
                    SceneManager.LoadScene (SceneManager.GetActiveScene().name);
                    break;
                case State.WatchBoard:
                    ShowWatchBoard();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
		}
        
        private void OnTitleBackButton()
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene().name);
            // _rewardDialogPresenter.Open();
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            
            // ベストスコアを保存
            int bestScore = RecordManager.instance.LoadBestScore();
            if (_mainManager.GetScore() > bestScore)
            {
                RecordManager.instance.SaveBestScore(_mainManager.GetScore());
                bestScore = _mainManager.GetScore();
            }

            // スコアを反映
            _view.ScoreText.text = _mainManager.GetScore().ToString();
            _view.BestScoreText.text = bestScore.ToString();

            OpenInternal();
        }
        
        private void OpenInternal()
        {
            gameObject.SetActive(true);
        }
        
        public void Reopen()
        {
            OpenInternal();
        }
    }
}
