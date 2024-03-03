using FruitShop.Presentation;
using FruitShop.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

namespace FruitShop
{
    public class CollectionPresenter : MonoBehaviour
    {
        [SerializeField]
        private CollectionView _view = null;

        [SerializeField]
        private LevelupDialogPresenter _levelupDialogPresenter = null;
        
        private PlayerData _playerData;
        private ShopUseCase _shopUseCase;
        
        [FormerlySerializedAs("_shopHeaderPresenter")] [SerializeField]
        private CollectionHeaderPresenter collectionHeaderPresenter = null;
        
        
        [Inject]
        void Construct(PlayerData playerData, ShopUseCase shopUseCase)
        {
            _playerData = playerData;
            _shopUseCase = shopUseCase;
        }
        
        void Awake()
        {
             if( _playerData.IsLevelMax())
             {
                 _view.LevelUpButton.interactable = false;
                 _view.HideNextLevelPrice();
             }
            
            _view.BackButton.onClick.AddListener(() =>
            {
                Close();
                SceneManager.LoadScene("Title");
            });
            
            _view.LevelUpButton.onClick.AddListener(() =>
            {
                bool hasLevelUp = LevelUp();

                if (hasLevelUp)
                {
                    _levelupDialogPresenter.Open();

                    if (_playerData.IsLevelMax())
                    {
                        _view.LevelUpButton.interactable = false;
                        _view.HideNextLevelPrice();
                    }
                }
            });
        }

        private void Start()
        {
            ShowFruits();
            
            // 次のレベルの値段を取得
            UpdateLevelupView();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateLevelupView()
        {
            int nextLevelPrice = 0;
            int nextLevel = _playerData.GetShopLevel() + 1;
            bool ret = _shopUseCase.GetShopLevelPrice(nextLevel, out nextLevelPrice);
            if (!ret)
            {
                _view.HideNextLevelPrice();
            }
            else
            {
                _view.NextLevelPriceText.text = nextLevelPrice.ToString();
            }
        }

        private bool LevelUp()
        {
            // レベルアップ処理
            // 次のレベルを取得
            int nextLevel = _playerData.GetShopLevel() + 1;
            // 次のレベルの値段を取得
            int nextLevelPrice = 0;
            bool ret = _shopUseCase.GetShopLevelPrice(nextLevel, out nextLevelPrice);
            if (!ret)
            {
                return false;
            }
            
            // お金が足りているか確認
            if (_playerData.GetMoney() < nextLevelPrice)
            {
                return false;
            }

            _playerData.LevelUpShop();
            _playerData.SaveAddMoney(-nextLevelPrice);

            UpdateView(nextLevelPrice);

            return true;
        }

        private void UpdateView(int nextLevelPrice)
        {
            // 表示の更新
            collectionHeaderPresenter.UpdateView();
            // NEXT LEVELの表示を更新
            _view.NextLevelPriceText.text = nextLevelPrice.ToString();
            
            // フルーツの表示を更新
            ShowFruits();
            
            UpdateLevelupView();
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        private void ShowFruits()
        {
            for (int i = 0; i < GameConstants.MaxShopLevel; i++)
            {
                int level = i + 1;
                _view.VisibleFruits(level, false);
            };
            
            // レベルに応じて、果物の表示を変える
            for (int i = 0; i < _playerData.GetShopLevel(); i++)
            {
                int level = i + 1;
                _view.VisibleFruits(level, true);
            }
        }
    }
}
