using App;
using Aquarium.Data;
using Zenject;

namespace OdenGame.Repository
{
	public class ShopModel
	{
		[Inject] private ShopData shopData = null;
		private SaveDataManager _saveDataManager = null;

		ShopModel()
		{
			_saveDataManager = SaveDataManager.Instance;
		}
	
		public ShopParam? GetProduct(int productId)
		{
			foreach (var param in shopData.Data)
			{
				if (param.productId == productId)
				{
					return param;
				}
			}

			return null;
		}

		public bool IsPurchased(int productId)
		{
			var purchaseProductList = _saveDataManager.LoadPurchaseProduct();
			int index = purchaseProductList.FindIndex(n => n == productId);

			if (index == -1)
			{
				return false;
			}
		
			return true;
		}

		public int GetCreatureTotalCount()
		{
			return shopData.ProductNum();
		}

		public int GetPurchasedCreatureCount()
		{
			int count = 0;
			foreach (var param in shopData.Data)
			{
				if (IsPurchased(param.productId))
				{
					count++;
				}
			}
			return count;
		}
	}
}
