using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
namespace Sy.Tests
{

    /// <summary>
    /// ResKit 的核心管理模型测试
    /// </summary>
    public class ResKitCoreModelTests
    {
        /// <summary>
        /// 用于资源管理的资源
        /// </summary>
        public class TestRes : Res
        {
            public override void Load()
            {
                State = ResState.Loaded;
            }

            public override void LoadAsync()
            {
                State = ResState.Loaded;
                // onLoad(true, this);
                DispatchOnLoadEvent(true);
            }

            public override void Unload()
            {
                State = ResState.NotLoad;
            }
        }

        /// <summary>
        /// 自定义的资源类型测试
        /// </summary>
        [Test]
        public void CustomResTest()
        {
            var customRes = new TestRes()
            {
                Name = "TestRes"
            };

            customRes.Load();

            Assert.AreEqual(ResState.Loaded, customRes.State);


        }
        // [Test]
        // public void ResMgrCURDTest()
        // {
        //     var customRes = new TestRes()
        //     {
        //         Name = "TestRes"
        //     };
        //     var resMgr = ResMgr.Instance;

        //     resMgr.LoadedReses.Add(customRes.Name, customRes);

        //     Assert.IsTrue(resMgr.LoadedReses.ContainsKey(customRes.Name));
        //     Assert.AreSame(resMgr.LoadedReses[customRes.Name], customRes);

        //     resMgr.LoadedReses.Remove(customRes.Name);

        //     Assert.IsFalse(resMgr.LoadedReses.ContainsKey(customRes.Name));
        // }
        [Test]
        public void ResMgrCurdTest()
        {
            var customRes = new TestRes()
            {
                Name = "TestRes"
            };

            var resMgr = ResMgr.Instance;

            resMgr.AddRes(customRes);

            Assert.IsNotNull(resMgr.GetRes(customRes.Name));
            Assert.AreSame(resMgr.GetRes(customRes.Name), customRes);

            resMgr.RemoveRes(customRes.Name);

            Assert.IsNull(resMgr.GetRes(customRes.Name));
        }

        /// <summary>
        /// ResLoader 测试
        /// </summary>
        // [Test]
        // public void ResLoaderTest()
        // {
        //     var resLoader = new ResLoader();
        //     var texture = resLoader.Load<Texture2D>("icon_texture");
        //     resLoader.UnloadAllAssets();
        //     resLoader = null;
        // }
        /// <summary>
        /// ResLoader 测试
        /// </summary>
        [Test]
        public void ResLoaderTest()
        {
            ResFactory.RegisterCustomRes((resSearchKeys) =>
                       {
                           if (resSearchKeys.Address.StartsWith("test://"))
                           {
                               return new TestRes()
                               {
                                   Name = resSearchKeys.Address,
                                   ResType = resSearchKeys.ResType
                               };
                           }

                           return null;
                       });
            // 测试 
            var resLoader = new ResLoader();

            var iconTextureRes = resLoader.LoadRes(new ResSearchKeys("test://icon_texture", typeof(Texture2D)));

            Assert.IsTrue(iconTextureRes is TestRes);
            Assert.AreEqual(1, iconTextureRes.RefCount);
            Assert.AreEqual(ResState.Loaded, iconTextureRes.State);

            resLoader.UnloadAllAssets();

            Assert.AreEqual(0, iconTextureRes.RefCount);
            Assert.AreEqual(ResState.NotLoad, iconTextureRes.State);
        }

        [Test]
        public void ResourcesResTest()
        {
            var resLoader = new ResLoader();

            var audioClip = resLoader.Load<AudioClip>("resources://ring");

            Assert.IsNotNull(audioClip);

            var audioClipRes = resLoader.LoadRes(new ResSearchKeys("resources://ring", typeof(AudioClip)));

            Assert.AreEqual(1, audioClipRes.RefCount);
            Assert.AreEqual(ResState.Loaded, audioClipRes.State);

            resLoader.UnloadAllAssets();

            Assert.AreEqual(0, audioClipRes.RefCount);
            Assert.AreEqual(ResState.NotLoad, audioClipRes.State);

            resLoader = null;
        }

    }
}