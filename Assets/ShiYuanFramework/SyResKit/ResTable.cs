using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Sy
{
    public class ResTable : Table<Res>
    {

        /// <summary>
        /// 根据
        /// </summary>
        public TableIndex<string, Res> NameIndex { get; private set; }
        public ResTable()
        {
            NameIndex = new TableIndex<string, Res>(res => res.Name);
        }
        // 新增
        public Res GetResWithSearchKeys(ResSearchKeys resSearchKeys)
        {
            return NameIndex.Get(resSearchKeys.Address)
                .FirstOrDefault(r => r.MatchResSearchKeysWithoutName(resSearchKeys));
        }


        protected override void OnAdd(Res item)
        {
            NameIndex.Add(item);
        }

        protected override void OnRemove(Res item)
        {
            NameIndex.Remove(item);
        }

        protected override void OnClear()
        {
            NameIndex.Clear();
        }
    }


}