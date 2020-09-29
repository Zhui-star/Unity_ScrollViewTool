using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yoyohan.EnhancedUI.EnhancedScroller;
using System;

namespace yoyohan.ScrollViewToolDemo
{
    /// <summary>
    /// 描述：
    /// 功能：
    /// 作者：yoyohan
    /// 创建时间：2019-08-17 10:34:11
    /// </summary>
    public class BtnScrollerCtrl : ScrollerCtrlBase
    {
        public Transform role;//按钮被点击的变换图片
        private int curShowTuChe = -1;

        protected override void Start()
        {
            base.Start();
            this.SetBtnDataList();
        }

        public void SetBtnDataList()
        {
            List<CellDataBase> lisBtnCellData = new List<CellDataBase>();

            for (int i = 0; i < 20; i++)
            {
                lisBtnCellData.Add(new BtnCellData() { pathRoot = "path" + i });
            }

            this.setDataList(lisBtnCellData);
            this.ReloadData();

            if (curShowTuChe == -1)
            {
                onCellViewClick((BtnCellView)this.scroller.GetCellViewAtDataIndex(0));
                curShowTuChe = 0;
            }
        }

        void onCellViewClick(BtnCellView btnCellView)
        {
            if (curShowTuChe != btnCellView.dataIndex)
            {
                curShowTuChe = btnCellView.dataIndex;
                SetRole(btnCellView.mTransform);
            }
        }

        void onCellViewRefersh(BtnCellView btnCellView)
        {
            Debug.Log("btnCellView.dataIndex :" + btnCellView.dataIndex + "   curShowTuChe:" + curShowTuChe + "  btnCellView.active:" + btnCellView.active);
            if (btnCellView.dataIndex == curShowTuChe)
            {
                role.gameObject.SetActive(btnCellView.active);
                if (btnCellView.active == true)
                {
                    SetRole(btnCellView.transform);
                }
            }
        }


        void SetRole(Transform transform)
        {
            role.gameObject.SetActive(true);
            role.SetParent(transform, false);
            role.SetAsFirstSibling();
            role.localPosition = Vector3.zero;
        }

        public override void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            base.CellViewVisibilityChanged(cellView);
        }


        private void OnEnable()
        {
            BtnCellView.onCellViewRefersh += onCellViewRefersh;
            BtnCellView.onCellViewClick += onCellViewClick;
        }

        private void OnDisable()
        {
            BtnCellView.onCellViewRefersh -= onCellViewRefersh;
            BtnCellView.onCellViewClick -= onCellViewClick;
        }

    }
}