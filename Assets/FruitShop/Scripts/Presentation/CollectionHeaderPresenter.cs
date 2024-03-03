using App;
using UnityEngine;

namespace FruitShop.Presentation
{
    public class CollectionHeaderPresenter : MonoBehaviour
    {
        [SerializeField]
        CollectionHeaderView _view = null;

        private void Awake()
        {
            UpdateView();
            
        }

        public void SetLevel(int level)
        {
            _view.SetLevelText(level);
        }

        public void SetCoin(int coin)
        {
            _view.SetCoinText(coin);
        }

        public void UpdateView()
        {
            int coin = SaveDataManager.Instance.GetMoney();
            SetCoin(coin);
            
            int level = SaveDataManager.Instance.GetShopLevel();
        #if false
            if (level >= GameConstants.MaxShopLevel)
            {
                _view.SetLevelMaxText();
            }
            else
            {
                SetLevel(level);
            }
        #endif
            SetLevel(level);
        }
    }
}
