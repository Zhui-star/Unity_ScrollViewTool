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
    /// 创建时间：2020-01-20 08:40:04
    /// </summary>
    public class PDFHeadScrollerCtrl : ScrollerCtrlBase
    {
        public EnhancedScrollerCellView HeadCellView;
        public EnhancedScrollerCellView PDFCellView;

        private bool isHead
        {
            get
            {
                return HeadCellView != null;
            }
        }
        private bool isPDF
        {
            get
            {
                return PDFCellView != null;
            }
        }

        protected override void Start()
        {
            base.Start();
        }

        public override ScrollerCtrlBase setDataList(List<CellDataBase> _lisData)
        {
            lisData.Clear();
            AddData();
            lisData.AddRange(_lisData);
            return this;
        }

        void AddData()
        {
            if (isPDF)
            {
                int count = isGridModel ? numberOfCellsPerRow : 1;
                for (int i = 0; i < count; i++)
                {
                    lisData.Add(new PDFData());
                }
            }
            if (isHead)
            {
                int count = isGridModel ? numberOfCellsPerRow : 1;
                for (int i = 0; i < count; i++)
                {
                    lisData.Add(new HeadData());
                }
            }
        }

        public override EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            int dataID = isGridModel ? dataIndex * numberOfCellsPerRow : dataIndex;

            if (lisData[dataID] is PDFData)
            {
                return PDFCellView;
            }
            if (lisData[dataID] is HeadData)
            {
                return HeadCellView;
            }

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

        public override float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            int dataID = isGridModel ? dataIndex * numberOfCellsPerRow : dataIndex;

            if (lisData[dataID] is PDFData)
            {
                return getCellSize(PDFCellView.transform);
            }
            if (lisData[dataID] is HeadData)
            {
                return getCellSize(HeadCellView.transform);
            }

            return cellSize;
        }


    }


    public class PDFData : CellDataBase
    {

    }

    public class HeadData : CellDataBase
    {

    }

}
