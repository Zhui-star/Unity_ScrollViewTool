using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yoyohan
{
    /// <summary>
    /// 描述：
    /// 功能：
    /// 作者：yoyohan
    /// 创建时间：2019-09-05 17:17:49
    /// </summary>
    public class ScrollBarBind : MonoBehaviour
    {

        public GameObject bindGos;
        private void OnEnable()
        {
            bindGos.gameObject.SetActive(true);
        }
        private void OnDisable()
        {
            bindGos.gameObject.SetActive(false);
        }
    }
}