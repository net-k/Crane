using System;
using Quiz.Framework.SupportScene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KumaFramework.SupportScene
{
    public class SupportScenePresenter : MonoBehaviour
    {
        private SupportSceneView _view;
    
        [SerializeField]
        private string licenseURL = "http://net-k.net/kumamon_flowercards/license";
    
        [SerializeField]
        private string supportURL = "https://docs.google.com/forms/d/e/1FAIpQLSf8bYFrrT0W_S2DagYW485kMZH3u7E7mf6czErhvmqFXXphOg/viewform?usp=sf_link";
    
        [SerializeField]
        private string privacyPolicyURL = "http://net-k.net/privacy";

        [SerializeField]
        private string termsOfServiceURL = "http://net-k.net/fruit_shop/terms";
        
        void Awake()
        {
            // 同じ GameObject に Attach している前提
            _view = GetComponent<SupportSceneView>();
        
            _view.LicenseButton.onClick.AddListener(() =>
                {
                    Application.OpenURL(licenseURL);   
                }
            );
        
            _view.SupportButton.onClick.AddListener(() =>
                {
                    Application.OpenURL(supportURL);   
                
                }
            );
        
            _view.PrivacyButton.onClick.AddListener(() =>
                {
                    Application.OpenURL(privacyPolicyURL);   
                
                }
            );
            
            _view.TermsButton.onClick.AddListener(() =>
            {
                Application.OpenURL(termsOfServiceURL);
            });
        
            _view.BackButton.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("Title");
                }
            );
        }
    
    
    }
}
