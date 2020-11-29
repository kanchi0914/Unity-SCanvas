using System.Linq;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using UniRx;
using UniRx.Triggers;
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
        /// DropDownオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクトを</param>
        /// <param name="rowCount">行数</param>
        /// <param name="constantItemWidth">アイテムの幅</param>
        /// <param name="scrollBarHeight">スクロールバーの高さ</param>
        public EGHorizontalGridLayoutScrollView
        (
            EGGameObject parent,
            int rowCount = 3,
            int constantItemWidth = 100,
            int scrollBarHeight = 20
        ) : this
        (
            parent.gameObject,
            rowCount,constantItemWidth,scrollBarHeight
        ){}

        /// <summary>
        /// GridLayoutGroupでアイテムを配置し、
        /// 水平方向にスクロールするScrollViewのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="rowCount">行数</param>
        /// <param name="constantItemWidth">アイテムの幅</param>
        /// <param name="scrollBarHeight">スクロールバーの高さ</param>
        /// <param name="name">オブジェクトの名前</param>
        public EGHorizontalGridLayoutScrollView(
            GameObject parent = null,
            int rowCount = 3,
            int constantItemWidth = 100,
            int scrollBarHeight = 20,
            string name = "EGHorizontalGridLayoutScrollView"
        ) : base(parent, name)
        {
            SetMiddleCenterAnchor()
                .SetPosition(0, 0)
                .SetSize(200, 200)
                .SetImageColor(Color.white, 0.4f);
            var viewport = new EGGameObject(gameObject, name: "Viewport")
                .SetFullStretchAnchor()
                .SetPivot(0,1)
                .SetRelativeSize(1, 1)
                .SetPosition(0, 0);

            ContentAreaObject = new EGGridLayoutView(viewport.gameObject, rowCount,
                constantItemWidth:constantItemWidth, name: "Content");
            ContentAreaObject
                .SetChildAlignment(TextAnchor.UpperLeft)
                .SetSize(200, defaultHeight)
                .SetFullStretchAnchor()
                .SetPivot(0, 1)
                .SetPosition(0, 0);
            ContentAreaObject.LayoutComponent.startAxis = GridLayoutGroup.Axis.Vertical;
            
            ScrollBarObject = new EGScrollBar(gameObject, name: "Scrollbar Horizontal");
            ScrollBarObject
                .SetHorizontalStretchWithBottomPivotAnchor()
                .SetSize(200, scrollBarHeight)
                .SetPosition(0, 0);
            ScrollBarObject.ScrollbarComponent.direction = Scrollbar.Direction.LeftToRight;
            
            ScrollRectComponent = gameObject.AddComponent<ScrollRect>();
            ScrollRectComponent.content = ContentAreaObject.gameObject.GetRectTransform();
            ScrollRectComponent.viewport = viewport.rectTransform;
            ScrollRectComponent.horizontalScrollbar = ScrollBarObject.gameObject.GetComponent<Scrollbar>();
            ScrollRectComponent.horizontalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.verticalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.horizontalScrollbarSpacing = -3;
            ScrollRectComponent.verticalScrollbarSpacing = -3;
            ScrollRectComponent.vertical = false;
            ScrollRectComponent.horizontal = true;
            
            var mask = viewport.gameObject.AddComponent<Mask>();
            viewport.gameObject.SetImage(UGUIResources.Mask);
            mask.showMaskGraphic = false;

            var csfitter = ContentAreaObject.gameObject.GetOrAddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            csfitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            
            gameObject.OnTransformChildrenChangedAsObservable().Subscribe(_ =>
            {
                var added = gameObject.GetChildrenObjects().Last();
                if (added != ScrollBarObject.gameObject)
                    gameObject.GetChildrenObjects().Last().transform.SetParent(ContentAreaObject.rectTransform);
            });
        }
    }
}