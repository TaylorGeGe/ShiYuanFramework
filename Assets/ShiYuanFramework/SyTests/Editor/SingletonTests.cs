using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Sy.Tests
{


    public class SingletonTests
    {
        public class ClassA : Singleton<ClassA>
        {
            private ClassA()
            {
            }
        }

        [Test]
        public void Singleton_SingletonTest()
        {
            var objA = ClassA.Instance;

            var objB = ClassA.Instance;

            Assert.AreSame(objA, objB);
        }

        public class MonoClassA : MonoSingleton<MonoClassA>
        {

        }


        [Test]
        public void Singleton_MonoSingletonTest()
        {
            var objA = MonoClassA.Instance;
            var objB = MonoClassA.Instance;

            Assert.AreSame(objA, objB);

            // 测试可以找到 MonoClassA
            var monoClass = GameObject.Find("MonoClassA");

            Assert.IsNotNull(monoClass);

            Object.Destroy(monoClass);
        }
    }
}