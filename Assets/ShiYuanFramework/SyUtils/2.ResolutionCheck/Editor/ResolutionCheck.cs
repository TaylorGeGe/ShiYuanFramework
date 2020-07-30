using NUnit.Framework;
using UnityEngine;

namespace Sy
{
    public static class ResolutionCheck
    {
        public static bool IsPad
        {
            get { return IsRatio(4, 3); }
        }
        public static bool IsPhone16_9
        {
            get { return IsRatio(4, 3); }
        }
        public static bool IsRatio(float width, float height)
        {
            var aspectRatio = ResolutionCheck.IsLandscape
           ? (float)Screen.width / Screen.height
           : (float)Screen.height / Screen.height;
            var destinationRatio = width / height;
            return aspectRatio > destinationRatio - 0.05f && aspectRatio < destinationRatio + 0.05f;
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
    }
}
