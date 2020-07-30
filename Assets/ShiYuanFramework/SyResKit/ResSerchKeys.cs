using System;
namespace Sy
{
    public class ResSearchKeys
    {
        /// <summary>
        /// 资源地址 (前缀 + 资源路径)
        /// </summary>
        public string Address { get; private set; }


        /// <summary>
        /// 资源的类型
        /// </summary>
        public Type ResType { get; private set; }
        /// <summary>
        /// 如果是 AB 中的资源，就要填充这个值
        /// </summary>
        public string OwnerBundleName { get; private set; }

        // public ResSearchKeys(string address, Type resType)
        // {
        //     ResType = resType;

        //     Address = address;
        // }
        public ResSearchKeys(string address, Type resType = null, string ownerBundleName = null)
        {
            ResType = resType ?? typeof(UnityEngine.Object);

            Address = address;

            OwnerBundleName = ownerBundleName;
        }
    }
}