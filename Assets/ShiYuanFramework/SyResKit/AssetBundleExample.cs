using System;
using System.IO;
using UnityEngine;

namespace Sy.Playground
{
    public class AssetBundleExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("SyFramework/Playground/AssetBundleExample/Build AssetBundle")]
        static void MenuClicked1()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }

            UnityEditor.BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
                UnityEditor.BuildAssetBundleOptions.None,
                UnityEditor.BuildTarget.StandaloneWindows);
        }
#endif

        private AssetBundle mCoinGetBundle;

        void Start()
        {
            var coinGetABPath = Application.streamingAssetsPath + "/coin_get";

            mCoinGetBundle = AssetBundle.LoadFromFile(coinGetABPath);

            var coinGetAudioClip = mCoinGetBundle.LoadAsset<AudioClip>("coin_get");

            gameObject.AddComponent<AudioSource>()
                .clip = coinGetAudioClip;
                gameObject.GetComponent<AudioSource>().Play();
        }

        private void OnDestroy()
        {
            mCoinGetBundle.Unload(true);
        }
    }
}