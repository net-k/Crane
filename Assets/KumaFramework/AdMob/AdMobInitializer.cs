using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobInitializer : SingletonMonoBehaviour<AdMobInitializer>
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void Initialize ()
	{
		Debug.Log("AdMobInitializer.Initialize");
		
		MobileAds.SetiOSAppPauseOnBackground(true);
		MobileAds.Initialize(status => {}) ;	
	}
}
