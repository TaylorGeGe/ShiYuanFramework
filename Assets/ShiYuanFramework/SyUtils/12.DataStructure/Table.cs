using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sy
{

    // 为了过渡，先把 TestDataItem 的定义建立在这里
    // public class TestDataItem
    // {
    //     public string Name { get; set; }

    //     public int Age { get; set; }
    // }


    public abstract class Table<TDataItem> : IEnumerable<TDataItem> where TDataItem : class
    {
        List<TDataItem> mItems = new List<TDataItem>();

        public void Add(TDataItem item)
        {
            mItems.Add(item);
            OnAdd(item);
        }
        public void Remove(TDataItem item)
        {
            mItems.Remove(item);
            OnRemove(item);
        }
        public void Clear()
        {
            mItems.Clear();

            OnClear();
        }
        // 改，由于 TDataItem 是引用类型，所以直接改值即可。
        public void Update()
        {
        }
        protected abstract void OnClear();
        protected abstract void OnAdd(TDataItem item);
        protected abstract void OnRemove(TDataItem item);
        public IEnumerable<TDataItem> Get(Func<TDataItem, bool> condition)
        {
            return mItems.Where(condition);
        }
        // 新增
        public IEnumerator<TDataItem> GetEnumerator()
        {
            return mItems.GetEnumerator();
        }

        // 新增
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}