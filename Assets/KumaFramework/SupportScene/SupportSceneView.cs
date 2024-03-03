using UnityEngine;
using UnityEngine.UI;

namespace Quiz.Framework.SupportScene
{
    public class SupportSceneView : MonoBehaviour
    {
        [SerializeField] private Button privacyButton = null;
        [SerializeField] private Button licenseButton = null;
        [SerializeField] private Button supportButton = null;
        [SerializeField] private Button termsButton = null;

        [SerializeField] private Button backButton = null;

        public Button PrivacyButton => privacyButton;

        public Button LicenseButton => licenseButton;

        public Button SupportButton => supportButton;
    
        public Button TermsButton => termsButton;
        
        public Button BackButton => backButton;
        
        private void Reset()
        {
                    
            licenseButton = GameObject.Find("LicenseButton").GetComponent<UnityEngine.UI.Button>();
            supportButton = GameObject.Find("SupportButton").GetComponent<UnityEngine.UI.Button>();
            privacyButton = GameObject.Find("PrivacyButton").GetComponent<UnityEngine.UI.Button>();
            termsButton = GameObject.Find("TermsOfServiceButton").GetComponent<UnityEngine.UI.Button>();
            backButton = GameObject.Find("BackButton").GetComponent<UnityEngine.UI.Button>();
        }

    }
}
