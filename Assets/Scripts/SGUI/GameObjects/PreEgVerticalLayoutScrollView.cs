using System;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static EGUI.Base.Utils;
using static UnityEngine.UI.ScrollRect;

namespace EGUI.GameObjects
{
    class PreEgVerticalLayoutScrollView : EGGameObject, ILayoutObject
    {
        private int constantItemHeight = 0;
        private readonly VerticalLayoutGroup layoutComponent;

        public int PaddingLeft => layoutComponent.padding.left;
        public int PaddingRight => layoutComponent.padding.right;
        public int PaddingTop => layoutComponent.padding.top;
        public int PaddingBottom => layoutComponent.padding.bottom;
        public float Spacing  => layoutComponent.spacing;
        public List<EGGameObject> childrenObjects = new List<EGGameObject>();

        private int scrollbarWidth = 0;

        private Vector2 contentAreaRectSize;

        public GameObject ContentArea { get; private set; }

        public PreEgVerticalLayoutScrollView(
            EGGameObject parent,
            int constantItemHeight = 0,
            bool isAutoSizingWidth = true,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "SVerticalLayoutScrollView"
        ) : base
        (
            parent,
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            name,
            () => UIFactory.CreateVerticalScrollView(parent.GameObject, name)
        )
        {
            ContentArea = GameObject.transform.FindDeep("Content");
            layoutComponent = ContentArea.GetComponent<VerticalLayoutGroup>();

            layoutComponent.childControlWidth = isAutoSizingWidth;
            layoutComponent.childControlHeight = (constantItemHeight > 0);
            layoutComponent.childScaleHeight = false;
            layoutComponent.childScaleWidth = false;
            layoutComponent.childForceExpandHeight = false;
            layoutComponent.childForceExpandWidth = isAutoSizingWidth;

            scrollbarWidth = (int) gameObject.transform.FindDeep("Scrollbar Vertical").GetComponent<RectTransform>()
                .sizeDelta.x;

            var contentAreaRect = ContentArea.GetComponent<RectTransform>();
            contentAreaRect.sizeDelta = new Vector2(-scrollbarWidth, RectSize.y);

            var csfitter = ContentArea.AddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            csfitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            this.constantItemHeight = constantItemHeight;
        }

        public void AddItem(EGGameObject egGameObject)
        {
            egGameObject.GameObject.transform.SetParent(ContentArea.transform, false);
            var layout = egGameObject.GameObject.TryAddComponent<LayoutElement>();
            if (constantItemHeight > 0)
            {
                layout.minHeight = constantItemHeight;
                layout.preferredHeight = constantItemHeight;
            }
            else
            {
                layout.minHeight = egGameObject.RectSize.y;
                layout.preferredHeight = egGameObject.RectSize.y;
            }
        }
        
        public PreEgVerticalLayoutScrollView SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
        {
            layoutComponent.padding.left = left ?? PaddingLeft;
            layoutComponent.padding.right = right ?? PaddingRight;
            layoutComponent.padding.top = top ?? PaddingTop;
            layoutComponent.padding.bottom = bottom ?? PaddingBottom;
            return this;
        }

        public PreEgVerticalLayoutScrollView SetSpacing(float spacing)
        {
            layoutComponent.spacing = spacing;
            return this;
        }
        
        public PreEgVerticalLayoutScrollView SetScrollbarVisibility(ScrollbarVisibility scrollbarVisibility)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.verticalScrollbarVisibility = scrollbarVisibility;
            return this;
        }

        public PreEgVerticalLayoutScrollView SetMovementType(MovementType movementType)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.movementType = movementType;
            return this;
        }

    }
}