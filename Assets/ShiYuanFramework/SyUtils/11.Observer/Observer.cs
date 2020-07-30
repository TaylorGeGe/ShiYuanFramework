using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy
{

    /// <summary>
    /// 通知消息
    /// </summary>
    class NotifyEvent
    {

    }
    /// <summary>
    /// 主题
    /// </summary>
    public abstract class Subject
    {
        List<Observer> mObservers = new List<Observer>();

        // 加入观察者
        public void Attach(Observer observer)
        {
            mObservers.Add(observer);
        }

        // 删除观察者
        public void Detach(Observer observer)
        {
            mObservers.Remove(observer);
        }

        // 通知所有观察者
        public void Notify()
        {
            mObservers.ForEach(observer => observer.Update());
        }
    }

    /// <summary>
    /// 观察者    
    /// </summary>
    public abstract class Observer
    {
        public abstract void Update();
    }
    class ConcreteSubject : Subject
    {
        /// <summary>
        /// 主题状态
        /// 即：Observer 感兴趣的数据
        /// </summary>
        private string mSubjectState;

        public void SetState(string state)
        {
            mSubjectState = state;
            // 数据变更时通知
            Notify();
        }

        public string GetState()
        {
            return mSubjectState;
        }
    }

    class ConcreteObserver : Observer
    {
        private ConcreteSubject mSubject = null;

        public ConcreteObserver(ConcreteSubject subject)
        {
            mSubject = subject;
        }

        public override void Update()
        {
            Debug.Log("ConcreteObserver.Update");
            Debug.Log("ConcreteObserver:Subject 当前的主题:" + mSubject.GetState());
        }
    }
}
