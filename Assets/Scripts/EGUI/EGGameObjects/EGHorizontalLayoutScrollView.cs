using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace EGUI.GameObjects
{
    class EGHorizontalLayoutScrollView : EGGameObject
    {
        private int defaultWidth = 200;

        private int defaultHeight = 200;
        
        /// <summary>
        /// ScrollRectコンポーネントへの参照
        /// </summary>
        public ScrollRect ScrollRectComponent { get; private set; }

        /// <summary>
        /// アイテムが配置されるgHorizontalLayoutGroupを持つオブジェクトへの参照
        /// </summary>
        public EgHorizontalLayoutView ContentAreaObject { get; private set; }
        
        /// <summary>
        /// スクロールバーオブジェクトへの参照
        /// </summary>
        public EGScrollBar ScrollBarObject { get; private set; }
        
        /// <summary>
        /// HorizontalLayoutGroupでアイテムが配置されるScrollViewオブジェクトを生成し、参照を保持するクラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isAutoSizingHeight">アイテムの高さを親に合わせるか</param>
        /// <param name="scrollBarHeight">ScrollBarオブジェクトの高さ</param>
        /// <param name="name">オブジェクト名</param>
        public EGHorizontalLayoutScrollView(
            GameObject parent,
            bool isAutoSizingHeight = true,
            int scrollBarHeight = 20,
            string name = "EGHorizontalLayoutScrollView"
        ) : base(parent, name)
        {
            gameObject.SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSize(defaultWidth, defaultHeight)
                .SetImageColor(Color.white, 0.4f);
            var viewport = new EGGameObject(gameObject, name: "Viewport")
                .gameObject
                .SetFullStretchAnchor()
                .SetPivot(0,1)
                .SetRectSizeByRatio(1, 1)
                .SetLocalPos(0, 0);

            ContentAreaObject = new EgHorizontalLayoutView(viewport,
                isAutoSizingHeight: isAutoSizingHeight, name: "Content");
            ContentAreaObject.gameObject
                .SetRectSize(0, defaultHeight)
                .SetFullStretchAnchor()
                .SetPivot(0, 1)
                .SetLocalPos(0, 0);
            
            ScrollBarObject = new EGScrollBar(gameObject, name: "Scrollbar Horizontal");
            ScrollBarObject.gameObject
                .SetHorizontalStretchWithBottomPivotAnchor()
                .SetRectSize(defaultWidth, scrollBarHeight)
                .SetLocalPos(0, 0);
            ScrollBarObject.ScrollbarComponent.direction = Scrollbar.Direction.LeftToRight;

            ScrollRectComponent = gameObject.AddComponent<ScrollRect>();
            ScrollRectComponent.content = ContentAreaObject.gameObject.GetRectTransform();
            ScrollRectComponent.viewport = viewport.GetRectTransform();
            ScrollRectComponent.horizontalScrollbar = ScrollBarObject.gameObject.GetComponent<Scrollbar>();
            ScrollRectComponent.horizontalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.verticalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.horizontalScrollbarSpacing = -3;
            ScrollRectComponent.verticalScrollbarSpacing = -3;
            ScrollRectComponent.vertical = false;
            ScrollRectComponent.horizontal = true;
            
            var mask = viewport.gameObject.AddComponent<Mask>();
            viewport.gameObject.SetImageSprite(UGUIResources.Mask);
            mask.showMaskGraphic = false;

            var csfitter = ContentAreaObject.gameObject.GetOrAddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
            csfitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        }
    }
}