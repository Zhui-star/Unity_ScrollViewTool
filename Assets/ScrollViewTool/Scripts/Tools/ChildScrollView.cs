using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace yoyohan
{

    public class ChildScrollView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        /// <summary>
        /// 外层被拦截需要正常拖动的ScrollRect
        /// </summary>
        public ScrollRect anotherScrollRect;
        private ScrollRect thisScrollRect;
        public PageDragTool PageDragTool;

        void Start()
        {
            thisScrollRect = GetComponent<ScrollRect>();
            if (anotherScrollRect == null)
                anotherScrollRect = GetComponentsInParent<ScrollRect>()[1];
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            anotherScrollRect.OnBeginDrag(eventData);
            if (PageDragTool!=null)
            {
                PageDragTool.OnBeginDrag(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            anotherScrollRect.OnDrag(eventData);
            float angle = Vector2.Angle(eventData.delta, Vector2.up);
            //判断拖动方向，防止水平与垂直方向同时响应导致的拖动时整个界面都会动
            if (angle > 45f && angle < 135f)
            {
                thisScrollRect.enabled = false;
                anotherScrollRect.enabled = true;
            }
            else
            {
                anotherScrollRect.enabled = false;
                thisScrollRect.enabled = true;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            anotherScrollRect.OnEndDrag(eventData);
            if (PageDragTool != null)
            {
                PageDragTool.OnEndDrag(eventData);
            }
            //拖动结束后调用外层ScrollView的回弹效果
            //if (anotherScrollRect.enabled)
            //    anotherScrollRect.GetComponent<ChildScrollView>().ChildScrollEndDrag(eventData);
            anotherScrollRect.enabled = true;
            thisScrollRect.enabled = true;
        }
    }
}