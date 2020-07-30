using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy
{
    public enum ResState
    {
        /// <summary>
        /// 未加载
        /// </summary>
        NotLoad = 0,

        /// <summary>
        /// 正在加载
        /// </summary>
        Loading = 1,

        /// <summary>
        /// 已加载好
        /// </summary>
        Loaded = 2,
    }
    /// <summary>
    /// 资源基类、负责存储资源状态、负责加载和卸载资源。
    /// </summary>
    public abstract class Res : SimpleRC
    {
        /// <summary>
        /// 资源状态
        /// </summary>
        public ResState State { get; protected set; }

        /// <summary>
        /// 资源名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public Type ResType { get; set; }

        /// <summary>
        /// 加载的资源
        /// </summary>
        public UnityEngine.Object Asset { get; set; }

        #region 用于向外提供异步加载完成的事件

        protected event Action<bool, Res> mOnLoad = null;

        public void RegisterOnLoadEventOnce(Action<bool, Res> onLoad)
        {
            mOnLoad += onLoad;
        }
        public virtual bool MatchResSearchKeysWithoutName(ResSearchKeys resSearchKeys)
        {
            return resSearchKeys.ResType == ResType;
        }
        protected void DispatchOnLoadEvent(bool succeed)
        {
            if (mOnLoad != null)
            {
                mOnLoad.Invoke(succeed, this);
                mOnLoad = null;
            }
        }

        #endregion

        #region 用于异步任务的中断

        protected Coroutine mLoadAsyncTask = null;

        public void StopLoadAsyncTask()
        {
            if (mLoadAsyncTask != null)
            {
                CoroutineRunner.Instance.StopCoroutine(mLoadAsyncTask);

                mLoadAsyncTask = null;
            }
        }

        #endregion
        public abstract void Load();

        public abstract void LoadAsync();

        public abstract void Unload();

        protected override void OnZeroRef()
        {
            // 自动触发卸载操作
            Unload();

            // 删除掉 ResMgr 中的共享资源
            ResMgr.Instance.RemoveRes(Name);
        }
    }
}