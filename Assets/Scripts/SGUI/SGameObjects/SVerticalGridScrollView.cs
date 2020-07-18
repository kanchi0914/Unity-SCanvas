using System.Dynamic;
using System;
using System.Collections.Generic;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace SGUI.SGameObjects
{
    class SVerticalGridScrollView : SGameObject
    {

        public int ItemHeight
        {
            get
            {
                return (int) ((contentAreaRectSize.y - spacing * (rowSize + 2)) / rowSize);
            }
        }

        public int ItemWidth
        {
            get
            {
                var a = (int) ((contentAreaRectSize.x - spacing * (columnSize + 2)) / columnSize);
                return (int) ((contentAreaRectSize.x - spacing * (columnSize + 2)) / columnSize);
            }
        }

        // private int itemWidth = 100;

        // private int itemHeight;

        private bool isItemAutoSizing = false;

        private int rowSize;

        private int columnSize;

        private int minContentAreaSize = 0;
        private int paddingLeft = 0;
        private int paddingRight = 0;
        private int paddingTop = 0;
        private int paddingBottom = 0;
        private int spacing = 0;
        public List<SGameObject> childrenObjects = new List<SGameObject> ();

        public GameObject ContentArea { get; private set; }

        public GridLayoutGroup gridLayout;

        private Vector2 contentAreaRectSize;

        // public SVerticalGridScrollView (
        //     SGameObject parent,
        //     string name,
        //     int itemSize = 100,
        //     int minContentAreaSize = 0
        // ) : this (parent, name)
        // {
        //     if (minContentAreaSize > 0) this.minContentAreaSize = minContentAreaSize;
        // }

        public SVerticalGridScrollView (
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

        public SVerticalGridScrollView (
            SGameObject parent,
            string name
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateVerticalGridLayoutScrollView (parent.GameObject, name);
            }))
        {
            ContentArea = this.GameObject.transform.FindDeep ("Content");
            contentAreaRectSize = new Vector2(this.RectSize.x, this.RectSize.y);
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
            SetSpacing ();
            SetPadding ();
            if (columnSize > 0)
            {
                Debug.Log(gridLayout.constraint);
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayout.constraintCount = columnSize;
                gridLayout.cellSize = new Vector2(ItemWidth, ItemHeight);
            }

            // foreach (SGameObject sg in childrenObjects)
            // {
            //     var child = sg.GameObject;
            //     var layout = child.GetComponent<LayoutElement> ();
            //     if (!layout) layout = child.AddComponent<LayoutElement> ();
            //     layout.minHeight = this.itemSize;
            //     layout.flexibleWidth = 1f;
            //     child.transform.SetParent (ContentArea.transform, false);
            // }
            // if (contentFieldSize < minContentAreaSize)
            // {
            //     rect.sizeDelta = new Vector2 (0, minContentAreaSize);
            // }
        }

        public void AddChild (SGameObject sGameObject)
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
            //SetItemSizeAuto ();
            return this;
        }

        public SVerticalGridScrollView SetSpacing (int spacing)
        {
            this.spacing = spacing;
            var rectSize = ContentArea.GetComponent<RectTransform> ().sizeDelta;
            SetSpacing ();
            //SetItemSizeAuto ();
            return this;
        }

        // private void SetItemSizeAuto ()
        // {
        //     var rectSize = ContentArea.GetComponent<RectTransform> ().sizeDelta;
        //     itemHeight = (int) ((rectSize.y - spacing * (rowSize + 2)) / rowSize);
        //     itemWidth = (int) ((rectSize.x - spacing * (columnSize + 2)) / columnSize);
        // }

        private void SetPadding ()
        {
            gridLayout.padding.left = paddingLeft;
            gridLayout.padding.right = paddingRight;
            gridLayout.padding.top = paddingTop;
            gridLayout.padding.bottom = paddingBottom;
        }

        private void SetSpacing ()
        {
            //gridLayout.spacing = this.spacing;
        }
    }
}