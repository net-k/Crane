using App;
using Quiz.Framework.Ad.AdMob;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Aquarium.Presentation.GameScene.Shop
{
	public class StorePresenter : MonoBehaviour 
	{
		[SerializeField]
		private StoreView storeView = null;
		private SaveDataManager _saveDataManager;
		[SerializeField]
		private AdMobRewardVideo _adMobRewardVideo = null;
		
		[SerializeField]
		private AdditionalPointPresenter _additionalPointPresenter = null;
		
		
		[Inject]
		void Construct()
		{
			_saveDataManager = SaveDataManager.Instance;
		}
		
		void Start()
		{
			SetEvents();
			Show();
		}

		private void SetEvents()
		{
			storeView.BackButton.onClick.AddListener(OnBackButtonClicked);
			
			storeView.RewardButton.onClick.AddListener(() =>
			{
				_additionalPointPresenter.Show();
			});
			
			_saveDataManager.MoneyUpdated += () =>
			{
				UpdateView();
				// storeView.CoinNum.text = _saveDataManager.GetDiamond().ToString();
			};
			
			_adMobRewardVideo.OnAdRewarded += OnAdRewarded;
			_adMobRewardVideo.OnAdClose += OnAdClose;	
		}
		
		private void OnAdRewarded()
		{
			Debug.Log("FooterMenuPresenter.OnAdRewarded");
			int watchTimes = _saveDataManager.LoadWatchPointAddedVideoTimes();
			if (watchTimes >= GameConstants.WatchPointAddedVideoTimesPerDay)
			{
				Debug.LogError("FooterMenuPresenter.OnAdRewarded 視聴回数制限を超えています");
				return;
			}
			watchTimes++;
		
			_saveDataManager.SaveWatchPointAddedVideoTimes(watchTimes);
			_saveDataManager.SaveAddDiamond(GameConstants.RewardVideoAdditionalDiamond);

			UpdateView();

#if false
			if (_dialogPresenter)
			{
				_dialogPresenter.Close();
				_dialogPresenter = null;
			}
#endif
		}
		private void OnAdClose()
		{
			// 再生
			SoundManager.Instance.Resume();
		}
		
		private void Initialize()
		{
			storeView.Initialize ();
			storeView.CoinNum.text = _saveDataManager.GetDiamond().ToString();
		}

		private void OnBackButtonClicked()
		{
			this.gameObject.SetActive (false);
			
		}

		public void Show()
		{
			this.gameObject.SetActive(true);
			Initialize();
		}

		public void UpdateView()
		{
			storeView.UpdateHeaderView();
		}
	}
}
