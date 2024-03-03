using System;
using System.Linq;

public class ItemLogic
{
    /// <summary>
    /// ランダムシャッフル
    /// </summary>
    /// <param name="exceptIndex"></param>
    /// <returns></returns>
    public int GetRandomNextExcept(int exceptIndex, int maxIndex)
    {
        // 0 から GameConstants.RandomNextMaxIndex - 1 までの配列を作成
        int[] array = new int[maxIndex];
        // 0 から GameConstants.RandomNextMaxIndex で初期化
        for (int i = 0; i < /*GameConstants.RandomNextMaxIndex*/maxIndex; i++)
        {
            array[i] = i;
        }
        array = array.OrderBy(i => Guid.NewGuid()).ToArray();
        
        // array から exceptIndex 以外の値を返す
        for (int i = 0; i < maxIndex; i++)
        {
            if (array[i] != exceptIndex)
            {
                return array[i];
            }
        }

        return 0;
    }
}