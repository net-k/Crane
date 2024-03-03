using Aquarium.Domain.Localize;
using UnityEngine;
using UnityEngine.UI;

namespace OdenGame.Store.ProductContent
{
	public class ProductContentView : MonoBehaviour
	{
		public enum CurrencyImageIndex : int
		{
			Diamond=0,
			Heart=1
		}
		
		private Localize _localize;
	
		[SerializeField]
		Text buttonText = null;
		public Text ButtonText
		{
			get { return buttonText; }
		}

		[SerializeField]
		Text nameText = null;
		public Text NameText
		{
			get { return nameText; }
		}

		[SerializeField]
		Text priceText = null; 
		public Text PriceText
		{
			get { return priceText; }
		}

		[SerializeField]
		Text detailText = null;
		public Text DetailText
		{
			get { return detailText; }
		}

		[SerializeField]
		Button purchaseButton = null;
		public Button PurchaseButton
		{
			get { return purchaseButton; }
		}

		[SerializeField] public Image _iconImage = null;
		public Image IconImage => _iconImage;

		[SerializeField] private GameObject price = null;
		[SerializeField] private Text changeButtonText = null;
		[SerializeField] private Image[] _priceTypeImage = null;

		[SerializeField] private Text _itemCountText;

		public Text ItemCountText => _itemCountText;

		public void ShowCurrencyTypeImage(CurrencyImageIndex index)
		{
			foreach (var image in _priceTypeImage)
			{
				image.gameObject.SetActive(false);
			}
			_priceTypeImage[(int)index].gameObject.SetActive(true);	
		}

		public void Initialize( Localize localize)
		{
			_localize = localize;
		}
	}
}
