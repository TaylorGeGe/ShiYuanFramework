using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
namespace Sy.Tests
{
    public class MathUtilTests
    {
        [Test]
        public static void MathUtil_PersentTest()
        {
            var percent = 50;
            var trueCount = 0;
            // 随机 1000 次
            for (var i = 0; i < 1000; i++)
            {
                if (MathUtil.Percent(50))
                {
                    trueCount++;
                }
            }
            Debug.Log(trueCount);
            Assert.IsTrue(trueCount < 600 && trueCount > 400);
        }
    }
}