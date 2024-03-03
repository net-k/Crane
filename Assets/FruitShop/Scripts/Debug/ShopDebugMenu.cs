using App;
using Aquarium.Presentation.GameScene.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace FruitShop.Debug
{
    public class ShopDebugMenu : MonoBehaviour
    {
        [SerializeField]
        Button _addMoneyButton = null;
        
        [SerializeField]
        Button _minusMoneyButton = null;

        [SerializeField]
        private Button _levelUpButton = null;

        [SerializeField]
        private Button _levelDownButton = null;

        [SerializeField]
        private Button _addDiamondButton = null;
        
        [SerializeField]
        private Button _minusDiamondButton = null;

        [SerializeField]
        private Button _closeButton = null;
        
        [SerializeField]
        private StorePresenter collectionHeaderPresenter = null;
        // Start is called before the first frame update
        void Start()
        {
        
            _addMoneyButton.onClick.AddListener(() =>
            {
                SaveDataManager.Instance.SaveAddMoney(1000);
                collectionHeaderPresenter.UpdateView();
            });
            _minusMoneyButton.onClick.AddListener(() =>
            {
                SaveDataManager.Instance.SaveAddMoney(-1000);
                collectionHeaderPresenter.UpdateView();
            });
            
            _levelUpButton.onClick.AddListener(() =>
            {
                SaveDataManager.Instance.LevelUpShop();
                collectionHeaderPresenter.UpdateView();
            });
            
            _levelDownButton.onClick.AddListener(() =>
            {
                SaveDataManager.Instance.LevelDownShop();
                collectionHeaderPresenter.UpdateView();
            });
            
            _addDiamondButton.onClick.AddListener(() =>
            {
                SaveDataManager.Instance.SaveAddDiamond(1000);
                collectionHeaderPresenter.UpdateView();
            });
            
            _minusDiamondButton.onClick.AddListener(() =>
            {
                SaveDataManager.Instance.SaveAddDiamond(-1000);
                collectionHeaderPresenter.UpdateView();
            });
            
            _closeButton.onClick.AddListener(() =>
            {
                Hide();
            });
        }


        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            
        }
    }
}
