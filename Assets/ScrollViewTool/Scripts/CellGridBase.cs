using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yoyohan
{
    public class CellGridBase : MonoBehaviour
    {
        private GameObject __mGameObject;
        private Transform __mTransform;
        public GameObject mGameObject { get { if (__mGameObject == null) __mGameObject = this.gameObject; return __mGameObject; } }
        public Transform mTransform { get { if (__mTransform == null) __mTransform = this.transform; return __mTransform; } }

        [HideInInspector]
        public ScrollerCtrlBase mScrollerCtrl;
        [HideInInspector]
        public CellViewBase mCellViewBase;
        [HideInInspector]
        public int mGridIndex;//处于cell下第几个格子的索引

        public CellDataBase mData;
        public int mDataIndex;//真data的索引
        public bool mActive = false;

        public void InitGrid(ScrollerCtrlBase scrollerCtrl, CellViewBase cellViewBase, int gridIndex)
        {
            mScrollerCtrl = scrollerCtrl;
            mCellViewBase = cellViewBase;
            mGridIndex = gridIndex;
        }

        /// <summary>
        /// 必须继承该类的代码
        /// </summary>
        public virtual void RefreshCellGrid()
        {
            mDataIndex = mCellViewBase.dataIndex * mScrollerCtrl.numberOfCellsPerRow + mGridIndex;
            mData = mScrollerCtrl.GetDataByID(mDataIndex);
            mActive = mCellViewBase.mActive ? mData != null : false;
            mGameObject.SetActive(mActive);
        }

    }
}