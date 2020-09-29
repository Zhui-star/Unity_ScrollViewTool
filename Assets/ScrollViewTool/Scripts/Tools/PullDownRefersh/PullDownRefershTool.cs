using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using yoyohan.EnhancedUI.EnhancedScroller;

namespace yoyohan
{
    /// <summary>
    /// 描述：
    /// 功能：
    /// 作者：yoyohan
    /// 创建时间：2020-01-19 15:49:38
    /// </summary>
    public class PullDownRefershTool : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public RectTransform pullDownRefershRect;
        public float pullDownThreshold = 100;

        private ScrollerCtrlBase scrollerCtrl;
        private Action OnPullDownRefershing;
        private bool _dragging = true;
        private bool _pullToRefresh = false;
        private bool _refershed = true;
        private float refershHeight;
        private float normalHeight;
        private GameObject arrow;
        private GameObject waiticon;
        private LayoutElement layoutElement;

        private void Start()
        {
            layoutElement = pullDownRefershRect.GetComponent<LayoutElement>();
            arrow = pullDownRefershRect.Find("arrow").gameObject;
            waiticon = pullDownRefershRect.Find("waiticon").gameObject;
            normalHeight = layoutElement.minHeight;
            refershHeight = normalHeight + 50;
        }

        /// <summary>
        /// 必须注册该组件才生效
        /// </summary>
        public void RegisterPullDownRefersh(ScrollerCtrlBase scrollerCtrl, Action refershingCallBack)
        {
            this.scrollerCtrl = scrollerCtrl;
            scrollerCtrl.scroller.scrollerScrolled = ScrollerScrolled;
            OnPullDownRefershing = refershingCallBack;
        }

        private void ScrollerScrolled(EnhancedScroller scroller, Vector2 val, float scrollPosition)
        {
            bool scrollMoved = scroller.ScrollRect.content.anchoredPosition.y <= -pullDownThreshold;

            if (_dragging && _refershed)
            {
                if (scrollMoved)
                {
                    _pullToRefresh = true;
                    ShowPullDownRefersh(1);
                }
                else
                {
                    _pullToRefresh = false;
                    ShowPullDownRefersh(0);
                }
            }

        }

        public void OnBeginDrag(PointerEventData data)
        {
            _dragging = true;
        }

        public void OnEndDrag(PointerEventData data)
        {
            _dragging = false;

            if (_pullToRefresh)
            {
                _pullToRefresh = false;
                _refershed = false;

                ShowPullDownRefersh(2);
                if (OnPullDownRefershing != null)
                {
                    OnPullDownRefershing();
                }
                Invoke("EndPullDownRefersh", 2);
            }
        }

        public void EndPullDownRefersh()
        {
            _refershed = true;
            ShowPullDownRefersh(3);
        }

        /// <summary>
        /// 0关闭，1准备刷新，2刷新中，3刷新完成
        /// </summary>
        public void ShowPullDownRefersh(int id)
        {
            //Debug.Log("ShowPullDownRefersh--------" + id);
            if (id == 0)
            {
                arrow.gameObject.SetActive(false);
                waiticon.gameObject.SetActive(false);
                layoutElement.minHeight = normalHeight;
            }
            else if (id == 1)
            {
                arrow.gameObject.SetActive(true);
                waiticon.gameObject.SetActive(false);
                layoutElement.minHeight = normalHeight;
            }
            else if (id == 2)
            {
                arrow.gameObject.SetActive(false);
                waiticon.gameObject.SetActive(true);
                layoutElement.minHeight = refershHeight;
                scrollerCtrl.scroller.ScrollRect.content.localPosition += Vector3.up * 50;
            }
            else if (id == 3)
            {
                arrow.gameObject.SetActive(false);
                waiticon.gameObject.SetActive(false);
                LeanTween.value(refershHeight, normalHeight, 0.3f).setOnUpdate(v => layoutElement.minHeight = v);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(pullDownRefershRect);
        }


    }
}
