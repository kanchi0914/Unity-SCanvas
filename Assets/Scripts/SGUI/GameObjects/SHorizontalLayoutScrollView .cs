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
    class SHorizontalLayoutScrollView : SGameObject, ILayoutObject
    {
        private int constantItemWidth = 0;

        private int paddingLeft = 0;
        private int paddingRight = 0;
        private int paddingTop = 0;
        private int paddingBottom = 0;
        private int spacing = 0;

        public List<SGameObject> childrenObjects = new List<SGameObject>();

        private int scrollbarWidth = 0;

        private Vector2 contentAreaRectSize;

        public GameObject ContentArea { get; private set; }

        public SHorizontalLayoutScrollView(
            SGameObject parent,
            string name = "SHorizontalLayoutScrollView",
            int constantItemWidth = 0,
            bool isAutoSizingHeight = true
        ) : base(parent, name,
            new Func<GameObject>(() =>
           {
               return UIFactory.CreateHorizontalScrollView(parent.GameObject, name);
           }))
        {
            ContentArea = this.GameObject.transform.FindDeep("Content");
            var layout = ContentArea.GetComponent<HorizontalLayoutGroup>();

            layout.childControlWidth = (constantItemWidth > 0);
            layout.childControlHeight = isAutoSizingHeight;
            layout.childScaleHeight = false;
            layout.childScaleWidth = false;
            layout.childForceExpandHeight = isAutoSizingHeight;
            layout.childForceExpandWidth = false;

            var scrollbarSize = (int)gameObject.transform.FindDeep("Scrollbar Horizontal").GetComponent<RectTransform>().sizeDelta.y;
            ContentArea = this.GameObject.transform.FindDeep("Content");
            var contentAreaRect = ContentArea.GetComponent<RectTransform>();
            contentAreaRect.sizeDelta = new Vector2(0, RectSize.y - scrollbarSize);

            var csfitter = ContentArea.AddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            csfitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

            this.constantItemWidth = constantItemWidth;
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
            if (constantItemWidth > 0)
            {
                layout.minWidth = constantItemWidth;
            }
            else
            {
                layout.minWidth = sGameObject.RectSize.x;
            }

            SetLayout();
        }

        public SHorizontalLayoutScrollView SetPadding(int left, int right, int top, int bottom)
        {
            paddingLeft = left;
            paddingRight = right;
            paddingTop = top;
            paddingBottom = bottom;
            SetPadding();
            return this;
        }

        public SHorizontalLayoutScrollView SetScrollbarVisibility(ScrollbarVisibility scrollbarVisibility)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.horizontalScrollbarVisibility = scrollbarVisibility;
            return this;
        }

        public SHorizontalLayoutScrollView SetMovementType(MovementType movementType)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.movementType = movementType;
            return this;
        }

        private void SetPadding()
        {
            var layout = gameObject.GetComponentInChildren<HorizontalLayoutGroup>();
            layout.padding.left = paddingLeft;
            layout.padding.right = paddingRight;
            layout.padding.top = paddingTop;
            layout.padding.bottom = paddingBottom;
        }

        private void SetSpacing()
        {
            var layout = gameObject.GetComponentInChildren<HorizontalLayoutGroup>();
            layout.spacing = this.spacing;
        }

        #region  RequiredMethods

        public new SHorizontalLayoutScrollView SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            return base.SetBackGroundColor(colorType, alpha) as SHorizontalLayoutScrollView;
        }

        public new SHorizontalLayoutScrollView SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SHorizontalLayoutScrollView;
        }

        public new SHorizontalLayoutScrollView SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SHorizontalLayoutScrollView;
        }

        public new SHorizontalLayoutScrollView SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as SHorizontalLayoutScrollView;
        }

        public new SHorizontalLayoutScrollView SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as SHorizontalLayoutScrollView;
        }

        public new SHorizontalLayoutScrollView SetLocalPos(float width, float height)
        {
            return base.SetLocalPos(width, height) as SHorizontalLayoutScrollView;
        }

        public new SHorizontalLayoutScrollView SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SHorizontalLayoutScrollView;
        }

        public new SHorizontalLayoutScrollView SetAnchorType(AnchorType anchorType)
        {
            return base.SetAnchorType(anchorType) as SHorizontalLayoutScrollView;
        }

        #endregion
    }

}