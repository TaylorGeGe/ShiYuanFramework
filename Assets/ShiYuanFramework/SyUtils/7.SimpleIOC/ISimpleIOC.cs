using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy
{
    public interface ISimpleIOC
    {
        /// <summary>
        /// 简单注册类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Register<T>();

        /// <summary>
        /// 注册为单例（不传入泛型版本）
        /// </summary>
        /// <param name="instance"></param>
        void RegisterInstance(object instance);

        /// <summary>
        /// 注册为单例
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        void RegisterInstance<T>(object instance);

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <typeparam name="TConcrete"></typeparam>
        void Register<TBase, TConcrete>() where TConcrete : TBase;

        /// <summary>
        ///  获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        T Resolve<T>() where T : class;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="obj"></param>
        void Inject(object obj);

        /// <summary>
        /// 清空 类型 和 实例
        /// </summary>
        void Clear();
    }
}