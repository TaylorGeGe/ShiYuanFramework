using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy
{
    public class ManagedByResources : MonoBehaviour
    {
        private Texture2D mIconTexture = null;
        private AudioClip mRing = null;

        private void Update()
        {
            // 点击鼠标左键加载资源
            if (Input.GetMouseButtonDown(0))
            {
                mIconTexture = Resources.Load<Texture2D>("icon_texture");
                mRing = Resources.Load<AudioClip>("ring");
            }

            // 点击鼠标右键卸载资源
            if (Input.GetMouseButtonDown(1))
            {
                Resources.UnloadAsset(mIconTexture);
                Resources.UnloadAsset(mRing);
            }
        }
    }
}