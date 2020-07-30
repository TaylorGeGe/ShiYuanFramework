using UnityEngine;

namespace Sy
{
    public class TypeEventSystemExample : MonoBehaviour
    {
        public class EventA { }
        public class EventB { }

        // 创建服务
        TypeEventManager mTypeEventManager = new TypeEventManager();

        void Start()
        {
            mTypeEventManager.Register<EventA>(OnEventAReceive);

            // 注册支持用 lambda 表达式
            mTypeEventManager.Register<EventB>((EventB =>
            {
                Debug.Log("On Event B Receive");
            }));
        }

        void OnEventAReceive(EventA eventA)
        {
            Debug.Log("On Event A Receive");
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mTypeEventManager.Send<EventA>(new EventA());
                mTypeEventManager.Send<EventB>(new EventB());
            }
        }

        void OnDestroy()
        {
            mTypeEventManager.UnRegisterAll();
            mTypeEventManager = null;
        }
    }
}