using UnityEngine;
using Sy;
namespace Sy
{
    public class AssetBundleRes : Res
    {
        public const string PREFIX = "ab://";
        ResLoader mResLoader = new ResLoader();
        private static AssetBundleManifest mManifest;
        public static AssetBundleManifest Manifest
        {
            get
            {
                if (!mManifest)
                {
                    var bundleName = AssetBundleUtil.FullPathForAssetBundleName(AssetBundleUtil.GetPlatformName());
                    var bundle = AssetBundle.LoadFromFile(bundleName);
                    mManifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                }

                return mManifest;
            }
        }
        public override void Load()
        {
            var assetBundleFileName = Name.Remove(0, PREFIX.Length);

            var dependBundleNames = Manifest.GetDirectDependencies(assetBundleFileName);

            foreach (var dependBundleName in dependBundleNames)
            {
                mResLoader.Load<AssetBundle>("ab://" + dependBundleName);
            }

            // 新增
            var path = AssetBundleUtil.FullPathForAssetBundleName(assetBundleFileName);
            Asset = AssetBundle.LoadFromFile(path);

            State = ResState.Loaded;
            DispatchOnLoadEvent(true);
        }
        private void LoadDependenyBundleAsync(System.Action onLoadDone)
        {
            var assetBundleFileName = Name.Remove(0, PREFIX.Length);

            var dependBundles = Manifest.GetDirectDependencies(assetBundleFileName);

            if (dependBundles.Length == 0)
            {
                onLoadDone();
            }

            var loadedCount = 0;

            foreach (var dependBundle in dependBundles)
            {
                mResLoader.LoadAsync<AssetBundle>("ab://" + dependBundle, (succeed, res) =>
                {
                    loadedCount++;

                    if (loadedCount == dependBundles.Length)
                    {
                        onLoadDone();
                    }
                });
            }
        }
        public override void LoadAsync()
        {
            State = ResState.Loading;
            LoadDependenyBundleAsync(() =>
            {
                var assetBundleFileName = Name.Remove(0, PREFIX.Length);
                // 新增
                var path = AssetBundleUtil.FullPathForAssetBundleName(assetBundleFileName);
                var request = AssetBundle.LoadFromFileAsync(path);
                request.completed += operation =>
                {
                    Asset = request.assetBundle;
                    State = ResState.Loaded;
                    DispatchOnLoadEvent(true);
                };
            });

        }

        public override void Unload()
        {
            var assetBundle = Asset as AssetBundle;

            if (assetBundle != null)
            {

                assetBundle.Unload(true);
            }
            mResLoader.UnloadAllAssets();
            mResLoader = null;
        }
    }
}