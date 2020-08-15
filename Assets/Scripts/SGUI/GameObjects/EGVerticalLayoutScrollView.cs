
using Assets.Scripts.Extensions;
using EGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace EGUI.GameObjects
{
    class EGVerticalLayoutScrollView : EgImage
    {
        private int constantItemHeight = -1;

        public EgVerticalLayoutView ContentArea { get; private set; }
        
        public EGScrollBar EgScrollBar { get; private set; }

        public EGVerticalLayoutScrollView(
            EGGameObject parent,
            int constantItemHeight = -1,
            bool isAutoSizingHeight = true,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "EGVerticalLayoutScrollView"
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
            ContentArea = new EgVerticalLayoutView(viewport, 
                isAutoSizingWidth:isAutoSizingHeight, name: "Content").SetMiddleCenterAnchor()
                as EgVerticalLayoutView;

            viewport.RectTransform.anchorMin = Vector2.zero;
            viewport.RectTransform.anchorMax = Vector2.one;
            viewport.RectTransform.sizeDelta = Vector2.zero;
            viewport.RectTransform.pivot = Vector2.up;

            ContentArea.SetHorizontalStretchWithTopPivotAnchor();
            ContentArea.RectTransform.sizeDelta = new Vector2(-20, 0);

            ContentArea.RectTransform.anchorMin = Vector2.up;
            ContentArea.RectTransform.anchorMax = Vector2.one;
            ContentArea.RectTransform.sizeDelta = new Vector2(-20, 0);
            ContentArea.RectTransform.pivot = Vector2.up;

            ScrollRect scrollRect = gameObject.AddComponent<ScrollRect>();
            scrollRect.content = ContentArea.RectTransform;
            scrollRect.viewport = viewport.RectTransform;

            Mask viewportMask = viewport.GameObject.AddComponent<Mask>();
            viewportMask.showMaskGraphic = false;

            EgScrollBar = new EGScrollBar(this, name: "Scrollbar Vertical")
                .SetMiddleCenterAnchor() as EGScrollBar;
            EgScrollBar.ScrollbarComponent.SetDirection(Scrollbar.Direction.BottomToTop, true);
            EgScrollBar.RectTransform.anchorMin = Vector2.right;
            EgScrollBar.RectTransform.anchorMax = Vector2.one;
            EgScrollBar.RectTransform.pivot = Vector2.one;
            EgScrollBar.RectTransform.sizeDelta = new Vector2(20, 0);

            scrollRect.verticalScrollbar = EgScrollBar.ScrollbarComponent;
            scrollRect.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            scrollRect.horizontalScrollbarSpacing = -3;
            scrollRect.verticalScrollbarSpacing = -3;

            scrollRect.vertical = true;
            scrollRect.horizontal = false;
            EgScrollBar.SetActive(true);

            SetScrollbarVisibility(ScrollbarVisibility.Permanent);
            
            var csfitter = ContentArea.GameObject.TryAddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            csfitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            this.constantItemHeight = constantItemHeight;
        }

        public EGVerticalLayoutScrollView AddItem(EGGameObject egGameObject)
        {
            ContentArea.AddItem(egGameObject);
            if (constantItemHeight > 0)
                egGameObject.GameObject.TryAddComponent<LayoutElement>()
                    .minHeight = constantItemHeight;
            return this;
        }

        public EGVerticalLayoutScrollView SetPadding(int? left = null, int? right = null, int? top = null,
            int? bottom = null)
        {
            ContentArea.SetPadding(left, right, top, bottom);
            return this;
        }

        public EGVerticalLayoutScrollView SetSpacing(float spacing)
        {
            ContentArea.SetSpacing(spacing);
            return this;
        }

        public EGVerticalLayoutScrollView SetScrollbarVisibility(ScrollbarVisibility scrollbarVisibility)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.verticalScrollbarVisibility = scrollbarVisibility;
            return this;
        }

        public EGVerticalLayoutScrollView SetMovementType(MovementType movementType)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.movementType = movementType;
            return this;
        }
    }
}