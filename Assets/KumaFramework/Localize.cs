// #define LOCALIZE_ENGLISH

using UnityEngine;

namespace Aquarium.Domain.Localize
{
	public class Localize  : MonoBehaviour
	{
		public static string DeviceLanguageEnglish = "English";

//		[SerializeField]
//		private LocalizeDataMaster localizeDataMaster;
	
		public static string GetDeviceLanguage()
		{
			string deviceLanguage = UnityEngine.Application.systemLanguage.ToString();
#if LOCALIZE_ENGLISH
return "English";
#endif
			if (deviceLanguage == "Japanese")
			{
			}
			else if (deviceLanguage == "English")
			{
			}
			else
			{
				return "English";
			}

			return deviceLanguage;
		}

		public string GetText(string key)
		{
			#if false
			// Debug.LogFormat("Localize.GetText key={0}", key);
			int index = localizeDataMaster.LocalizeDataList.FindIndex(n => n.key == key);
			if (index == -1)
			{
				return "";
			}

			switch (GetDeviceLanguage())
			{
				case "Japanese":
					return localizeDataMaster.LocalizeDataList[index].Japanese;
				case "English":
				default:
					return localizeDataMaster.LocalizeDataList[index].English;
				
			}
			#endif
			return key;
		}
	}
}
