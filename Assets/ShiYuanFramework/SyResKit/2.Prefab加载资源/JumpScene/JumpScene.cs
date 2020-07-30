using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sy
{
    public class JumpScene : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            transform.Find("BtnJump").GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Scene");
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}