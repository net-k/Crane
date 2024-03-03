using System;
using System.Collections.Generic;
using Aquarium.Data;
using UnityEngine;

namespace App
{
    public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
    {
        private readonly int SaveDataVersion = 1;
        private readonly string RecordType_SaveDataVersion = "SaveDataVersion";
        private readonly string RecordType_StageScore = "StageScoreKey";
        private readonly string RecordType_StageUnlock = "StageUnlockKey"; // ステージがアンロックされているか？
        private readonly string RecordType_WatchPointAddedVideoTimes = "WatchPointAddedVideoTimes"; // ポイント追加の動画を見た回数
        private readonly string RecordType_Life = "Life";
        private readonly string RecordType_LifeLastRecoverTime = "LifeLastRecoverTime";
        private readonly string RecordType_Money = "Money";
        private readonly string RecordType_Diamond = "Diamond";
        private readonly string RecordType_Records = "Records";
        private readonly string RecordType_Level = "Level";   
        private readonly string RecordType_ItemList = "ItemList";
        private readonly string RecordType_Items = "OdenItems";
        
        private readonly string RecordType_CurrentBGM = "CurrentBGM";
        private readonly string RecordType_CurrentBackground = "CurrentBackground";
        private readonly string RecordType_SatietyPoint = "SatietyPoint";
        
        public event Action MoneyUpdated;

        [System.Serializable]
        public class Item
        {
            public int id;
            public int count;
        }
       /* 
        [System.Serializable]
        public class ItemListContainer
        {
            public List<Item> items = new List<Item>();
        }

        private ItemListContainer _itemListContainer = new ItemListContainer();
        */
        struct Record
        {
            public float timeUnitSec;
            private int inputCharacterCount;

            Record(float t, int c)
            {
                timeUnitSec = t;
                inputCharacterCount = c;
            }

            // public GetInputCharacterPerTimeMin;
        }
        
        public void Initialize()
        {
            if (!ES3.KeyExists(RecordType_SaveDataVersion))
            {
                CreateInitialData_SaveDataVersion1();
            }
        }

        private void CreateInitialData_SaveDataVersion1()
        {
            SaveLifeLastRecoverTime(DateTime.Now); 
            ES3.Save(RecordType_SaveDataVersion, 1);

            SaveShopLevel(GameConstants.INITIAL_SHOP_LEVEL);
            SaveMoney(GameConstants.INITIAL_MONEY);
        }

        public void SaveScore(string category, int stageNo, int score)
        {
            var key = RecordType_StageScore + $"{stageNo.ToString()}-{category.ToString()}";
            
            ES3.Save<int>(key, score);
        }

        public int LoadScore(string category, int stageNo)
        {
            var key = RecordType_StageScore + $"{stageNo}-{category}";
            if (!ES3.KeyExists(key)) return 0;

            return ES3.Load<int>(key);
        }
#if false
        public void SaveUnlock(string category, int stageNo)
        {
            var key = RecordType_StageUnlock + $"{stageNo}-{category}";
            ES3.Save<int>(key, 1);
        }


        public bool LoadUnlock(string category, int stageNo)
        {
            var key = RecordType_StageUnlock + $"{stageNo}-{category}";
            if (!ES3.KeyExists(key)) return false;
            var value = ES3.Load<int>(key);
            return Convert.ToBoolean(value);
        }
#endif
        public int LoadBestScore(string category, int stage)
        {
            return LoadScore(category, stage);
        }

        public void SaveBestScore(string category, int stage, int playerCorrectNum)
        {
            SaveScore(category, stage, playerCorrectNum);
        }

        /// <summary>
        ///     動画視聴回数を保存する
        /// </summary>
        /// <param name="times"></param>
        public void SaveWatchPointAddedVideoTimes(int times)
        {
            ES3.Save<int>(RecordType_WatchPointAddedVideoTimes, times);
        }

        /// <summary>
        ///     動画視聴回数をロードする
        /// </summary>
        /// <returns></returns>
        public int LoadWatchPointAddedVideoTimes()
        {
            if (!ES3.KeyExists(RecordType_WatchPointAddedVideoTimes)) return 0;
            return ES3.Load<int>(RecordType_WatchPointAddedVideoTimes);
        }

         
        public DateTime LoadLifeLastRecoverTime()
        {
            if (!ES3.KeyExists(RecordType_LifeLastRecoverTime))
            {
                Debug.LogError("key (life last recover time) not found.");
                SaveLifeLastRecoverTime(DateTime.Now);
                return DateTime.Now;
            }
            var last = ES3.Load<string>(RecordType_LifeLastRecoverTime);
            
            return DateTime.Parse(last);
        }
        
        public void SaveLifeLastRecoverTime(DateTime last)
        {
            var lastString = last.ToString();
            ES3.Save<string>(RecordType_LifeLastRecoverTime, lastString );
        }

        void SaveRecord(Record record)
        {
            var records = new List<Record>();
            if (!ES3.KeyExists(RecordType_Records))
            {
                LoadRecord(out records);
            }

            ES3.Save< List<Record> >(RecordType_Records, records );
        }

        void LoadRecord( out List<Record> records)
        {
            records = new List<Record>();
            ES3.Load< List<Record> >(RecordType_Records, records );
        }

        public void SaveDiamond(int diamond)
        {
            if (!ES3.KeyExists(RecordType_Diamond))
            {
                ES3.Save<int>(RecordType_Diamond, GameConstants.INITIAL_DIAMOND);
            }
            else
            {
                ES3.Save<int>(RecordType_Diamond, diamond);
            }
        }
        
        public int SaveAddDiamond(int money)
        {
            int currentMoney = GetDiamond();
            int nextMoney = currentMoney + money;
            SaveDiamond(currentMoney + money);

            return nextMoney;
        }

        public int GetDiamond()
        {
            if (!ES3.KeyExists(RecordType_Diamond))
            {
                SaveDiamond(GameConstants.INITIAL_DIAMOND);
            }
            return ES3.Load<int>(RecordType_Diamond);
        }        
        
        public void SaveMoney(int money)
        {
            ES3.Save<int>(RecordType_Money, money);
            if (MoneyUpdated != null)
            {
                MoneyUpdated();
            }
        }

        public int SaveAddMoney(int money)
        {
            int currentMoney = GetMoney();
            int nextMoney = currentMoney + money;
            SaveMoney(currentMoney + money);

            return nextMoney;
        }

        public int GetMoney()
        {
            if (!ES3.KeyExists(RecordType_Money))
            {
                SaveMoney(GameConstants.INITIAL_MONEY);
            }
            return ES3.Load<int>(RecordType_Money);
        }
        
        public int GetShopLevel()
        {
            if (!ES3.KeyExists(RecordType_Level))
            {
                SaveShopLevel(GameConstants.INITIAL_SHOP_LEVEL);
            }
            return ES3.Load<int>(RecordType_Level);
        }

        private int SaveShopLevel(int level)
        {
            ES3.Save<int>(RecordType_Level, level);
            return level;
        }
        
        public bool LevelUpShop()
        {
            int nextShopLevel = GetShopLevel() + 1;
            if( GameConstants.MaxShopLevel < nextShopLevel )
            {
                return false;
            }
            
            ES3.Save<int>(RecordType_Level, nextShopLevel );
            return true;
        }

        public void LevelDownShop()
        {
            int prevShopLevel = GetShopLevel() - 1;
            if( prevShopLevel < 0 )
            {
                prevShopLevel = 0;
            }
                        
            ES3.Save<int>(RecordType_Level, prevShopLevel );
        }

        public bool HasItem(int itemId)
        {
            var items = new List<Item>();
            if (!ES3.KeyExists(RecordType_Items))
            {
                return false;
            }
            LoadItems(out items);
            

            var item = items.Find(i => i.id == itemId);
            if (item == null)
            {
                return false;
            }

            return true;
        }

        public bool ConsumeItem(int itemId, int consumeItemCount)
        {
            var items = new List<Item>();
            if (!ES3.KeyExists(RecordType_Items))
            {
                return false;
            }
            LoadItems(out items);

            var item = items.Find(i => i.id == itemId);
            if (item == null)
            {
                Debug.LogError($"item not found. id={itemId}");
                return false;
            }

            int itemRemainCount = item.count - consumeItemCount;
            if (itemRemainCount < 0)
            {
                Debug.LogError($"item count is negative. id={itemId}");
                return false;
            }

            item.count -= consumeItemCount;
            
            ES3.Save(RecordType_Items, items );
            return true;
        }

        void LoadItems( out List<Item> items)
        {
            items = new List<Item>();
            if (!ES3.KeyExists(RecordType_Items))
            {
            }

            items = ES3.Load(RecordType_Items, items );
        }

        public void AddItem(int itemId, int itemCount)
        {
            var items = new List<Item>();
            if (ES3.KeyExists(RecordType_Items))
            {
                LoadItems(out items);
            }
            else
            {
                ES3.Save(RecordType_Items, items );
            }

            var item = items.Find(i => i.id == itemId);
            if (item == null)
            {
                item = new Item();
                item.id = itemId;
                item.count = 1;
                items.Add(item);
            }
            else
            {
                item.count += itemCount;
            }
            
            ES3.Save(RecordType_Items, items );
        }

        public int GetItemCount(int itemId)
        {
            var items = new List<Item>();
            if (!ES3.KeyExists(RecordType_Items))
            {
                return 0;
            }

            LoadItems(out items);

            var item = items.Find(i => i.id == itemId);
            if (item == null)
            {
                return 0;
            }

            return item.count;
        }
        
        public string GetCurrentBGM()
        {
            return ES3.Load<string>(RecordType_CurrentBGM);
        }

        public object LoadBackgroundIndex()
        {
            return ES3.Load<int>(RecordType_CurrentBackground);
        }


        public List<int> LoadPurchaseProduct()
        {
            var list = new List<int>();

            if (ES3.KeyExists(RecordType_ItemList))
            {
                list = ES3.Load<List<int>>(RecordType_ItemList);
            }
            return list;
        }

        public void UseDiamond(int price)
        {
            SaveAddDiamond(price*-1);
        }
    }
}