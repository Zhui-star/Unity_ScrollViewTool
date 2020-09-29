using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace yoyohan.ScrollViewToolDemo
{
    /// <summary>
    /// 描述：
    /// 功能：
    /// 作者：yoyohan
    /// 创建时间：2019-08-17 11:03:21
    /// </summary>
    public class BtnCellView : CellViewBase
    {
        public static Action<BtnCellView> onCellViewRefersh;
        public static Action<BtnCellView> onCellViewClick;
        public Text titleText;

        public BtnCellData mBtnCellData
        {
            get
            {
                return mData.toOtherType<BtnCellData>();
            }
        }


        public override void RefreshCellView()
        {
            base.RefreshCellView();

            if (onCellViewRefersh != null)
                onCellViewRefersh(this);
            if (!active)
                return;

            titleText.text = mBtnCellData.pathRoot;
        }

        public void OnBtnClick()
        {
            if (onCellViewClick != null)
                onCellViewClick(this);
        }


    }
}