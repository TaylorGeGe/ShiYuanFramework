using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
namespace Sy.Tests
{
    public class SimpleIOCTests
    {
        [Test]
        public void SimpleIOCRegisterResolveTest()
        {
            var simpleIOC = new SimpleIOC();

            simpleIOC.Register<SimpleIOC>();


            var obj = simpleIOC.Resolve<SimpleIOC>();

            // 是否创建了实例
            Assert.IsNotNull(obj);

            // 不相同 说明是 创建了实例
            Assert.AreNotEqual(simpleIOC, obj);
        }

        [Test]
        public void SimpleIOCResolveRegisteredType()
        {
            var simpleIOC = new SimpleIOC();

            // 不进行注册

            var obj = simpleIOC.Resolve<SimpleIOC>();

            // 为空值时才应该测试通过
            Assert.IsNull(obj);
        }

        [Test]
        public void SimpleIOCRegisterTwice()
        {
            var simpleIOC = new SimpleIOC();

            // 重复注册的容错
            simpleIOC.Resolve<SimpleIOC>();
            simpleIOC.Resolve<SimpleIOC>();

            // 代码到达这里就算通过
            Assert.IsTrue(true);
        }


        [Test]
        public void SimpleIOCRegsiterInstance()
        {
            var simpleIOC = new SimpleIOC();

            simpleIOC.RegisterInstance(new SimpleIOC());

            var instanceA = simpleIOC.Resolve<SimpleIOC>();
            var instanceB = simpleIOC.Resolve<SimpleIOC>();

            // 两个实例相同就算通过
            Assert.AreEqual(instanceA, instanceB);
        }

        [Test]
        public void SimpleIOCRegsiterDependency()
        {
            var simpleIOC = new SimpleIOC();

            // 注册依赖
            simpleIOC.Register<ISimpleIOC, SimpleIOC>();

            var ioc = simpleIOC.Resolve<ISimpleIOC>();

            Debug.Log(ioc.GetType());
            // 通过 ISimpleIOC 获取的对象类型应该是 SimpleIOC 
            Assert.AreEqual(ioc.GetType(), typeof(SimpleIOC));
        }

        [Test]
        public void SimpleIOCRegsiterInstanceDependency()
        {
            var simpleIOC = new SimpleIOC();

            // 注册依赖
            simpleIOC.RegisterInstance<ISimpleIOC>(simpleIOC);

            var iocA = simpleIOC.Resolve<ISimpleIOC>();
            var iocB = simpleIOC.Resolve<ISimpleIOC>();

            Assert.AreEqual(iocA, simpleIOC);
            Assert.AreEqual(iocA, iocB);
        }

        class SomeDependencyA { }

        class SomeDependencyB { }

        class SomeCtrl
        {
            [SimpleIOCInject]
            public SomeDependencyA A { get; set; }

            [SimpleIOCInject]
            public SomeDependencyB B { get; set; }
        }

        [Test]
        public void SimpleIOCInject()
        {
            var simpleIOC = new SimpleIOC();

            // 注册依赖
            simpleIOC.RegisterInstance(new SomeDependencyA());

            simpleIOC.Register<SomeDependencyB>();

            var someCtrl = new SomeCtrl();

            simpleIOC.Inject(someCtrl);

            Assert.IsNotNull(someCtrl.A);
            Assert.IsNotNull(someCtrl.B);

            Assert.AreEqual(someCtrl.A.GetType(), typeof(SomeDependencyA));
            Assert.AreEqual(someCtrl.B.GetType(), typeof(SomeDependencyB));
        }

        [Test]
        public void SimpleIOCClear()
        {
            var simpleIOC = new SimpleIOC();

            // 注册依赖
            simpleIOC.RegisterInstance(new SomeDependencyA());
            simpleIOC.RegisterInstance<ISimpleIOC>(simpleIOC);
            simpleIOC.Register<SomeDependencyB>();

            simpleIOC.Clear();

            // 获取对象
            var someDependencyA = simpleIOC.Resolve<SomeDependencyA>();
            var someDependencyB = simpleIOC.Resolve<SomeDependencyB>();
            var ioc = simpleIOC.Resolve<ISimpleIOC>();

            // 全部为空才对
            Assert.IsNull(someDependencyA);
            Assert.IsNull(someDependencyB);
            Assert.IsNull(ioc);
        }
    }
}