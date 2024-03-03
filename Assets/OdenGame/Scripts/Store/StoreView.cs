using System.Collections.Generic;
using App;
using Aquarium.Data;
using Aquarium.Domain.Localize;
using Aquarium.Presentation.GameScene.Shop.ProductContent;
using OdenGame.Domain;
using OdenGame.Repository;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Aquarium.Presentation.GameScene.Shop
{
	public class StoreView : MonoBehaviour {

		[SerializeField]
		private Text _coinNum = null;

		public Text CoinNum => _coinNum;

		[SerializeField] private Localize _localize = null;
	
		[SerializeField]
		private Button backButton = null;
		public Button BackButton
		{
			get { return backButton; }
		}

		[SerializeField]
		private Button _rewardButton = null;

		public Button RewardButton => _rewardButton;

		private SaveDataManager _saveDataManager = null;
		private ShopData _shopData = null;
		private ShopModel _shopModel = null;
		private Player _player;
		
		/// <summary>
		/// スクロールビューの Content
		/// </summary>
		[SerializeField]
		private GameObject Content = null;

		private List<ProductContentPresenter> products = new List<ProductContentPresenter>();

		[Inject]
		void Construct(ShopData shopData, ShopModel shopModel, Player player)
		{
			_shopData = shopData;
			_saveDataManager = SaveDataManager.Instance;
			_shopModel = shopModel;
			_player = player;
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			var parent = Content;
			foreach(Transform child in parent.gameObject.transform){
				Destroy(child.gameObject);
			}

			products.Clear();
			foreach (var productData in _shopData.Data)
			{
				if( !productData.enable ) continue;
				CreateProduct(productData.productId);
			}

			UpdateHeaderView();
		}

		/// <summary>
		/// Creates the button.
		/// </summary>
		/// <param name="info">Info.</param>
		void CreateProduct(int productId){
			var parent = Content;
        
			var listButtonPrefab = (GameObject)Resources.Load ("Prefabs/UI/Shop/ProductContent");
			if (listButtonPrefab == null) Debug.LogError("listButtonPrefab is null");
			
			var listContent = SetChild (parent, listButtonPrefab, "Product");

			var productContentPresenter = listContent.GetComponent<ProductContentPresenter>();
			productContentPresenter.Initialize(productId,_shopModel, _saveDataManager,  _localize, _player);
			productContentPresenter.Show();
			
			productContentPresenter.OnPurchased += OnPurchased;
			productContentPresenter.OnChanged += OnChanged;
        
			products.Add(productContentPresenter);
		}

		public void UpdateHeaderView()
		{
			if (_saveDataManager == null)
			{
				_saveDataManager = SaveDataManager.Instance;
			}
			_coinNum.text = _saveDataManager.GetDiamond().ToString();
		}
	
		void OnPurchased()
		{
			UpdateHeaderView();
		}

		void OnChanged()
		{
			
			foreach (var product in products)
			{
				product.UpdateView();
			}
		}
		
		// SetChild関数の中身
		/// <summary>
		/// 子オブジェクトセット処理
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="child"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public Transform SetChild(GameObject parent,GameObject child,string name = null)
		{
			// プレハブからインスタンスを生成
			GameObject obj = Instantiate(child);
			// 作成したオブジェクトを子として登録
			obj.transform.SetParent(parent.transform, false);

			return obj.transform;
		}
	}
}
