using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sy.Tests
{
    public class ResKitV0_0_15Tests
    {
        [Test]
        public void LoadAssetBundleDependenciesTest()
        {
            var resLoader = new ResLoader();

            var coinGetPrefab = resLoader.Load<GameObject>("coin_get_prefab", "coin_get");

            var coinGetObj = Object.Instantiate(coinGetPrefab);

            var audioClip = coinGetObj.GetComponent<AudioSource>().clip;

            Assert.IsTrue(audioClip);
        }
        [UnityTest]
        public IEnumerator LoadAssetBundleDependenciesAsyncTest()
        {
            var resLoader = new ResLoader();

            var loadDone = false;

            resLoader.LoadAsync<GameObject>("coin_get_prefab", "coin_get", (succeed, res) =>
            {
                var coinGetPrefab = res.Asset as GameObject;

                var coinGetObj = Object.Instantiate(coinGetPrefab);

                var audioClip = coinGetObj.GetComponent<AudioSource>().clip;

                Assert.IsTrue(audioClip);

                loadDone = true;
            });

            while (!loadDone)
            {
                yield return null;
            }
        }
    }
}
