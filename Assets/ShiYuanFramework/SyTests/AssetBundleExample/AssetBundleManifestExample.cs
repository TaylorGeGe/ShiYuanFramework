using System.Linq;
using UnityEngine;

namespace Sy.Tests
{
    public class AssetBundleManifestExample : MonoBehaviour
    {
        void Start()
        {
            var windowsBundlePath = AssetBundleUtil.FullPathForAssetBundleName("Windows");

            var windowsBundle = AssetBundle.LoadFromFile(windowsBundlePath);

            var manifest = windowsBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            manifest.GetAllAssetBundles()
                .ToList()
                .ForEach(Debug.Log);

            manifest.GetAllDependencies("coin_get_prefab")
                .ToList()
                .ForEach(dependBundle => Debug.LogFormat("coin_get_prefab depends:{0}", dependBundle));

            windowsBundle.Unload(true);
        }
    }
}