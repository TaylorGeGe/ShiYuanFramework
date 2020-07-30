using System.IO;
using UnityEditor;
using UnityEngine;

namespace Sy
{
    public class AssetBundleBuilder : MonoBehaviour
    {
        [MenuItem("SyFramework/LXResKit/Build AssetBundles")]
        static void BuildAssetBundles()
        {
            var outputPath = Application.streamingAssetsPath + "/AssetBundles/" + AssetBundleUtil.GetPlatformName();

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression,
                EditorUserBuildSettings.activeBuildTarget);

            AssetDatabase.Refresh();
        }
    }
}