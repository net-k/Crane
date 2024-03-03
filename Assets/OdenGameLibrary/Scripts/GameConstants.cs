// #define DEBUG_GAME // 素材がすべて落とせる
// #define DEBUG_MODE
// #define DEBUG_ITEM

public class GameConstants
{
#if DEBUG_ITEM
    public static bool IsInfiniteItemUsage = true;
#else
    public static bool IsInfiniteItemUsage = false;
#endif
#if DEBUG_MODE    
    public static bool IsDebugMode = true;
#else
    public static bool IsDebugMode = false;
#endif   
    
    public static int MaxBreadNumber = 11;
    
#if DEBUG_GAME
    public static int RandomNextMaxIndex = MaxBreadNumber; // debug
#else
    public static int RandomNextMaxIndex = 5; // これが本番 release
#endif 
    
    public static readonly string ApplicationVersion = "0.9.0.0";
    public static int MaxShopLevel = 11;

    public static int WatermelonBonusScore = 500;
    public static float ScoreBonusFactor = 0.5f;

    public static int INITIAL_SHOP_LEVEL = 1;
    public static int INITIAL_MONEY = 0;
    public static int INITIAL_DIAMOND = 0;
    
    // 動画によるポイント取得
    // public static int LoginBonusAdditionalMoney = 25;
    // public static int RewardVideoAdditionalMoney = 10;
    public static int WatchPointAddedVideoTimesPerDay = 5;
    // public static int TreasureBoxMoney = 5;
    public static int RewardVideoAdditionalDiamond = 1;
}
