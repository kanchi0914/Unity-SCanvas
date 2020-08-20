using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace EGUI.GameObjects
{
    class EGVerticalGridLayoutScrollView : EGGameObject
    {
        private int defaultWidth = 200;
        private int defaultHeight = 200;
                
        /// <summary>
        /// ScrollRectコンポーネントへの参照
        /// </summary>
        public ScrollRect ScrollRectComponent { get; private set; }
        
        /// <summary>
        /// アイテムが配置されるGridLayoutGroupを持つオブジェクトをラップするクラス
        /// </summary>
        public EGGridLayoutView ContentAreaObject { get; private set; }

        /// <summary>
        /// crollBarオブジェクトをラップするクラス
        /// </summary>
        public EGScrollBar ScrollBarObject { get; private set; }
        
        /// <summary>
        /// GridLayoutGroupでアイテムが配置され、垂直方向にスクロールする
        /// ScrollRectコンポーネントを持つScrollViewを生成し、参照を保持するクラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="columnCount">列数</param>
        /// <param name="constantItemHeight">アイテムの高さ</param>
        /// <param name="scrollBarWidth">スクロールバーの幅</param>
        /// <param name="name">オブジェクトの名前</param>
        public EGVerticalGridLayoutScrollView(
            GameObject parent,
            int columnCount = 3,
            int constantItemHeight = 100,
            int scrollBarWidth = 20,
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
                .gameObject
                .SetFullStretchAnchor()
                .SetPivot(0,1)
                .SetRectSizeByRatio(1, 1)
                .SetLocalPos(0, 0);
                
            ContentAreaObject = new EGGridLayoutView(viewport,
                columnCount: columnCount, constantItemHeight:constantItemHeight, name: "Content");
            ContentAreaObject.gameObject
                .SetRectSize(200 - scrollBarWidth, 0 )
                .SetFullStretchAnchor()
                .SetLocalPos(0, 0);

            var scrollBar = new EGScrollBar(gameObject, name: "Scrollbar Vertical");
            scrollBar.gameObject
                .SetVerticalStretchWithRightPivotAnchor()
                .SetRectSize(scrollBarWidth, 200)
                .SetLocalPos(0, 0);
            scrollBar.ScrollbarComponent.direction = Scrollbar.Direction.BottomToTop;
            
            ScrollRectComponent = gameObject.AddComponent<ScrollRect>();
            ScrollRectComponent.content = ContentAreaObject.gameObject.GetRectTransform();
            ScrollRectComponent.viewport = viewport.GetRectTransform();
            ScrollRectComponent.verticalScrollbar = scrollBar.gameObject.GetComponent<Scrollbar>();
            ScrollRectComponent.horizontalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.verticalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.horizontalScrollbarSpacing = -3;
            ScrollRectComponent.verticalScrollbarSpacing = -3;
            ScrollRectComponent.vertical = true;
            ScrollRectComponent.horizontal = false;
            ScrollRectComponent.verticalScrollbarVisibility = ScrollbarVisibility.Permanent;
            
            var mask = viewport.gameObject.AddComponent<Mask>();
            viewport.gameObject.SetImageSprite(UGUIResources.Mask);
            mask.showMaskGraphic = false;
            
            var csfitter = ContentAreaObject.gameObject.GetOrAddComponent<ContentSizeFitter>();
            csfitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
            csfitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        }

    }
}