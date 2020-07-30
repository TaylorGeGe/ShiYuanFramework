using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Sy
{
    public class SimpleIOCInject : Attribute
    {
    }
    public class SimpleIOC : ISimpleIOC
    {
        HashSet<Type> RegisteredType = new HashSet<Type>();

        // 用来存储 Instance 的字典
        Dictionary<Type, object> mInstances = new Dictionary<Type, object>();

        Dictionary<Type, Type> mDependency = new Dictionary<Type, Type>();

        public void Register<T>()
        {
            RegisteredType.Add(typeof(T));
        }

        public void RegisterInstance(object instance)
        {
            var type = instance.GetType();

            mInstances.Add(type, instance);
        }

        public void RegisterInstance<T>(object instance)
        {
            var type = typeof(T);

            mInstances.Add(type, instance);
        }

        public void Register<TBase, TConcrete>() where TConcrete : TBase
        {
            var baseObj = typeof(TBase);
            var concreteObj = typeof(TConcrete);

            mDependency.Add(baseObj, concreteObj);
        }

        public T Resolve<T>() where T : class
        {
            var type = typeof(T);

            return Resolve(type) as T;
        }

        object Resolve(Type type)
        {
            if (mInstances.ContainsKey(type))
            {
                return mInstances[type];
            }

            if (mDependency.ContainsKey(type))
            {
                // 转换 BaseType 为 ConcreteType
                return Activator.CreateInstance(mDependency[type]);
            }

            if (RegisteredType.Contains(type))
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }

        public void Inject(object obj)
        {
            foreach (var propertyInfo in obj.GetType().GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(SimpleIOCInject), true).Any()))
            {
                var instance = Resolve(propertyInfo.PropertyType);

                if (instance != null)
                {
                    propertyInfo.SetValue(obj, instance, null);
                }
                else
                {
                    Debug.LogErrorFormat("不能获取类型为:{0} 的对象", propertyInfo.PropertyType);
                }
            }
        }

        public void Clear()
        {
            RegisteredType.Clear();
            mDependency.Clear();
            mInstances.Clear();
        }
    }
}