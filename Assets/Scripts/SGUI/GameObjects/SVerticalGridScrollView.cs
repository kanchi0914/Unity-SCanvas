using System;
using System.Collections.Generic;
using SGUI.Base;
using SGUI.GameObjects.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;
using UniRx;
using static UnityEngine.UI.ScrollRect;

namespace SGUI.GameObjects
{
    class SVerticalGridScrollView : SGameObject, ILayoutObject
    {

        public float ItemHeight
        {
            get; private set;
        }

        public int ItemWidth
        {
            get
            {
                return (int)((contentAreaRectSize.x - (gridLayout.padding.left + gridLayout.padding.right)
                    - gridLayout.spacing.x * (columnSize - 1)) / columnSize);
            }
        }

        private bool isItemAutoSizing = false;

        private int columnSize = 1;

        private int minContentAreaSize = 0;

        private float scrollbarWidth = 0;

        private int spacingX = 0;
        private int spacingY = 0;

        public List<SGameObject> childrenObjects = new List<SGameObject>();

        public GameObject ContentArea { get; private set; }

        public GridLayoutGroup gridLayout;

        private Vector2 contentAreaRectSize;

        public SVerticalGridScrollView(
            SGameObject parent,
            int itemHeight = 100,
            int columnSize = 2
        ) : base(parent, "SVerticalGridScrollView",
            new Func<GameObject>(() =>
           {
               return UIFactory.CreateVerticalGridLayoutScrollView(parent.GameObject, "SVerticalGridScrollView");
           }))
        {
            scrollbarWidth = gameObject.transform.FindDeep("Scrollbar Vertical").GetComponent<RectTransform>().sizeDelta.x;
            ContentArea = this.GameObject.transform.FindDeep("Content");
            ContentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(-scrollbarWidth, RectSize.y);

            var csfitter = ContentArea.AddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            csfitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            gridLayout = ContentArea.GetComponent<GridLayoutGroup>();

            ItemHeight = itemHeight;
            this.columnSize = columnSize;

            gridLayout.childAlignment = TextAnchor.UpperLeft;
            SetScrollbarVisibility(ScrollbarVisibility.Permanent);
            SetMovementType(MovementType.Clamped);

            gameObject.ObserveEveryValueChanged(_ => RectSize).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.spacing.x).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.spacing.y).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.padding).Subscribe(_ => UpdateCellSize());
            
            UpdateCellSize();
        }

        private void UpdateCellSize()
        {
            contentAreaRectSize = new Vector2(ApparentRectSize.x - scrollbarWidth, ApparentRectSize.y);
            gridLayout.cellSize = new Vector2(ItemWidth, ItemHeight);
        }

        public void AddItem(SGameObject sGameObject)
        {
            childrenObjects.Add(sGameObject);
            sGameObject.GameObject.transform.SetParent(ContentArea.transform, false);
        }

        public SVerticalGridScrollView SetItemHeight(float itemHeight)
        {
            ItemHeight = itemHeight;
            return this;
        }

        public SVerticalGridScrollView SetScrollbarVisibility(ScrollbarVisibility scrollbarVisibility)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.verticalScrollbarVisibility = scrollbarVisibility;
            return this;
        }

        public SVerticalGridScrollView SetMovementType(MovementType movementType)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.movementType = movementType;
            return this;
        }

        public SVerticalGridScrollView SetPadding(int left, int right, int top, int bottom)
        {
            gridLayout.padding.left = left;
            gridLayout.padding.right = right;
            gridLayout.padding.top = top;
            gridLayout.padding.bottom = bottom;
            return this;
        }

        public SVerticalGridScrollView SetSpacing(float spacingX, float spacingY)
        {
            gridLayout.spacing = new Vector2(spacingX, spacingY);
            return this;
        }

        //private void SetPadding()
        //{
        //    gridLayout.padding.left = paddingLeft;
        //    gridLayout.padding.right = paddingRight;
        //    gridLayout.padding.top = paddingTop;
        //    gridLayout.padding.bottom = paddingBottom;
        //    //UpdateCellSize();
        //}

        //private void SetSpacing()
        //{
        //    gridLayout.spacing = new Vector2(spacingX, spacingY);
        //    //UpdateCellSize();
        //}

        #region  RequiredMethods

        public new SVerticalGridScrollView SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            return base.SetBackGroundColor(colorType, alpha) as SVerticalGridScrollView;
        }

        public new SVerticalGridScrollView SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SVerticalGridScrollView;
        }

        public new SVerticalGridScrollView SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SVerticalGridScrollView;
        }

        public new SVerticalGridScrollView SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as SVerticalGridScrollView;
        }

        public new SVerticalGridScrollView SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as SVerticalGridScrollView;
        }

        public new SVerticalGridScrollView SetLocalPos(float width, float height)
        {
            return base.SetLocalPos(width, height) as SVerticalGridScrollView;
        }

        public new SVerticalGridScrollView SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SVerticalGridScrollView;
        }

        public new SVerticalGridScrollView SetAnchorType(AnchorType anchorType)
        {
            return base.SetAnchorType(anchorType) as SVerticalGridScrollView;
        }

        #endregion
    }
}