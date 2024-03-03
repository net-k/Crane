using UnityEngine;

namespace FruitShop.Domain
{
    public class BonusCalculator
    {
        public int CalculateScoreBonus(int score)
        {
            // スコアにいい感じの係数を書けたい
            return (int)(score *  GameConstants.ScoreBonusFactor);
        }
        
        public int CalculateWatermelonBonus(int watermelonCount)
        {
            return watermelonCount * GameConstants.WatermelonBonusScore;
        }
        
        
    }
}
