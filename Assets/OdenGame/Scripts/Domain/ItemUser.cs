using System;
using OdenGame.Domain;
using UnityEngine;
using Zenject;

namespace OdenGame
{
    public class ItemUser : MonoBehaviour
    {
        [SerializeField]
        private MainManager _mainManager = null;

        private Player _player;
        private ItemUsageInformation _itemUsageInformation;

        [Inject]
        void Construct(Player player, ItemUsageInformation itemUsageInformation)
        {
            _player = player;
            _itemUsageInformation = itemUsageInformation;
        }
        
        public bool UseItem(int itemId)
        {
            if (!_player.HasItem(itemId))
            {
                UnityEngine.Debug.Log($"ItemUser.UseItem {itemId} 持っていないアイテムを使用しようとしました");
                return false;
            }
            
            Item item = new Item();
            switch (item.GetItemType(itemId))
            {
                case Item.ItemType.Karashi:
                     UseChangeNextItem();
                     break;
                case Item.ItemType.Shichimi:
                    UseDeleteRandomGameObject();
                    break;
                case Item.ItemType.Miso:
                    break;
                case Item.ItemType.Yuzu:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
           
            _player.ConsumeItem(itemId);
            return true;
        }

        private void UseDeleteRandomGameObject()
        {
            _mainManager.DeleteRandomGameObject();
        }

        private void UseChangeNextItem()
        {
            _mainManager.CreateNextBreadWithItem();
        }

        public bool CanUseItem(int itemId)
        {
            if (!_itemUsageInformation.CanUsage(itemId))
            {
                return false;
            }
            
            Item item = new Item();
            switch (item.GetItemType(itemId))
            {
                case Item.ItemType.Shichimi:
                    if( !_mainManager.ExistsGameObject() )
                    {
                        return false;
                    }
                    break;
                case Item.ItemType.Karashi:
                case Item.ItemType.Miso:
                case Item.ItemType.Yuzu:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
    }
}
