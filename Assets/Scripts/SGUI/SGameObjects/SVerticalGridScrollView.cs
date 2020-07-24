using System;
using System.Collections.Generic;
using SGUI.Base;
using SGUI.SGameObjects.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace SGUI.SGameObjects
{
    class SVerticalGridScrollView : SGameObject, ILayoutObject
    {

        public int ItemHeight
        {
            get
            {
                return (int) ((contentAreaRectSize.y - spacingY * (rowSize)) / rowSize);
            }
        }

        public int ItemWidth
        {
            get
            {
                return (int) ((contentAreaRectSize.x - (paddingLeft + paddingRight) - spacingX * (columnSize - 1)) / columnSize) ;
            }
        }

        private bool isItemAutoSizing = false;

        private int rowSize;

        private int columnSize;

        private int minContentAreaSize = 0;
        private int paddingLeft = 0;
        private int paddingRight = 0;
        private int paddingTop = 0;
        private int paddingBottom = 0;

        private int scrollbarWidth = 0;

        private int spacingX = 0;
        private int spacingY = 0;

        public List<SGameObject> childrenObjects = new List<SGameObject> ();

        public GameObject ContentArea { get; private set; }

        public GridLayoutGroup gridLayout;

        private Vector2 contentAreaRectSize;

        public SVerticalGridScrollView (
            SGameObject parent,
            string name,
            int rowSize,
            int columnSize
        ) : this (parent, name)
        {
            this.rowSize = rowSize;
            this.columnSize = columnSize;
            this.isItemAutoSizing = true;
        }

        public SVerticalGridScrollView (
            SGameObject parent,
            string name
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateVerticalGridLayoutScrollView (parent.GameObject, name);
            }))
        {
            scrollbarWidth = (int) gameObject.transform.FindDeep ("Scrollbar Vertical").GetComponent<RectTransform> ().sizeDelta.x;
            var parentWidth = RectSize.x;
            ContentArea = this.GameObject.transform.FindDeep ("Content");
            var rect = ContentArea.GetComponent<RectTransform> ();

            var csfitter = ContentArea.AddComponent<ContentSizeFitter> ();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            csfitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            contentAreaRectSize = new Vector2 (RectSize.x - scrollbarWidth, RectSize.y);
            gridLayout = ContentArea.GetComponent<GridLayoutGroup> ();
        }

        public void SetScrollbarVisibility (ScrollbarVisibility scrollbarVisibility)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect> ();
            scrollView.verticalScrollbarVisibility = scrollbarVisibility;
        }

        public void SetMovementType (MovementType movementType)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect> ();
            scrollView.movementType = movementType;
        }

        public void SetLayout ()
        {
            contentAreaRectSize = new Vector2 (RectSize.x - scrollbarWidth, RectSize.y);
            SetSpacing ();
            SetPadding ();
            gridLayout.cellSize = new Vector2 (ItemWidth, ItemHeight);
        }

        public void AddItem (SGameObject sGameObject)
        {
            childrenObjects.Add (sGameObject);
            sGameObject.GameObject.transform.SetParent (ContentArea.transform, false);
            SetLayout ();
        }

        public SVerticalGridScrollView SetPadding (int left, int right, int top, int bottom)
        {
            paddingLeft = left;
            paddingRight = right;
            paddingTop = top;
            paddingBottom = bottom;
            SetPadding ();
            return this;
        }

        public SVerticalGridScrollView SetSpacing (int spacingX, int spacingY)
        {
            this.spacingX = spacingX;
            this.spacingY = spacingY;
            SetSpacing ();
            return this;
        }

        private void SetPadding ()
        {
            gridLayout.padding.left = paddingLeft;
            gridLayout.padding.right = paddingRight;
            gridLayout.padding.top = paddingTop;
            gridLayout.padding.bottom = paddingBottom;
        }

        private void SetSpacing ()
        {
            gridLayout.spacing = new Vector2 (spacingX, spacingY);
        }
    }
}