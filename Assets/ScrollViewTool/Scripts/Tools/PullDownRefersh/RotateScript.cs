using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace yoyohan
{
    /// <summary>
    /// 描述：
    /// 功能：
    /// 作者：yoyohan
    /// 创建时间：2020-01-19 11:48:24
    /// </summary>
    public class RotateScript : MonoBehaviour
    {
        public float speed = 1f;

        // Update is called once per frame
        void Update()
        {
            Vector3 rot = gameObject.transform.localEulerAngles;
            rot.z = rot.z + speed * Time.deltaTime;
            gameObject.transform.localEulerAngles = rot;
        }
    }
}

