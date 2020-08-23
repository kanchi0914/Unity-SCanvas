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
        /// GridLayoutGroupでアイテムを配置し
        /// 垂直方向にスクロールするScrollViewをラップするクラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="columnCount">列数</param>
        /// <param name="constantItemHeight">アイテムの高さ</param>
        /// <param name="scrollBarWidth">スクロールバーの幅</param>
        public EGVerticalGridLayoutScrollView
        (
            EGGameObject parent,
            int columnCount = 3,
            int constantItemHeight = 100,
            int scrollBarWidth = 20
        ) : this
        (
            parent.gameObject, columnCount, constantItemHeight, scrollBarWidth
        )
        {
        }

        /// <summary>
        /// GridLayoutGroupでアイテムを配置し
        /// 垂直方向にスクロールするScrollViewをラップするクラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="columnCount">列数</param>
        /// <param name="constantItemHeight">アイテムの高さ</param>
        /// <param name="scrollBarWidth">スクロールバーの幅</param>
        /// <param name="name">オブジェクトの名前</param>
        public EGVerticalGridLayoutScrollView(
            GameObject parent = null,
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
            SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSize(defaultWidth, defaultHeight)
                .SetImageColor(Color.white, 0.4f);
            var viewport = new EGGameObject(gameObject, name: "Viewport")
                .SetFullStretchAnchor()
                .SetPivot(0, 0)
                .SetRectSizeByRatio(1, 1)
                .SetLocalPos(0, 0);

            ContentAreaObject = new EGGridLayoutView(viewport.gameObject,
                columnCount: columnCount, constantItemHeight: constantItemHeight, name: "Content") as EGGridLayoutView;
            ContentAreaObject
                .SetChildAlignment(TextAnchor.UpperLeft)
                .SetRectSize(defaultWidth, 0)
                .SetFullStretchAnchor()
                .SetPivot(0, 1)
                .SetLocalPos(0, 0);

            ScrollBarObject = new EGScrollBar(gameObject, name: "Scrollbar Vertical");
            ScrollBarObject
                .SetVerticalStretchWithRightPivotAnchor()
                .SetRectSize(scrollBarWidth, defaultHeight)
                .SetLocalPos(0, 0);
            ScrollBarObject.ScrollbarComponent.direction = Scrollbar.Direction.BottomToTop;

            ScrollRectComponent = gameObject.AddComponent<ScrollRect>();
            ScrollRectComponent.content = ContentAreaObject.gameObject.GetRectTransform();
            ScrollRectComponent.viewport = viewport.rectTransform;
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
            csfitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            csfitter.verticalFit = ContentSizeFitter.FitMode.MinSize;

            gameObject.OnTransformChildrenChangedAsObservable().Subscribe(_ =>
            {
                var added = gameObject.GetChildrenObjects().Last();
                if (added != ScrollBarObject.gameObject)
                    gameObject.GetChildrenObjects().Last().transform.SetParent(ContentAreaObject.rectTransform);
            });
        }

        public EGVerticalGridLayoutScrollView SetChildAlignment(
            TextAnchor childAlignment
        )
        {
            ContentAreaObject.SetChildAlignment(childAlignment);
            return this;
        }

        public EGVerticalGridLayoutScrollView SetPaddingAndSpacing(int? paddingLeft = null, int? paddingRight = null,
            int? paddingTop = null,
            int? paddingBottom = null, float? spacing = null)
        {
            ContentAreaObject.SetPaddingAndSpacing(paddingLeft, paddingRight, paddingTop, paddingBottom, spacing);
            return this;
        }

        public EGVerticalGridLayoutScrollView SetPaddingAndSpacing(int num)
        {
            ContentAreaObject.SetPaddingAndSpacing(num, num, num, num, num);
            return this;
        }

        public EGVerticalGridLayoutScrollView SetMovementType(MovementType? movementType = null,
            float? elasticity = null)
        {
            ScrollRectComponent.movementType = movementType ?? ScrollRectComponent.movementType;
            ScrollRectComponent.elasticity = elasticity ?? ScrollRectComponent.elasticity;
            return this;
        }

        public EGVerticalGridLayoutScrollView SetScrollBarVisibility(ScrollbarVisibility visibility,
            float? spacing = null)
        {
            ScrollRectComponent.verticalScrollbarVisibility = visibility;
            ScrollRectComponent.verticalScrollbarSpacing = spacing ?? ScrollRectComponent.verticalScrollbarSpacing;
            return this;
        }
    }
}