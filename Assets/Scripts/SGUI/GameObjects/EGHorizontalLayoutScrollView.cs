
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace EGUI.GameObjects
{
    class EGHorizontalLayoutScrollView : EGGameObject
    {
        private int constantItemWidth = 0;
        
        public ScrollRect ScrollRectComponent { get; private set; }

        public EgHorizontalLayoutView ContentAreaObject { get; private set; }

        public EGHorizontalLayoutScrollView(
            GameObject parent,
            int constantItemWidth = 0,
            bool isAutoSizingHeight = true,
            string name = "EGHorizontalLayoutScrollView"
        ) : base
        (
            parent,
            name
        )
        {
            gameObject.SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSize(200, 200)
                .SetImageColor(Color.white, 0.4f);
            var viewport = new EGGameObject(gameObject, name: "Viewport")
                .gameObject.SetMiddleCenterAnchor();
            ContentAreaObject = new EgHorizontalLayoutView(viewport,
                isAutoSizingHeight: isAutoSizingHeight, name: "Content");
            ContentAreaObject.gameObject.SetMiddleCenterAnchor();

            viewport.gameObject.SetFullStretchAnchor(); 
            viewport.gameObject.GetRectTransform().pivot = Vector2.up;

            ContentAreaObject.gameObject.GetRectTransform().anchorMin = Vector2.up;
            ContentAreaObject.gameObject.GetRectTransform().anchorMax = Vector2.one;
            ContentAreaObject.gameObject.GetRectTransform().sizeDelta = new Vector2(0, 180);
            ContentAreaObject.gameObject.GetRectTransform().pivot = Vector2.up;

            ScrollRectComponent = gameObject.AddComponent<ScrollRect>();
            ScrollRectComponent.content = ContentAreaObject.gameObject.GetRectTransform();
            ScrollRectComponent.viewport = viewport.GetRectTransform();

            Mask viewportMask = viewport.gameObject.AddComponent<Mask>();
            viewportMask.showMaskGraphic = false;

            var scrollBar = new EGScrollBar(gameObject, name: "Scrollbar Horizontal");
            scrollBar.gameObject.SetMiddleCenterAnchor();
            scrollBar.gameObject.GetRectTransform().anchorMin = Vector2.zero;
            scrollBar.gameObject.GetRectTransform().anchorMax = Vector2.right;
            scrollBar.gameObject.GetRectTransform().pivot = Vector2.zero;
            scrollBar.gameObject.GetRectTransform().sizeDelta = new Vector2(0, 20);

            ScrollRectComponent.horizontalScrollbar = scrollBar.gameObject.GetComponent<Scrollbar>();
            ScrollRectComponent.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.horizontalScrollbarSpacing = -3;
            ScrollRectComponent.verticalScrollbarSpacing = -3;

            ScrollRectComponent.vertical = false;
            ScrollRectComponent.horizontal = true;
            scrollBar.gameObject.SetActive(true);

            ScrollRectComponent.horizontalScrollbarVisibility = ScrollbarVisibility.Permanent;
            
            var csfitter = ContentAreaObject.gameObject.TryAddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            csfitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

            this.constantItemWidth = constantItemWidth;
        }

    }
}