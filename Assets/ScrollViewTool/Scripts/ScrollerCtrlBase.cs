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
    public class ScrollerCtrlBase : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public EnhancedScroller scroller;
        public CellViewBase cellViewPrefab;

        private float _cellSize = -1;
        public float cellSize
        {
            get
            {
                if (_cellSize == -1)
                {
                    _cellSize = getCellSize(cellViewPrefab.transform);
                }
                return _cellSize;
            }
        }

        private int _pageCount = -1;
        /// <summary>
        /// 一页包含几个格子 用于翻页
        /// </summary>
        public int pageCount
        {
            get
            {
                if (_pageCount == -1)
                {
                    _pageCount = scroller.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical ? (int)scroller.ScrollRectSize / (int)cellSize : (int)scroller.ScrollRectSize / (int)cellSize;
                }
                return _pageCount;
            }
        }

        [Tooltip("格子模式勾选")]
        public bool isGridModel = false;
        /// <summary>
        /// 如果是格子模式 该值有用
        /// </summary>
        private int _numberOfCellsPerRow = -1;
        public int numberOfCellsPerRow
        {
            get
            {
                if (_numberOfCellsPerRow == -1)
                {
                    _numberOfCellsPerRow = cellViewPrefab.gridCount;
                }
                return _numberOfCellsPerRow;
            }
        }

        [Space(20)]
        public List<CellDataBase> lisData;

        private bool isReloaded = false;


        #region 数据相关操作
        public List<CellDataBase> RemoveAtID(int id)
        {
            if (id >= 0 && id <= lisData.Count - 1)
            {
                lisData.RemoveAt(id);
            }

            return lisData;
        }

        public CellDataBase GetDataByID(int id)
        {
            if (id >= 0 && id <= lisData.Count - 1)
            {
                return lisData[id];
            }

            return null;
        }
        #endregion

        protected float getCellSize(Transform tr)
        {
            RectTransform rect = tr as RectTransform;
            return scroller.scrollDirection == EnhancedScroller.ScrollDirectionEnum.Vertical ? rect.sizeDelta.y : rect.sizeDelta.x;
        }


        protected virtual void Start()
        {
            if (cellViewPrefab != null)
                cellViewPrefab.mGameObject.SetActive(false);
        }


        private bool isInit = false;

        private void InitCtrl()
        {
            scroller.Delegate = this;
            scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
            isInit = true;
        }

        public virtual ScrollerCtrlBase setDataList(List<CellDataBase> _lisData)
        {
            this.lisData = _lisData;
            return this;
        }

        /// <summary>
        /// Scroller的启动方法  注意：调用之前先setDataList
        /// </summary>
        public virtual ScrollerCtrlBase ReloadData(float scrollPositionFactor = 0)
        {
            if (isInit == false)
                InitCtrl();

            scroller.ReloadData(scrollPositionFactor);
            isReloaded = true;
            return this;
        }

        public virtual ScrollerCtrlBase RefershData()
        {
            if (isReloaded==false)
            {
                ReloadData();
                return this;
            }
            scroller.RefreshActiveCellViews();
            return this;
        }


        public virtual EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            int dataID = isGridModel ? dataIndex * numberOfCellsPerRow : dataIndex;


            CellViewBase cellView = scroller.GetCellView(cellViewPrefab) as CellViewBase;
            if (isGridModel == false)
            {
                cellView.name = "Cell " + dataID;
                cellView.setDataIndex(dataID);
            }
            else
            {
                cellView.name = "Cell " + dataID + " to " + (dataID + numberOfCellsPerRow - 1);
                cellView.setDataIndex(dataID);
            }
            return cellView;
        }

        public virtual float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return cellSize;
        }

        public virtual int GetNumberOfCells(EnhancedScroller scroller)
        {
            if (isGridModel == false)
            {
                return lisData.Count;
            }
            else
            {
                return Mathf.CeilToInt((float)lisData.Count / (float)numberOfCellsPerRow);
            }
        }

        public virtual void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            cellView.RefreshCellView();
        }


        public void OnUpButtonClick()
        {
            if (scroller.IsTweening == true)
                return;

            scroller.JumpToDataIndex(scroller.StartDataIndex - pageCount, 0, 0.05f, true, EnhancedScroller.TweenType.linear, 0.5f);
        }

        public void OnDownButtonClick()
        {
            if (scroller.IsTweening == true)
                return;

            scroller.JumpToDataIndex(scroller.StartDataIndex + pageCount, 0, 0.05f, true, EnhancedScroller.TweenType.linear, 0.5f);
        }






    }
}