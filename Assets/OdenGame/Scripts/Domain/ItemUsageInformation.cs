using System.Collections.Generic;

namespace OdenGame.Domain
{
    public class ItemUsageInformation
    {
        List<ItemUsage> _itemUsages = new List<ItemUsage>();
        
        public void AddItemUsage(int itemId)
        {
            var itemUsage = _itemUsages.Find(x => x.ItemId == itemId);
            if (itemUsage == null)
            {
                _itemUsages.Add(new ItemUsage(itemId, 1));
            }
            else
            {
                itemUsage = new ItemUsage(itemId, itemUsage.UsageCount + 1);
            }
        }


        public bool CanUsage(int itemId)
        {
            if(GameConstants.IsInfiniteItemUsage)
            {
                return true;
            }
            
            var itemUsage = _itemUsages.Find(x => x.ItemId == itemId);
            if (itemUsage == null || itemUsage.UsageCount == 0 )
            {
                return true;
            }

            return false;
        }
    }

    internal class ItemUsage
    {
        private int _itemId;
        private int _usageCount;
        
        public ItemUsage(int itemId, int usageCount)
        {
            _itemId = itemId;
            _usageCount = usageCount;
        }
        
        public int ItemId => _itemId;
        public int UsageCount => _usageCount;
    }
}
