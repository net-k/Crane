using FruitShop.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Quiz.Presentation
{

	public class TitlePresenter : MonoBehaviour
	{
		[SerializeField]
		private TitleView _view = null;

		[SerializeField]
		private MenuDialog _menuDialog;
		
		public void Awake()
		{
			SetEvents();
			Initialize();
		}

		// 初期化
		public void Initialize()
		{
		}

		// Viewのイベントの設定を行う
		private void SetEvents()
		{
			_view.StartButton.onClick.AddListener(OnStartButtonClicked);
			_view.ShopButton.onClick.AddListener(OnShopButtonClicked);
			_view.CollectionButton.onClick.AddListener(OnCollectionButtonClicked);
		}

		private void OnShopButtonClicked()
		{
			SceneManager.LoadScene("Shop");
		}

		private void OnStartButtonClicked()
		{
			// SoundManager.Instance.PlaySE("decision5");
			Next();
		}

		private void OnCollectionButtonClicked()
		{
			SceneManager.LoadScene("Collection");
		}

		private void Next()
		{
			// SceneManager.LoadScene("MainScene");
			_menuDialog.Show();
		}
	}
}
