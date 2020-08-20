
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace EGUI.GameObjects
{
    class EGVerticalLayoutScrollView : EGGameObject
    {
        private int defaultWidth = 200;
        private int defaultHeight = 200;
        
        /// <summary>
        /// ScrollRectコンポーネントへの参照
        /// </summary>
        public ScrollRect ScrollRectComponent { get; private set; }

        /// <summary>
        /// アイテムが配置されるVerticalLayoutGroupを持つオブジェクトをラップするクラス
        /// </summary>
        public EGVerticalLayoutView ContentAreaObject { get; private set; }
        
        /// <summary>
        /// ScrollBarオブジェクトをラップするクラス
        /// </summary>
        public EGScrollBar ScrollBarObject { get; private set; }

        /// <summary>
        /// VerticalLayoutGroupでアイテムが配置されるScrollViewオブジェクトを生成し、参照を保持するクラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isAutoSizingWidth">アイテムの幅を親に合わせるか</param>
        /// <param name="scrollBarWidth">ScrollBarオブジェクトの幅</param>
        /// <param name="name">オブジェクト名</param>
        public EGVerticalLayoutScrollView(
            GameObject parent,
            bool isAutoSizingWidth = true,
            int scrollBarWidth = 20,
            string name = "EGVerticalLayoutScrollView"
        ) : base
        (
            parent,
            name
        )
        {
            gameObject.SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSize(defaultWidth, defaultHeight)
                .SetImageColor(Color.white, 0.4f);
            
            var viewport = new EGGameObject(gameObject, name: "Viewport")
                .gameObject
                .SetFullStretchAnchor()
                .SetPivot(0,0)
                .SetRectSizeByRatio(1, 1)
                .SetLocalPos(0, 0);
                
            ContentAreaObject = new EGVerticalLayoutView(viewport,
                isAutoSizingWidth, name: "Content");
            ContentAreaObject.gameObject
                .SetRectSize(defaultWidth, 0 )
                .SetFullStretchAnchor()
                .SetPivot(0, 0)
                .SetHorizontalStretchWithTopPivotAnchor()
                .SetLocalPos(0, 0);

            ScrollBarObject = new EGScrollBar(gameObject, name: "Scrollbar Vertical");
            ScrollBarObject.gameObject
                .SetVerticalStretchWithRightPivotAnchor()
                .SetRectSize(scrollBarWidth, defaultHeight)
                .SetLocalPos(0, 0);
            ScrollBarObject.ScrollbarComponent.direction = Scrollbar.Direction.BottomToTop;
            
            ScrollRectComponent = gameObject.AddComponent<ScrollRect>();
            ScrollRectComponent.content = ContentAreaObject.gameObject.GetRectTransform();
            ScrollRectComponent.viewport = viewport.GetRectTransform();
            ScrollRectComponent.verticalScrollbar = ScrollBarObject.gameObject.GetComponent<Scrollbar>();
            ScrollRectComponent.horizontalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.verticalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.horizontalScrollbarSpacing = -3;
            ScrollRectComponent.verticalScrollbarSpacing = -3;
            ScrollRectComponent.vertical = true;
            ScrollRectComponent.horizontal = false;
            
            var mask = viewport.gameObject.AddComponent<Mask>();
            viewport.gameObject.SetImageSprite(UGUIResources.Mask);
            mask.showMaskGraphic = false;
            
            var csfitter = ContentAreaObject.gameObject.GetOrAddComponent<ContentSizeFitter>();
            csfitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
            csfitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        }

    }
}