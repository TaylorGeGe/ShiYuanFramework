using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sy.Tests
{
    public class ResKitV0_0_11Tests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GetWrongTypeBugTest()
        {
            var resLoader = new ResLoader();

            var coinGetTextAsset = resLoader.Load<TextAsset>("resources://ring");

            Assert.IsNotNull(coinGetTextAsset);

            var coinGetAudioClip = resLoader.Load<AudioClip>("resources://ring");

             Assert.IsNotNull(coinGetAudioClip);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ResKitV0_0_11TestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
