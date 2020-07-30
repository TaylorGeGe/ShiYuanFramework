using UnityEngine;

namespace Sy
{
    public class AssetRes : Res
    {
        /// <summary>
        /// 所属的 AssetBundle 名字
        /// </summary>
        public string OwnerBundleName { get; set; }

        /// <summary>
        /// 用于获取 加载对应 AssetBundle 的 ResLoader
        /// </summary>
        private ResLoader mResLoader = new ResLoader();
        public override bool MatchResSearchKeysWithoutName(ResSearchKeys resSearchKeys)
        {
            return resSearchKeys.OwnerBundleName == OwnerBundleName && resSearchKeys.ResType == ResType;
        }

        public override void Load()
        {
            var bundle = mResLoader.Load<AssetBundle>(AssetBundleRes.PREFIX + OwnerBundleName);

            Asset = bundle.LoadAsset(Name, ResType);

            State = ResState.Loaded;

            DispatchOnLoadEvent(true);
        }

        public override void LoadAsync()
        {
            State = ResState.Loading;

            mResLoader.LoadAsync<AssetBundle>(AssetBundleRes.PREFIX + OwnerBundleName, (succeed, res) =>
            {
                var bundle = res.Asset as AssetBundle;
                var assetRequest = bundle.LoadAssetAsync(Name, ResType);

                assetRequest.completed += operation =>
                {
                    Asset = assetRequest.asset;

                    State = ResState.Loaded;

                    DispatchOnLoadEvent(true);
                };
            });
        }

        public override void Unload()
        {
            // prefab 资源不能用 Resources.UnloadAsset 清除
            if (Asset is GameObject)
            {

            }
            else
            {
                Resources.UnloadAsset(Asset);
            }

            Asset = null;

            mResLoader.UnloadAllAssets();
            mResLoader = null;
        }
    }
}