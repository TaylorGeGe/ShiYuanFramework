using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
namespace Sy.Tests
{
    public class TypeEventSystemTests
    {
        [Test]
        public void TypeEventSystem_RegisterTest()
        {
            var receivedMsg = string.Empty;

            Action<string> onReceive = (msg) => { receivedMsg = msg; };

            TypeEventSystem.Register(onReceive);

            TypeEventSystem.Send("Hello");

            Assert.AreEqual(receivedMsg, "Hello");

            // 为了避免影响其他的单元测试，所以要注销一下
            TypeEventSystem.UnRegister(onReceive);
        }

        [Test]
        public void TypeEventSystem_SendTest()
        {
            var receivedCount = 0;

            Action<string> onReceive = (msg) => { receivedCount++; };

            TypeEventSystem.Register(onReceive);
            TypeEventSystem.Register(onReceive);
            TypeEventSystem.Register(onReceive);
            TypeEventSystem.Register(onReceive);
            TypeEventSystem.Register(onReceive);

            TypeEventSystem.Send("Hello");

            Assert.AreEqual(receivedCount, 5);

            // 为了避免影响其他的单元测试，所以要注销一下
            TypeEventSystem.UnRegister(onReceive);
            TypeEventSystem.UnRegister(onReceive);
            TypeEventSystem.UnRegister(onReceive);
            TypeEventSystem.UnRegister(onReceive);
            TypeEventSystem.UnRegister(onReceive);
        }

        [Test]
        public void TypeEventSystem_UnRegisterTest()
        {
            var receivedCount = 0;

            Action<string> onReceive = (msg) => { receivedCount++; };

            TypeEventSystem.Register(onReceive);
            TypeEventSystem.Register(onReceive);
            TypeEventSystem.Register(onReceive);
            TypeEventSystem.Register(onReceive);
            TypeEventSystem.Register(onReceive);

            TypeEventSystem.UnRegister(onReceive);
            TypeEventSystem.UnRegister(onReceive);
            TypeEventSystem.UnRegister(onReceive);


            TypeEventSystem.Send("Hello");

            Assert.AreEqual(receivedCount, 2);

            // 为了避免影响其他的单元测试，所以要注销一下
            TypeEventSystem.UnRegister(onReceive);
            TypeEventSystem.UnRegister(onReceive);
        }
    }
}