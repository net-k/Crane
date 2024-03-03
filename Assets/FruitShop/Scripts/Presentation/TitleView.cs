using UnityEngine;
using UnityEngine.UI;

namespace FruitShop.Scripts
{
	public class TitleView : MonoBehaviour{
		[SerializeField]
		private Button startButton;
		public Button StartButton
		{
			get { return startButton; }
		}

		[SerializeField]
		private Button _collectionButton;

		public Button CollectionButton => _collectionButton;

		[SerializeField]
		private Button shopButton;
		public Button ShopButton
		{
			get { return shopButton; }
		}

	}
}
