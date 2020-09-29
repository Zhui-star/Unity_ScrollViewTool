using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yoyohan.EnhancedUI.EnhancedScroller;


namespace yoyohan
{
    /// <summary>
    /// 描述：
    /// 功能：
    /// 作者：yoyohan
    /// 创建时间：2019-08-17 10:37:01
    /// </summary>
    public class CellViewBase : EnhancedScrollerCellView
    {
        private GameObject __mGameObject;
        private Transform __mTransform;
        public GameObject mGameObject { get { if (__mGameObject == null) __mGameObject = this.gameObject; return __mGameObject; } }
        public Transform mTransform { get { if (__mTransform == null) __mTransform = this.transform; return __mTransform; } }

        /// <summary>
        /// 在父类的active的基础上 再根据data是否存在 判断得出的mActive 
        /// </summary>
        public bool mActive
        {
            get
            {
                if (isGridModel)
                {
                    return active;
                }
                else
                {
                    return active ? mData != null : false;
                }
            }
        }
        public int mDataIndex;
        public CellDataBase mData
        {
            get
            {
                return scrollerCtrl.GetDataByID(mDataIndex);
            }
        }
        public ScrollerCtrlBase scrollerCtrl;
        public bool isGridModel = false;
        public int gridCount = 4;
        public CellGridBase gridPrefab;
        public bool needInstantiate = true;
        public List<CellGridBase> lisCellGrid = new List<CellGridBase>();

        void Awake()
        {
            if (isGridModel && scrollerCtrl.cellViewPrefab != this)
            {
                if (needInstantiate == true)
                {
                    lisCellGrid.Add(gridPrefab);
                    gridPrefab.InitGrid(scrollerCtrl, this, 0);
                    for (int i = 1; i < gridCount; i++)
                    {
                        CellGridBase item = Instantiate(gridPrefab.mGameObject, mTransform).GetComponent<CellGridBase>();
                        item.InitGrid(scrollerCtrl, this, i);
                        lisCellGrid.Add(item);
                    }
                }
                else
                {
                    for (int i = 0; i < lisCellGrid.Count; i++)
                    {
                        lisCellGrid[i].InitGrid(scrollerCtrl, this, i);
                    }
                }
            }
        }

        public virtual void setDataIndex(int dataIndex)
        {
            mDataIndex = dataIndex;
        }

        public override void RefreshCellView()
        {
            if (isGridModel)
            {
                for (int i = 0; i < lisCellGrid.Count; i++)
                {
                    lisCellGrid[i].RefreshCellGrid();
                }
            }
            else
            {
                mGameObject.SetActive(mActive);
            }
        }

    }
}