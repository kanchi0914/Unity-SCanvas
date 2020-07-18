using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace SGUI.SGameObjects
{
    class SHorizontallGridLayoutScrollView : SGameObject
    {

        public int ItemHeight
        {
            get
            {
                return (int) ((contentAreaRectSize.y - spacingY * (rowSize + 1)) / rowSize);
            }
        }

        public int ItemWidth
        {
            get
            {
                return (int) ((contentAreaRectSize.x - spacingX * (columnSize + 1)) / columnSize);
            }
        }

        private bool isItemAutoSizing = false;

        private int rowSize = 3;

        private int columnSize = 3;

        private int minContentAreaSize = 0;
        private int paddingLeft = 0;
        private int paddingRight = 0;
        private int paddingTop = 0;
        private int paddingBottom = 0;

        private int scrollbarHeight = 0;

        private int spacingX = 0;
        private int spacingY = 0;

        public List<SGameObject> childrenObjects = new List<SGameObject> ();

        public GameObject ContentArea { get; private set; }

        public GridLayoutGroup gridLayout;

        private Vector2 contentAreaRectSize;

        public SHorizontallGridLayoutScrollView (
            SGameObject parent,
            string name,
            int rowSize = 3,
            int columnSize = 3
        ) : this (parent, name)
        {
            this.rowSize = rowSize;
            this.columnSize = columnSize;
            this.isItemAutoSizing = true;
        }

        public SHorizontallGridLayoutScrollView (
            SGameObject parent,
            string name
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateHorizontalGridLayoutScrollView (parent.GameObject, name);
            }))
        {
            scrollbarHeight = (int)gameObject.transform.FindDeep ("Scrollbar Horizontal").GetComponent<RectTransform> ().sizeDelta.y;
            var parentWidth = RectSize.x;
            ContentArea = this.GameObject.transform.FindDeep ("Content");
            var rect = ContentArea.GetComponent<RectTransform> ();

            contentAreaRectSize = new Vector2 (RectSize.x, RectSize.y - scrollbarHeight);

            var csfitter = ContentArea.AddComponent<ContentSizeFitter> ();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            csfitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

            gridLayout = ContentArea.GetComponentInChildren<GridLayoutGroup> ();
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            gridLayout.constraintCount = rowSize;

            SetLayout();
        }

        public void SetScrollbarVisibility (ScrollbarVisibility scrollbarVisibility)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect> ();
            scrollView.horizontalScrollbarVisibility = scrollbarVisibility;
        }

        public void SetMovementType (MovementType movementType)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect> ();
            scrollView.movementType = movementType;
        }

        public void SetLayout ()
        {
            contentAreaRectSize = new Vector2 (RectSize.x, RectSize.y - scrollbarHeight);
            SetSpacing ();
            SetPadding ();
            gridLayout.cellSize = new Vector2 (ItemWidth, ItemHeight);
        }

        public void AddChild (SGameObject sGameObject)
        {
            childrenObjects.Add (sGameObject);
            sGameObject.GameObject.transform.SetParent (ContentArea.transform, false);
            SetLayout ();
        }

        public SHorizontallGridLayoutScrollView SetPadding (int left, int right, int top, int bottom)
        {
            paddingLeft = left;
            paddingRight = right;
            paddingTop = top;
            paddingBottom = bottom;
            SetPadding ();
            return this;
        }

        public SHorizontallGridLayoutScrollView SetSpacing (int spacingX, int spacingY)
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