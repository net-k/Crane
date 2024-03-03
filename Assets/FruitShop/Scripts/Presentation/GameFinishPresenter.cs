using System;
using CatBreeding.Scripts.Infrastructure.Framework.Ad.AdMob;
using UnityEngine;

namespace PanGame.Presentation
{
    public class GameFinishPresenter : MonoBehaviour
    {
        [SerializeField] 
        private AdMobInterstitial _adMobInterstitial = null;

        [SerializeField]
        private GameFinishView _view = null;
        
        [SerializeField]
        private ResultPresenter _resultPresenter;
        
        enum State
        {
            Default,
            BackTitle,
        }

        private State _state;

        private void Awake()
        {
            _state = State.Default;
            
            _view.OkButton.onClick.AddListener(() =>
            {
                _state = State.BackTitle;
               
               if (_adMobInterstitial.IsLoaded())
               {
                    SoundManager.Instance.StopBGM();
                    _adMobInterstitial.OnAdClose += OnInterstitialClosed;
                    _adMobInterstitial.Show();
               }
               else
               {
                    OnTitleBackButton();
               }

               Close();
            });
            
        }
        
        
        private void OnInterstitialClosed()
		{
			_adMobInterstitial.OnAdClose -= OnInterstitialClosed;
            
            switch (_state)
            {
                case State.Default:
                    break;
                case State.BackTitle:
                    // SceneManager.LoadScene (SceneManager.GetActiveScene().name);
                    _resultPresenter.Open();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
		}
        
        private void OnTitleBackButton()
        {
            _resultPresenter.Open();
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        public void Reopen()
        {
            Open();
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }
    }
}
