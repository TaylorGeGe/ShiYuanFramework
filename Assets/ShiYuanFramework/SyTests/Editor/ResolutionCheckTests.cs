using NUnit.Framework;
using UnityEngine;

namespace Sy.Tests
{
    public class ResolutionCheckTests
    {

        [Test]
        public void ReslutionCheck_LandScapeTests()
        {
            Debug.LogFormat("是否是横屏：{0}", IsLandscape);
            Assert.AreEqual(IsLandscape, Screen.width > Screen.height);
        }
        public static bool IsLandscape
        {
            get
            {
                var screenWidth = Screen.width;
                var screenHeight = Screen.height;
                return screenWidth > screenHeight;
            }

        }
        [Test]
        public void ResolutionCheck_4_3_Tests()
        {


            var aspectRatio = ResolutionCheckTests.IsLandscape
            ? (float)Screen.width / Screen.height
            : (float)Screen.height / Screen.height;
            var isPad = aspectRatio > 4.0f / 3 - 0.05f && aspectRatio < 4.0f / 3 + 0.05f;
            Debug.LogFormat("是否是 4:3 分辨率?{0}", isPad);
        }
        [Test]
        public void ResolutionCheckIsPad()
        {
            Debug.LogFormat("是否是 4:3 分辨率?{0}", ResolutionCheck.IsPad);
        }
        // public static bool IsPad
        // {
        //     get
        //     {
        //         var aspectRatio = ResolutionCheckTests.IsLandscape
        //            ? (float)Screen.width / Screen.height
        //            : (float)Screen.height / Screen.width;

        //         return aspectRatio > 4.0f / 3 - 0.05f && aspectRatio < 4.0f / 3 + 0.05f;
        //     }
        // }
        [Test]
        public void ResolutionCheck_16_9()
        {
            Debug.LogFormat("是否是 16:9 分辨率?{0}", ResolutionCheck.IsPhone16_9);

        }

        // public static bool IsPhone16_9
        // {
        //     get
        //     {
        //         var aspectRatio = ResolutionCheckTests.IsLandscape
        //             ? (float)Screen.width / Screen.height
        //             : (float)Screen.height / Screen.width;

        //         return aspectRatio > 16.0f / 9 - 0.05f && aspectRatio < 16.0f / 9 + 0.05f;
        //     }
        // }
    }
}
