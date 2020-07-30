using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace Sy.Tests
{
    public class ResKitV0_0_10Tests
    {
        [UnityTest]
        public IEnumerator LoadAsyncTwiceBugTest()
        {
            var loadedCount = 0;

            var resLoader = new ResLoader();

            resLoader.LoadAsync<AudioClip>("resources://ring", (succeed, res) =>
            {
                Assert.AreEqual(ResState.Loaded, res.State);
                loadedCount++;
            });

            resLoader.LoadAsync<AudioClip>("resources://ring", (succeed, res) =>
            {
                Assert.AreEqual(ResState.Loaded, res.State);
                loadedCount++;
            });

            yield return new WaitUntil(() => loadedCount == 2);
        }
    }
    
}