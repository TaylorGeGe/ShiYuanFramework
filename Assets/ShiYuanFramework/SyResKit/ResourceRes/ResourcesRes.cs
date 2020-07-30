using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy
{
    public class CoroutineRunner : MonoSingleton<CoroutineRunner> { }
    /// <summary>
    /// ResourcesRes：通过 Resources 加载的资源，前缀是 resources://
    /// </summary>
    public class ResourcesRes : Res
    {
        public const string PREFIX = "resources://";

        public override void Load()
        {
            var resourceName = Name.Remove(0, ResourcesRes.PREFIX.Length);
            Asset = Resources.Load(resourceName, ResType);

            State = ResState.Loaded;
            DispatchOnLoadEvent(true);
        }

        public override void LoadAsync()
        {
            State = ResState.Loading;


            // 存储异步任务。
            mLoadAsyncTask = CoroutineRunner.Instance.StartCoroutine(DoLoadAsync());
        }

        public IEnumerator DoLoadAsync()
        {
            var resourceName = Name.Remove(0, ResourcesRes.PREFIX.Length);

            var loadRequest = Resources.LoadAsync(resourceName, ResType);

            yield return loadRequest;

            if (loadRequest.asset)
            {
                Asset = loadRequest.asset;

                State = ResState.Loaded;
                DispatchOnLoadEvent(true);
                // onLoad(true, this);
            }
            else
            {
                State = ResState.NotLoad;
                DispatchOnLoadEvent(false);
                // onLoad(false, null);
            }
            // 异步任务完成之后，需要置空
            mLoadAsyncTask = null;
        }

        public override void Unload()
        {
            // 卸载操作
            Resources.UnloadAsset(Asset);

            State = ResState.NotLoad;
        }
    }
}