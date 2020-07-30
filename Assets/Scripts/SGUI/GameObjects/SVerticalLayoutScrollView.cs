using System;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using SGUI.Base;
using SGUI.GameObjects.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;
using static UnityEngine.UI.ScrollRect;

namespace SGUI.GameObjects
{
    class SVerticalLayoutScrollView : SGameObject, ILayoutObject
    {
        private int constantItemHeight = 0;

        private int paddingLeft = 0;
        private int paddingRight = 0;
        private int paddingTop = 0;
        private int paddingBottom = 0;
        private int spacing = 0;

        public List<SGameObject> childrenObjects = new List<SGameObject>();

        private int scrollbarWidth = 0;

        private Vector2 contentAreaRectSize;

        public GameObject ContentArea { get; private set; }

        public SVerticalLayoutScrollView(
            SGameObject parent,
            string name = "SVerticalLayoutScrollView",
            int constantItemHeight = 0,
            bool isAutoSizingWidth = true
        ) : base(parent, name,
            new Func<GameObject>(() =>
           {
               return UIFactory.CreateVerticalScrollView(parent.GameObject, name);
           }))
        {
            ContentArea = this.GameObject.transform.FindDeep("Content");
            var layout = ContentArea.GetComponent<VerticalLayoutGroup>();

            layout.childControlWidth = isAutoSizingWidth;
            layout.childControlHeight = (constantItemHeight > 0);
            layout.childScaleHeight = false;
            layout.childScaleWidth = false;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = isAutoSizingWidth;

            scrollbarWidth = (int)gameObject.transform.FindDeep("Scrollbar Vertical").GetComponent<RectTransform>().sizeDelta.x;

            var contentAreaRect = ContentArea.GetComponent<RectTransform>();
            contentAreaRect.sizeDelta = new Vector2(-scrollbarWidth, RectSize.y);

            var csfitter = ContentArea.AddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            csfitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            this.constantItemHeight = constantItemHeight;
        }

        private void SetLayout()
        {
            SetSpacing();
            SetPadding();
        }

        public void AddItem(SGameObject sGameObject)
        {
            childrenObjects.Add(sGameObject);
            sGameObject.GameObject.transform.SetParent(ContentArea.transform, false);
            var layout = sGameObject.GameObject.TryAddComponent<LayoutElement>();
            if (constantItemHeight > 0)
            {
                layout.minHeight = constantItemHeight;
                layout.preferredHeight = constantItemHeight;
            }
            else
            {
                layout.minHeight = sGameObject.RectSize.y;
                layout.preferredHeight = sGameObject.RectSize.y;
            }

            SetLayout();
        }

        public SVerticalLayoutScrollView SetPadding(int left, int right, int top, int bottom)
        {
            paddingLeft = left;
            paddingRight = right;
            paddingTop = top;
            paddingBottom = bottom;
            SetPadding();
            return this;
        }

        public SVerticalLayoutScrollView SetScrollbarVisibility(ScrollbarVisibility scrollbarVisibility)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.verticalScrollbarVisibility = scrollbarVisibility;
            return this;
        }

        public SVerticalLayoutScrollView SetMovementType(MovementType movementType)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.movementType = movementType;
            return this;
        }

        private void SetPadding()
        {
            var layout = gameObject.GetComponentInChildren<VerticalLayoutGroup>();
            layout.padding.left = paddingLeft;
            layout.padding.right = paddingRight;
            layout.padding.top = paddingTop;
            layout.padding.bottom = paddingBottom;
        }

        private void SetSpacing()
        {
            var layout = gameObject.GetComponentInChildren<VerticalLayoutGroup>();
            layout.spacing = this.spacing;
        }

        #region  RequiredMethods

        public new SVerticalLayoutScrollView SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            return base.SetBackGroundColor(colorType, alpha) as SVerticalLayoutScrollView;
        }

        public new SVerticalLayoutScrollView SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SVerticalLayoutScrollView;
        }

        public new SVerticalLayoutScrollView SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SVerticalLayoutScrollView;
        }

        public new SVerticalLayoutScrollView SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as SVerticalLayoutScrollView;
        }

        public new SVerticalLayoutScrollView SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as SVerticalLayoutScrollView;
        }

        public new SVerticalLayoutScrollView SetLocalPos(float width, float height)
        {
            return base.SetLocalPos(width, height) as SVerticalLayoutScrollView;
        }

        public new SVerticalLayoutScrollView SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SVerticalLayoutScrollView;
        }

        public new SVerticalLayoutScrollView SetAnchorType(AnchorType anchorType)
        {
            return base.SetAnchorType(anchorType) as SVerticalLayoutScrollView;
        }

        #endregion
    }

}