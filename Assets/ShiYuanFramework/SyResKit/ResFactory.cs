using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy
{

    /// <summary>
    /// 用于创建 Res，可以根据不同的地址返回不同的 Res，支持 Res 扩展。
    /// </summary>
    public class ResFactory
    {
        private static Func<ResSearchKeys, Res> mResCreator = s => null;
        // private static Func<string, Res> mResCreator = s => null;
        /// <summary>
        /// 根据地址加载资源
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static Res Create(ResSearchKeys resSearchKeys)
        {
            if (resSearchKeys.Address.StartsWith(ResourcesRes.PREFIX))
            {
                return new ResourcesRes()
                {
                    Name = resSearchKeys.Address,
                    ResType = resSearchKeys.ResType
                };
            }
            if (resSearchKeys.Address.StartsWith("ab://"))
            {
                return new AssetBundleRes()
                {
                    Name = resSearchKeys.Address,
                    ResType = resSearchKeys.ResType
                };
            }
            // 新增
            if (!string.IsNullOrEmpty(resSearchKeys.OwnerBundleName))
            {
                return new AssetRes()
                {
                    Name = resSearchKeys.Address,
                    ResType = resSearchKeys.ResType,
                    OwnerBundleName = resSearchKeys.OwnerBundleName
                };
            }
            return mResCreator.Invoke(resSearchKeys);
        }
        /// <summary>
        /// 注册自定义的资源的创建功能
        /// </summary>
        /// <param name="customResCreator"></param>
        public static void RegisterCustomRes(Func<ResSearchKeys, Res> customResCreator)
        {
            mResCreator = customResCreator;
        }
    }
}