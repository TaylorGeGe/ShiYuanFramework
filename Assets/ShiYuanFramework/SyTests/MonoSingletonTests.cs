using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Sy;
namespace Tests
{
    public class MonoSingletonTests
    {
        public class MonoClassA : MonoSingleton<MonoClassA>
        {

        }
        // A Test behaves as an ordinary method
        [Test]
        public void MonoSingletonTestsSimplePasses()
        {
            // Use the Assert class to test conditions
 

        }
        [Test]
        public void Test_MonoSingleton()
        {
            var objA = MonoClassA.Instance;
            var objB = MonoClassA.Instance;

            Assert.AreSame(objA, objB);

            // 测试可以找到 MonoClassA
            var monoClass = GameObject.Find("MonoClassA");

            Assert.IsNotNull(monoClass);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator MonoSingletonTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
