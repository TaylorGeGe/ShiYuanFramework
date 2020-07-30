using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
namespace Sy
{
    public class AssemblyUtil : MonoBehaviour
    {
        public static Assembly EditorAssembly
        {
            get
            {
                // 1.获取当前项目中所有的 assembly (可以理解为 代码编译好的 dll)
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                // 2.获取编辑器环境(dll)
                var editorAssembly = assemblies.First(assembly => assembly.FullName.StartsWith("Assembly-CSharp-Editor"));

                return editorAssembly;
            }
        }
    }
}