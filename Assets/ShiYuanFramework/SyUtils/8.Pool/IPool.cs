using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy
{
    public interface IPool<T>
    {
        T Allocate();

        bool Recycle(T obj);


    }

    public abstract class Pool<T> : IPool<T>
    {
        protected IObjectFactory<T> mFactory;
        protected Stack<T> mCacheStack = new Stack<T>();

        public int CurCount
        {
            get { return mCacheStack.Count; }
        }
        protected int mMaxCount = 5;
        public virtual T Allocate()
        {
            return mCacheStack.Count == 0
                           ? mFactory.Create()
                           : mCacheStack.Pop();
        }

        public abstract bool Recycle(T obj);
    }

    public class SimpleObjectPool<T> : Pool<T>
    {
        readonly Action<T> mResetMethod;

        public SimpleObjectPool(Func<T> factoryMethod, Action<T> resetMethod = null, int initCount = 0)
        {
            mFactory = new CustomObjectFactory<T>(factoryMethod);
            mResetMethod = resetMethod;

            for (int i = 0; i < initCount; i++)
            {
                mCacheStack.Push(mFactory.Create());
            }
        }

        public override bool Recycle(T obj)
        {
            mResetMethod.InvokeGracefully(obj);
            mCacheStack.Push(obj);
            return true;
        }

    }

    public interface IPoolable
    {
        void OnRecycled();

        bool IsRecycled { get; set; }
    }
    public class SafeObjectPool<T> : Pool<T> where T : IPoolable, new()
    {
        public override T Allocate()
        {
            T result = base.Allocate();
            result.IsRecycled = false;
            return result;
        }

        public override bool Recycle(T t)
        {
            if (t == null || t.IsRecycled)
            {
                return false;
            }

            if (mMaxCount > 0)
            {
                if (mCacheStack.Count >= mMaxCount)
                {
                    t.OnRecycled();
                    return false;
                }
            }

            t.IsRecycled = true;
            t.OnRecycled();
            mCacheStack.Push(t);

            return true;
        }
        public void OnSingletonInit()
        {
        }
        public SafeObjectPool()
        {
            mFactory = new DefaultObjectFactory<T>();
            // Debug.Log(mFactory.GetType());
        }
        private static SafeObjectPool<T> mInstance = null;

        public static SafeObjectPool<T> Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new SafeObjectPool<T>();
                }

                return mInstance;
            }
        }
        public void Dispose()
        {
            mInstance = null;
        }
        /// <summary>
        /// Init the specified maxCount and initCount.
        /// </summary>
        /// <param name="maxCount">Max Cache count.</param>
        /// <param name="initCount">Init Cache count.</param>
        public void Init(int maxCount, int initCount)
        {
            if (maxCount > 0)
            {
                initCount = Math.Min(maxCount, initCount);

                mMaxCount = maxCount;
            }

            if (CurCount < initCount)
            {
                for (int i = CurCount; i < initCount; ++i)
                {
                    Recycle(mFactory.Create());


                }
            }
        }
        /// <summary>
        /// Gets or sets the max cache count.
        /// </summary>
        /// <value>The max cache count.</value>
        public int MaxCacheCount
        {
            get { return mMaxCount; }
            set
            {
                mMaxCount = value;

                if (mCacheStack != null)
                {
                    if (mMaxCount > 0)
                    {
                        if (mMaxCount < mCacheStack.Count)
                        {
                            int removeCount = mMaxCount - mCacheStack.Count;
                            while (removeCount > 0)
                            {
                                mCacheStack.Pop();
                                --removeCount;
                            }
                        }
                    }
                }
            }
        }
    }

}