using UnityEditor;
using UnityEngine;

namespace Sy
{
    public class EditorModulizationPlatformEditor : EditorWindow
    {
        private EditorModuleContainer mContainer;

        /// <summary>
        /// 打开窗口
        /// </summary>
        [MenuItem("SyFramework/LXUtils/0.EditorModulizationPlatform")]
        public static void Open()
        {
            EditorModulizationPlatformEditor editorPlatform = GetWindow<EditorModulizationPlatformEditor>();
            editorPlatform.position = new Rect(
                Screen.width / 2,
                Screen.height * 2 / 3,
                600,
                500
            );

            // 初始化 Container
            editorPlatform.mContainer = new EditorModuleContainer();

            editorPlatform.mContainer.Init();

            editorPlatform.Show();
        }

        private void OnGUI()
        {
            // 渲染
            mContainer.ResolveAll<IEditorPlatformModule>()
                .ForEach(editorPlatformModule => editorPlatformModule.OnGUI());
        }
    }
}