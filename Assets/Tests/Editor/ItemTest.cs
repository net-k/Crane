using NUnit.Framework;
using UnityEngine;

public class ItemTest
{
    [Test]
    public void ExceptIndexTest()
    {
        ItemLogic itemLogic = new ItemLogic();

        bool ret = false;
        for (int i = 0; i < GameConstants.RandomNextMaxIndex; i++)
        {
            int exceptIndex = i;

            int tryCount = 100;
            while (tryCount >= 0)
            {
                int nextInedex = itemLogic.GetRandomNextExcept(exceptIndex, GameConstants.RandomNextMaxIndex);
                ret = exceptIndex != nextInedex;
                Debug.Log($"ei:{exceptIndex} ni:{nextInedex} r:{ret}");
                if (!ret)
                {
                    break;
                }

                tryCount--;
            }
        }
        Assert.That(ret);
    }
}
