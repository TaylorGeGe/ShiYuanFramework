
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Sy
{

    /// <summary>
    /// Res 的服务类，主要面向用户，提供简化的资源加载和卸载 API。
    /// </summary>
    public class ResLoader
    {
        private ResTable mLoadedReses = new ResTable();

        public T Load<T>(string ownerBundleName, string assetName) where T : UnityEngine.Object
        {
            var res = LoadRes(new ResSearchKeys(assetName, typeof(T), ownerBundleName));

            if (res == null)
            {
                Debug.LogError("不存在的资源类型：" + assetName + ":" + typeof(T) + ":" + ownerBundleName);
                return null;
            }

            return res.Asset as T;
        }
        public void LoadAsync<T>(string ownerBundleName, string assetName, Action<bool, Res> onLoad)
        {
            LoadResAsync(new ResSearchKeys(assetName, typeof(T), ownerBundleName), onLoad);
        }


        public T Load<T>(string assetPathOrName) where T : UnityEngine.Object
        {
            // var res = LoadRes(assetPathOrName);
            var res = LoadRes(new ResSearchKeys(assetPathOrName, typeof(T)));

            if (res == null)
            {
                Debug.LogError("不存在的资源类型：" + assetPathOrName);
                return null;
            }

            return res.Asset as T;
        }

        public void LoadAsync<T>(string address, Action<bool, Res> onLoad)
        {
            LoadResAsync(new ResSearchKeys(address, typeof(T)), onLoad);
        }
        /// <summary>
        /// 从 Cache 中获取资源
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Res GetResFromCache(ResSearchKeys resSearchKeys)
        {
            var res = mLoadedReses.GetResWithSearchKeys(resSearchKeys);
            // 先判断当前脚本有没有加载过资源，加载过则直接返回
            if (res != null)
            {
                return res;
            }
            // 如果没在当前脚本加载过，则判断资源共享池中是否加载过（其他脚本是否正在使用资源）
            res = ResMgr.Instance.GetRes(resSearchKeys);

            if (res != null)
            {
                Add2LoadedReses(res);

                return res;
            }

            return null;
        }

        void Add2LoadedReses(Res res)
        {
            res.Retain();

            // 记录到当前的脚本记录中
            mLoadedReses.Add(res);
        }

        public Res LoadRes(ResSearchKeys resSearchKeys)
        {
            var res = GetResFromCache(resSearchKeys);

            if (res != null)
            {
                if (res.State == ResState.Loaded)
                {
                    return res;
                }

                if (res.State == ResState.Loading)
                {
                    // 正在做异步加载
                    res.StopLoadAsyncTask();

                    res.Load();

                    return res;
                }

                if (res.State == ResState.NotLoad)
                {
                    throw new Exception(string.Format("{0} 状态异常 {1}", resSearchKeys, res.State));
                }
            }

            // 如果都未记录，则通过 ResFactory.Create 创建资源
            res = ResFactory.Create(resSearchKeys);

            if (res == null) return null;

            // 记录到资源共享池中
            ResMgr.Instance.AddRes(res);

            // 添加到 ResLoader 以加载的列表
            Add2LoadedReses(res);

            // 做加载操作
            res.Load();

            return res;
        }

        public void LoadResAsync(ResSearchKeys resSearchKeys, Action<bool, Res> onLoad)
        {
            var res = GetResFromCache(resSearchKeys);

            if (res != null)
            {
                if (res.State == ResState.Loaded)
                {
                    onLoad(true, res);
                }
                else if (res.State == ResState.Loading)
                {
                    // 有可能正在进行着异步加载，直接注册即可。
                    res.RegisterOnLoadEventOnce(onLoad);
                }
                else
                {
                    Debug.LogErrorFormat("{0} 状态异常 {1}", resSearchKeys, res.State);
                    onLoad(false, null);
                }

                return;
            }

            // 如果都未记录，则通过 ResFactory.Create 创建资源
            res = ResFactory.Create(resSearchKeys);

            if (res == null)
            {
                onLoad(false, null);
                return;
            }

            // 记录到资源共享池中
            ResMgr.Instance.AddRes(res);

            // 添加到 ResLoader 以加载的列表
            Add2LoadedReses(res);
            // 注册加载的事件
            res.RegisterOnLoadEventOnce(onLoad);
            // 做加载操作
            res.LoadAsync();
        }

        public void UnloadAllAssets()
        {

            foreach (var resKeyValue in mLoadedReses)
            {
                resKeyValue.Release();
            }

            mLoadedReses.Clear();
            // foreach (var resKeyValue in mLoadedReses)
            // {
            //     resKeyValue.Value.Release();
            // }

            // mLoadedReses.Clear();
        }
    }
}