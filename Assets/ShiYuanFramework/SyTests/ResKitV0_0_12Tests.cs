using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Sy.Tests
{
    public class ResKitV0_0_12Tests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void LoadAssetBundle()
        {
            var resLoader = new ResLoader();

            var coinGetBundle = resLoader.Load<AssetBundle>("ab://coin_get");

            var coinGetClip = coinGetBundle.LoadAsset<AudioClip>("coin_get");

            Assert.IsTrue(coinGetClip);

            resLoader.UnloadAllAssets();
        }

        [UnityTest]
        public IEnumerator LoadAssetBundleAsync()
        {
            var resLoader = new ResLoader();

            var loadDone = false;

            resLoader.LoadAsync<AssetBundle>("ab://coin_get", (succeed, res) =>
            {
                var coinGetBundle = res.Asset as AssetBundle;

                var coinGetClip = coinGetBundle.LoadAsset<AudioClip>("coin_get");

                Assert.IsTrue(coinGetClip);

                loadDone = true;
            });

            while (!loadDone)
            {
                yield return null;
            }

            resLoader.UnloadAllAssets();
        }
    }
}
