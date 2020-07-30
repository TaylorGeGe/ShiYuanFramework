using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy
{
    public class AssetBundleExample : MonoBehaviour
    {
        ResLoader mResLoader = new ResLoader();

        void Start()
        {
            // mResLoader.Load<AssetBundle>("ab://coin_get");
            // var coinGetPrefab = mResLoader.Load<GameObject>("coin_get_prefab", "coin_get");

            // Instantiate(coinGetPrefab);
            mResLoader.LoadAsync<GameObject>("coin_get_prefab", "coin_get", (b, res) =>
           {
               if (b)
               {
                   Instantiate(res.Asset);
                   Debug.Log(" res.RefCount" + res.RefCount);
               }

           });


        }

        private void OnDestroy()
        {
            mResLoader.UnloadAllAssets();
            mResLoader = null;
        }
    }
}