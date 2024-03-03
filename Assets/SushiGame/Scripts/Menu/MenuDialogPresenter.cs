using System;
using CatBreeding.Scripts.Infrastructure.Framework.Ad.AdMob;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PanGame.Presentation.Menu
{
    public class MenuDialogPresenter : MonoBehaviour
    {
        [SerializeField]
        private MenuDialogView _menuDialogView = null;
        
        [SerializeField]
        private AdMobInterstitial _adMobInterstitial = null;
        
        private void Awake()
        {
            _menuDialogView.RetryButton.onClick.AddListener(() =>
            {
                if (_adMobInterstitial.IsLoaded())
                {
                    SoundManager.Instance.StopBGM();
                    _adMobInterstitial.OnAdClose += OnInterstitialClosed;
                    _adMobInterstitial.Show();
                }
                else
                {
                    Retry();
                }                
            });
            
            _menuDialogView.BackToGameButton.onClick.AddListener(() =>
            {
                Hide();
            });
        }

        private void Hide()
        {
            _menuDialogView.gameObject.SetActive(false);
        }

        private void OnInterstitialClosed()
        {
            _adMobInterstitial.OnAdClose -= OnInterstitialClosed;

            Retry();
        }
        
        private void Retry()
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene().name);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public bool IsShow()
        {
            return gameObject.activeSelf;
        }
    }
}
