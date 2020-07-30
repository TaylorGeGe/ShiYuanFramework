using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Sy
{
    /// <summary>
    /// Res 管理类，负责提供对 Res 的增删改查。
    /// </summary>
    public class ResMgr : Singleton<ResMgr>
    {
        private ResMgr()
        {
        }
        // private Dictionary<string, Res> mLoadedReses = new Dictionary<string, Res>();
        private ResTable mLoadedReses = new ResTable();


        public void AddRes(Res res)
        {
            mLoadedReses.Add(res);
        }
        public void RemoveRes(string resName)
        {
            var res2Remove = mLoadedReses.NameIndex.Get(resName).SingleOrDefault();
            mLoadedReses.Remove(res2Remove);
        }
        public Res GetRes(string resName)
        {
            return mLoadedReses.NameIndex.Get(resName).FirstOrDefault();
        }
        public Res GetRes(ResSearchKeys resSearchKeys)
        {
            return mLoadedReses.GetResWithSearchKeys(resSearchKeys);
        }
        public void OnResUnloaded(string AssetName)
        {

            // if (mLoadedReses.Get(AssetName, out res))
            // {
            //     mLoadedReses.Remove(AssetName);
            // }
        }
        // 更新

    }
}