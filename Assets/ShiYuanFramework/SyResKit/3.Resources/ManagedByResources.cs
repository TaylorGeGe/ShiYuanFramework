using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy.Playground
{
    public class ManagedByResources : MonoBehaviour
    {
        // List<Object> mLoadedAssets = new List<Object>();
        ResLoader resLoader = new ResLoader();

        public AudioSource audioSource;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        private void Start()
        {

            // var subject = new ConcreteSubject();
            // var observer = new ConcreteObserver(subject);
            // subject.Attach(observer);
            // subject.SetState("测试");

        }
        public bool loaded = false;
        private void Update()
        {
            // 点击鼠标左键加载资源
            if (Input.GetMouseButtonDown(0))
            {
                LoadAsync();
            }

            // 点击鼠标右键卸载资源
            if (Input.GetMouseButtonDown(1))
            {
                resLoader.UnloadAllAssets();
            }
        }

        void LoadAsync()
        {
            resLoader.LoadAsync<AudioClip>("resources://rin是g", (b, res) =>
            {
                if (b)
                {
                    audioSource.clip = res.Asset as AudioClip;
                    audioSource.Play();
                    loaded = true;
                    Debug.Log("加载完成");
                }
                else
                {
                    loaded = false;
                    Debug.Log("没有资源");
                }
            });


        }
        private void OnDestroy()
        {
            resLoader.UnloadAllAssets();
            resLoader = null;

        }
    }
}