using UnityEngine;

namespace OdenGame.Domain
{
    public class Item
    {
        public enum ItemType : int
        {
            Karashi = 1,
            Shichimi = 2,
            Miso = 3,
            Yuzu = 4
        }

        public ItemType GetItemType(int itemId)
        {
            return (ItemType)itemId;
        }
    }
}
