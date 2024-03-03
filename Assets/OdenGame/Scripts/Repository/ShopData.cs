using System;
using UnityEngine;
using Zenject;

namespace Aquarium.Data
{
    public enum Category : int
    {
        Item,
        BGM,
        Background,
        Diamond,
    }

    public enum CurrencyType
    {
        Diamond,
        Heart
    }
    
    [Serializable]
    public struct ShopParam
    {
        public bool enable;
        public int sortOrder;
        public int productId;
        public Category categoryId;
        public string name;
        public int price;
        public string detail;
        public string prefabFileName;
        public CurrencyType currencyType;
        public bool sale; // 販売するいか否か
    }


    [CreateAssetMenu(menuName = "OdenGame/CreateShopData", fileName = "ShopData")]
    public class ShopData : ScriptableObjectInstaller
    {
        [SerializeField] private ShopParam[] shopData;
        public ShopParam[] Data
        {
            get { return shopData; }
        }

        public int ProductNum()
        {
            return shopData.Length;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}