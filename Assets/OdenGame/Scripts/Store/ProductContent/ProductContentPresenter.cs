using System;
using App;
using Aquarium.Data;
using I2.Loc;
using OdenGame.Domain;
using OdenGame.Repository;
using OdenGame.Store.ProductContent;
using UnityEngine;
using Localize = Aquarium.Domain.Localize.Localize;

namespace Aquarium.Presentation.GameScene.Shop.ProductContent
{
    public class ProductContentPresenter : MonoBehaviour
    {
        private Localize _localize;
        private SaveDataManager _saveDataManager;

        private ShopModel _shopModel;
        private int productId;
        private ProductContentView view;
        public event Action OnPurchased;
        public event Action OnChanged;

        public Player _player;

        public void Initialize(int productId, ShopModel shopModel, SaveDataManager saveDataManager, Localize localize, Player player)
        {
            view = GetComponent<ProductContentView>();
            view.PurchaseButton.onClick.AddListener(OnPurchaseButton);
            view.PurchaseButton.interactable = true;
            _player = player;
            _saveDataManager = saveDataManager;
            _shopModel = shopModel;
            _localize = localize;

            view.Initialize(localize);
            if (_shopModel == null)
            {
                Debug.LogError("ShopModel is null");
                Debug.Break();
            }

            this.productId = productId;
            UpdateView();
        }

        /// <summary>
        ///     購入ボタンを押した
        /// </summary>
        private void OnPurchaseButton()
        {
            var data = _shopModel.GetProduct(productId);
            OnFirstPurchased(data);
        }

        /// <summary>
        ///     初回の購入
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void OnFirstPurchased(ShopParam? data)
        {
            bool purchased = Purchase(data.Value.price, data.Value.categoryId, data.Value.currencyType, data.Value.prefabFileName);
            if (purchased)
            {
                GivePurchaseItem(data);
                UpdateView();
            }
        }

        private static void GivePurchaseItem(ShopParam? data)
        {
            switch (data.Value.categoryId)
            {
                case Category.Item:
                {
                    // Purchase(data.Value.price, data.Value.categoryId, data.Value.currencyType, data.Value.prefabFileName);
                    int itemId = data.Value.productId;
                    SaveDataManager.Instance.AddItem(itemId, 1);
                }
                    break;
                case Category.BGM:
                case Category.Background:
                    break;
                case Category.Diamond:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                    break;
            }
        }


        private bool Purchase(int price, Category categoryId, CurrencyType currencyType, string prefabFileName)
        {
            if (!IsPurchasable(productId, price, currencyType))
            {
                return false;
            }

            if (currencyType == CurrencyType.Diamond)
            {
                _saveDataManager.UseDiamond(price);
            }
            else if(currencyType == CurrencyType.Heart)
            {
                int additionalMoney = Int32.Parse(prefabFileName);
                _saveDataManager.SaveAddMoney(additionalMoney);
            }

            if (OnPurchased != null)
            {
                OnPurchased();
            }

            return true;
        }

        /// <summary>
        ///     View を最新の情報で更新する
        /// </summary>
        public void UpdateView()
        {
            var data = _shopModel.GetProduct(productId);
            if (data == null)
            {
                Debug.LogErrorFormat("Error productId= {0} is not found", productId.ToString());
                return;
            }

            var value = data.Value;

            switch (data.Value.categoryId)
            {
                case Category.Item:
                    string name = LocalizationManager.GetTranslation(value.name);
                    view.NameText.text = name;
                    string detail = LocalizationManager.GetTranslation(value.detail);
                    view.DetailText.text = detail;
                    string filePath = $"Textures/Items/{value.prefabFileName}"; 
                    view.IconImage.sprite = Resources.Load<Sprite>(filePath);

                    if (!value.sale)
                    {
                        view.ButtonText.text = LocalizationManager.GetTranslation("Sold Out");
                        view.PurchaseButton.interactable = false;
                    } 
                    break;
                case Category.BGM:
                case Category.Background:
                    view.NameText.text = _localize.GetText(value.name);
                    view.DetailText.gameObject.SetActive(true);
                    view.DetailText.text = GetDetailByCategory(data.Value.categoryId);
                    break;
                case Category.Diamond:
                    view.NameText.text = $"{_localize.GetText(value.name)} x {value.prefabFileName}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            view.PriceText.text = value.price.ToString();
            
            int itemCount =  _player.GetItemCount(productId);
            view.ItemCountText.text = itemCount.ToString();
            
            if (data.Value.categoryId != Category.Item)
            {
            }

            view.ShowCurrencyTypeImage(GetCurrencyImageIndex(data.Value.currencyType));

            UpdateButtonText(_shopModel.IsPurchased(productId));
        }

        private ProductContentView.CurrencyImageIndex GetCurrencyImageIndex(CurrencyType currencyType)
        {
            var index = ProductContentView.CurrencyImageIndex.Diamond;
            switch (currencyType)
            {
                case CurrencyType.Diamond:
                    index = ProductContentView.CurrencyImageIndex.Diamond;
                    break;
                case CurrencyType.Heart:
                    index = ProductContentView.CurrencyImageIndex.Heart;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return index;
        }

        private string GetDetailByCategory(Category categoryId)
        {
            switch (categoryId)
            {
                case Category.BGM:
                    return "BGM";
                case Category.Background:
                    return "Background";
            }

            return "";
        }

        /// <summary>
        /// 購入済みであるか否かで、ボタンの表示を変える
        /// </summary>
        private void UpdateButtonText(bool isPurchased)
        {
            var data = _shopModel.GetProduct(productId);
            var isSelected = false;
            var isSelectable = false;
            switch (data.Value.categoryId)
            {
                case Category.Item:
                    isSelectable = true;
                    break;
                case Category.BGM:
                    // 選択中か判定する
                    if (data.Value.prefabFileName == _saveDataManager.GetCurrentBGM())
                    {
                        isSelected = true;
                    }
                    isSelectable = true;
                    break;
                case Category.Background:
                    // 選択中か判定する
                    if (data.Value.prefabFileName == _saveDataManager.LoadBackgroundIndex().ToString())
                    {
                        isSelected = true;
                    }
                    isSelectable = true;
                    break;
                case Category.Diamond:
                    isSelectable = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            UpdateButtonContent();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateButtonContent()
        {
        }

        /// <summary>
        /// 購入できるか？
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        private bool IsPurchasable(int productId, int price, CurrencyType currencyType)
        {
            if (_shopModel.IsPurchased(productId))
            {
                return false;
            }

            switch (currencyType)
            {
                case CurrencyType.Diamond:
                    if (_saveDataManager.GetDiamond() >= price)
                    {
                        return true;
                    }
                    break;
                case CurrencyType.Heart:
                    if (_saveDataManager.GetMoney() >= price)
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}