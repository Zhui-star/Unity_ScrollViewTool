using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace yoyohan
{
    /// <summary>
    /// 描述：
    /// 功能：
    /// 作者：yoyohan
    /// 创建时间：2020-02-10 17:41:48
    /// </summary>
    public class PageDragTool : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public ScrollerCtrlBase ScrollerCtrlBase;
        public Action<int> OnPageDragToolSnap;

        private float value;

        public void OnBeginDrag(PointerEventData data)
        {
            value = ScrollerCtrlBase.scroller.NormalizedScrollPosition;
        }

        public void OnEndDrag(PointerEventData data)
        {
            if (ScrollerCtrlBase.scroller.NormalizedScrollPosition > value)
            {
                ScrollerCtrlBase.scroller.snapWatchOffset = 0.95f;
            }
            else
            {
                ScrollerCtrlBase.scroller.snapWatchOffset = 0.05f;
            }

            int dataIndex = ScrollerCtrlBase.scroller.Snap();

            if (OnPageDragToolSnap != null)
            {
                OnPageDragToolSnap(dataIndex);
            }
        }
    }
}
