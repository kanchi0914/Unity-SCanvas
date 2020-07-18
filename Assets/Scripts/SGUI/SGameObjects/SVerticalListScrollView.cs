using System;
using System.Collections.Generic;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace SGUI.SGameObjects
{
    class SVerticalListScrollView : SGameObject
    {

        private int itemSize = 100;

        private bool isItemAutoSizing = false;

        private int visibleItemCount;

        private int minContentAreaSize = 0;
        private int paddingLeft = 0;
        private int paddingRight = 0;
        private int paddingTop = 0;
        private int paddingBottom = 0;
        private int spacing = 0;
        public List<SGameObject> childrenObjects = new List<SGameObject> ();

        public GameObject ContentArea { get; private set; }

        public SVerticalListScrollView (
            SGameObject parent,
            string name,
            int itemSize = 100,
            int minContentAreaSize = 0
            // ScrollbarVisibility scrollbarVisibility = ScrollbarVisibility.AutoHide,
            // MovementType movementType = MovementType.Elastic
        ) : this (parent, name)
        {
            // var scrollView = gameObject.GetComponentInChildren<ScrollRect> ();
            // scrollView.verticalScrollbarVisibility = scrollbarVisibility;
            // scrollView.movementType = movementType;

            if (minContentAreaSize > 0) this.minContentAreaSize = minContentAreaSize;
        }

        public SVerticalListScrollView (
            SGameObject parent,
            string name,
            int visibleItemCount = 0
            // ScrollbarVisibility scrollbarVisibility = ScrollbarVisibility.AutoHide,
            // MovementType movementType = MovementType.Elastic
        ) : this (parent, name)
        {
            // var scrollView = gameObject.GetComponentInChildren<ScrollRect> ();
            // scrollView.verticalScrollbarVisibility = scrollbarVisibility;
            // scrollView.movementType = movementType;

            if (visibleItemCount > 0)
            {
                this.itemSize = (int) (ContentArea.GetComponent<RectTransform> ().sizeDelta.y / visibleItemCount);
                this.isItemAutoSizing = true;
                this.visibleItemCount = visibleItemCount;
            }
        }

        public SVerticalListScrollView (
            SGameObject parent,
            string name
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateScrollView (parent.GameObject, name);
            }))
        {
            ContentArea = this.GameObject.transform.FindDeep ("Content");
            var layout = ContentArea.GetComponent<VerticalLayoutGroup> ();
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childScaleHeight = false;
            layout.childScaleWidth = false;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;
        }

        private void SetLayout ()
        {
            SetSpacing ();
            SetPadding ();
            var rect = ContentArea.GetComponent<RectTransform> ();

            var contentFieldSize = itemSize * childrenObjects.Count + ((childrenObjects.Count - 1) * spacing) + paddingTop + paddingBottom;
            rect.sizeDelta = new Vector2 (0, contentFieldSize);

            foreach (SGameObject sg in childrenObjects)
            {
                var child = sg.GameObject;
                var layout = child.GetComponent<LayoutElement> ();
                if (!layout) layout = child.AddComponent<LayoutElement> ();
                layout.minHeight = this.itemSize;
                layout.flexibleWidth = 1f;
                child.transform.SetParent (ContentArea.transform, false);
            }
            if (contentFieldSize < minContentAreaSize)
            {
                rect.sizeDelta = new Vector2 (0, minContentAreaSize);
            }
        }

        public SVerticalListScrollView SetVerticalListItems (
            List<SGameObject> sGameObjects,
            int rowSize = 10,
            TextAnchor textAnchor = TextAnchor.UpperLeft
        )
        {
            this.childrenObjects = sGameObjects;
            SetLayout ();
            // SetListLayout (textAnchor);
            // SetChildrenSize (1);
            sGameObjects.ForEach (
                s => { s.SetParentSGameObject (this); });
            return this;
        }

        public void AddChild (SGameObject sGameObject)
        {
            childrenObjects.Add (sGameObject);
            SetLayout ();
        }

        public void SetItemSize (int size)
        {
            this.itemSize = size;
            this.isItemAutoSizing = false;
        }

        public void SetVisibleItemCount (int count)
        {
            this.isItemAutoSizing = true;
        }

        public SVerticalListScrollView SetPadding (int left, int right, int top, int bottom)
        {
            paddingLeft = left;
            paddingRight = right;
            paddingTop = top;
            paddingBottom = bottom;
            SetPadding ();
            return this;
        }

        public SVerticalListScrollView SetScrollbarVisibility (ScrollbarVisibility scrollbarVisibility)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect> ();
            scrollView.verticalScrollbarVisibility = scrollbarVisibility;
            return this;
        }

        public SVerticalListScrollView SetMovementType (MovementType movementType)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect> ();
            scrollView.movementType = movementType;
            return this;
        }

        public SVerticalListScrollView SetSpacing (int spacing)
        {
            this.spacing = spacing;
            if (isItemAutoSizing) itemSize = 10;
            var rectSize = ContentArea.GetComponent<RectTransform> ().sizeDelta;
            if (isItemAutoSizing) itemSize = (int) ((rectSize.y - spacing * (visibleItemCount + 2)) / visibleItemCount);
            SetSpacing ();
            return this;
        }

        private void SetPadding ()
        {
            var layout = gameObject.GetComponentInChildren<VerticalLayoutGroup> ();
            layout.padding.left = paddingLeft;
            layout.padding.right = paddingRight;
            layout.padding.top = paddingTop;
            layout.padding.bottom = paddingBottom;
        }

        private void SetSpacing ()
        {
            var layout = gameObject.GetComponentInChildren<VerticalLayoutGroup> ();
            layout.spacing = this.spacing;
        }
    }
}