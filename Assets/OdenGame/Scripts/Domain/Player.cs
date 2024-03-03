using App;

namespace OdenGame.Domain
{
    public class Player
    {
        private SaveDataManager _saveDataManager;
        private ItemUsageInformation _itemUsageInformation;
        
        Player(ItemUsageInformation itemUsageInformation)
        {
            _saveDataManager = SaveDataManager.Instance;
            _itemUsageInformation = itemUsageInformation;
        }

        
        public void ConsumeItem(int itemId)
        {
            int consumeCount = 1;
            _saveDataManager.ConsumeItem( itemId, consumeCount);
            _itemUsageInformation.AddItemUsage(itemId);
        }

        public bool HasItem(int itemId)
        {
            return _saveDataManager.HasItem(itemId);
        }

        public void AddItem(int itemId, int itemCount)
        {
            _saveDataManager.AddItem(itemId, itemCount);
        }

        public int GetItemCount(int itemId)
        {
            return _saveDataManager.GetItemCount(itemId);
        }
    }
}
