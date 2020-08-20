using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace EGUI.GameObjects
{
    class EGHorizontalGridLayoutScrollView : EGGameObject
    {
        private int defaultWidth = 200;

        private int defaultHeight = 200;
        
        /// <summary>
        /// ScrollRectコンポーネントへの参照
        /// </summary>
        public ScrollRect ScrollRectComponent { get; private set; }

        /// <summary>
        /// アイテムが配置されるGridLayoutGroupを持つゲームオブジェクトをラップするクラス
        /// </summary>
        public EGGridLayoutView ContentAreaObject { get; private set; }
        
        /// <summary>
        /// ScrollBarゲームオブジェクトをラップするクラス
        /// </summary>
        public EGScrollBar ScrollBarObject { get; private set; }

        /// <summary>
        /// GridLayoutGroupでアイテムが配置される、
        /// 水平方向にスクロールするScrollRectコンポーネントを持つScrollViewを生成し、参照を保持するクラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="rowCount">行数</param>
        /// <param name="constantItemWidth">アイテムの幅</param>
        /// <param name="scrollBarHeight">スクロールバーの高さ</param>
        /// <param name="name">オブジェクトの名前</param>
        public EGHorizontalGridLayoutScrollView(
            GameObject parent,
            int rowCount = 3,
            int constantItemWidth = 100,
            int scrollBarHeight = 20,
            string name = "EGHorizontalGridLayoutScrollView"
        ) : base(parent, name)
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

            ContentAreaObject = new EGGridLayoutView(viewport, rowCount,
                constantItemWidth:constantItemWidth, name: "Content");
            ContentAreaObject.gameObject
                .SetRectSize(200, defaultHeight)
                .SetFullStretchAnchor()
                .SetPivot(0, 1)
                .SetLocalPos(0, 0);
            ContentAreaObject.LayoutComponent.startAxis = GridLayoutGroup.Axis.Vertical;
            
            ScrollBarObject = new EGScrollBar(gameObject, name: "Scrollbar Horizontal");
            ScrollBarObject.gameObject
                .SetHorizontalStretchWithBottomPivotAnchor()
                .SetRectSize(200, scrollBarHeight)
                .SetLocalPos(0, 0);

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