using System;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace EGUI.GameObjects
{
    public class EGHorizontalGridLayoutScrollView : EgImage
    {
        private int columnSize = 3;

        private int scrollbarHeight = 0;
        public EGGridLayoutView ContentArea { get; private set; }

        public EGHorizontalGridLayoutScrollView(
            EGGameObject parent,
            int rowSize = 3,
            int itemWidth = 100,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "EGHorizontalGridLayoutScrollView"
        ) : base
        (
            parent,
            null,
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            false,
            name
        )
        {
            SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSize(200, 200)
                .SetColor(ColorType.White, 0.4f);
            var viewport = new EgImage(this, name: "Viewport").SetMiddleCenterAnchor();
            ContentArea = new EGGridLayoutView(viewport, RectTransform.Axis.Horizontal, rowCount:rowSize, 
                constantItemWidth:itemWidth).SetMiddleCenterAnchor() as EGGridLayoutView;

            viewport.RectTransform.anchorMin = Vector2.zero;
            viewport.RectTransform.anchorMax = Vector2.one;
            viewport.RectTransform.sizeDelta = Vector2.zero;
            viewport.RectTransform.pivot = Vector2.up;

            ContentArea.RectTransform.anchorMin = Vector2.up;
            ContentArea.RectTransform.anchorMax = Vector2.one;
            ContentArea.RectTransform.sizeDelta = new Vector2(0, 180);
            ContentArea.RectTransform.pivot = Vector2.up;

            ScrollRect scrollRect = gameObject.AddComponent<ScrollRect>();
            scrollRect.content = ContentArea.RectTransform;
            scrollRect.viewport = viewport.RectTransform;

            Mask viewportMask = viewport.GameObject.AddComponent<Mask>();
            viewportMask.showMaskGraphic = false;

            var scrollBar = new EGScrollBar(this, name: "Scrollbar Horizontal").SetMiddleCenterAnchor();
            scrollBar.RectTransform.anchorMin = Vector2.zero;
            scrollBar.RectTransform.anchorMax = Vector2.right;
            scrollBar.RectTransform.pivot = Vector2.zero;
            scrollBar.RectTransform.sizeDelta = new Vector2(0, 20);

            scrollRect.horizontalScrollbar = scrollBar.GameObject.GetComponent<Scrollbar>();
            scrollRect.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            scrollRect.horizontalScrollbarSpacing = -3;
            scrollRect.verticalScrollbarSpacing = -3;

            scrollRect.vertical = false;
            scrollRect.horizontal = true;
            scrollBar.SetActive(true);

            SetScrollbarVisibility(ScrollbarVisibility.Permanent);
            
            var csfitter = ContentArea.GameObject.TryAddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            csfitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        }

        public void SetScrollbarVisibility(ScrollbarVisibility scrollbarVisibility)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.horizontalScrollbarVisibility = scrollbarVisibility;
        }

        public void SetMovementType(MovementType movementType)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.movementType = movementType;
        }

        public void AddItem(EGGameObject egGameObject)
        {
            ContentArea?.AddItem(egGameObject);
        }

        public EGHorizontalGridLayoutScrollView SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
        {
            ContentArea.SetPadding(left, right, top, bottom);
            return this;
        }

        public EGHorizontalGridLayoutScrollView SetSpacing(float? spacingX = null, float? spacingY = null)
        {
            ContentArea.SetSpacing(spacingX, spacingY);
            return this;
        }
    }
}