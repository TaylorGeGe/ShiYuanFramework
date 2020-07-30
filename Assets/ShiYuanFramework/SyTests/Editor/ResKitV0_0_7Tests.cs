using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sy.Tests
{
    public class ResKitV0_0_7Tests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void LoadAsyncTwiceBugTest()
        {
            var resLoader = new ResLoader();

            resLoader.LoadAsync<AudioClip>("resources://coin_get", (b, res) => { });
            resLoader.LoadAsync<AudioClip>("resources://coin_get", (b, res) => { });

            Assert.Pass();
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ResKitV0_0_7TestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
