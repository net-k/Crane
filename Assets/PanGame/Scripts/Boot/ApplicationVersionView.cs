using Quiz.Framework.Ad.AdMob;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.Presentation.Title
{
    public class ApplicationVersionView : MonoBehaviour
    {
        [SerializeField] private Text _versionText;

        private void Awake()
        {
            _versionText.text = CreateVersionText();
        }

        void Start()
        {
            Destroy(gameObject, 5.0f);
        }

        private string CreateVersionText()
        {
            // return $"Version {GameConstants.ApplicationVersion}";
            string admobTest = AdMobConstants.IsAdMobTestMode ? "|adtest" : "";
            string debugMode = GameConstants.IsDebugMode ? "|debug" : "";
            return $"v{GameConstants.ApplicationVersion}{admobTest}{debugMode}";
        }
    }
}
