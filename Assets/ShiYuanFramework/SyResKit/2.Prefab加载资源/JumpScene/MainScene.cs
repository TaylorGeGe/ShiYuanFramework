using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sy
{
    public class MainScene : MonoBehaviour
    {


        [SerializeField] private GameObject go1;
        [SerializeField] private GameObject go2;

        private GameObject mRingGameObj = null;
        private GameObject mIconGameObj = null;
        // Start is called before the first frame update
        void Start()
        {
            transform.Find("BtnJump").GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainScene");
            });
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mRingGameObj = Instantiate(go1);
                mIconGameObj = Instantiate(go2);

            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(mRingGameObj);
                Destroy(mIconGameObj);
                mRingGameObj = null;
                mIconGameObj = null;
            }

        }
    }
}
