using UnityEngine;
using UnityEngine.UI;

namespace KumaFramework.UI.RewardUI.AdditionalPointDialog
{
    public class AdBannerView : MonoBehaviour
    {
        [SerializeField]
        private Text _adBannerText;

   
        void Awake()
        {
#if false
        if (Localize.GetDeviceLanguage() == Localize.DeviceLanguageEnglish)
        {
            _adBannerText.text = "Ad";
        }
        else
        {
            _adBannerText.text = "広告";
        }
#endif
        }
    }
}
