using System.Linq;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using EGUI.Base;
using UniRx.Triggers;
using UniRx;
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
        /// VerticalLayoutGroupでアイテムを配置するScrollViewオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isAutoSizingWidth">アイテムの幅を親に合わせるか</param>
        /// <param name="scrollBarWidth">ScrollBarオブジェクトの幅</param>
        public EGVerticalLayoutScrollView
        (
            EGGameObject parent,
            bool isAutoSizingWidth = true,
            int scrollBarWidth = 20
        ) : this
        (
            parent.gameObject, isAutoSizingWidth, scrollBarWidth
        )
        {
        }

        /// <summary>
        /// VerticalLayoutGroupでアイテムを配置するScrollViewオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isAutoSizingWidth">アイテムの幅を親に合わせるか</param>
        /// <param name="scrollBarWidth">ScrollBarオブジェクトの幅</param>
        /// <param name="name">オブジェクト名</param>
        public EGVerticalLayoutScrollView(
            GameObject parent = null,
            bool isAutoSizingWidth = true,
            int scrollBarWidth = 20,
            string name = "EGVerticalLayoutScrollView"
        ) : base
        (
            parent,
            name
        )
        {
            SetAnchorType(AnchorType.MiddleCenter)
                .SetPosition(0, 0)
                .SetSize(defaultWidth, defaultHeight)
                .SetImageColor(Color.white, 0.4f);

            var viewport = new EGGameObject(gameObject, name: "Viewport")
                .SetAnchorType(AnchorType.FullStretch)
                .SetPivot(0, 0)
                .SetRelativeSize(1, 1)
                .SetPosition(0, 0);

            ContentAreaObject = new EGVerticalLayoutView(viewport.gameObject, isAutoSizingWidth, name: "Content")
                .SetSize(defaultWidth, 0)
                .SetAnchorType(AnchorType.FullStretch)
                // .SetFullStretchAnchor()
                .SetPivot(0, 0)
                .SetAnchorType(AnchorType.HorizontalStretchWithTopPivot)
                // .SetHorizontalStretchWithTopPivotAnchor()
                .SetPosition(0, 0) as EGVerticalLayoutView;

            ScrollBarObject = new EGScrollBar(gameObject, name: "Scrollbar Vertical")
                .SetDirection(Scrollbar.Direction.BottomToTop)
                .SetAnchorType(AnchorType.VerticalStretchWithLeftPivot)
                .SetSize(scrollBarWidth, defaultHeight)
                .SetPosition(0, 0) as EGScrollBar;

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
            viewport.gameObject.SetImage(UGUIDefaultResources.Mask);
            mask.showMaskGraphic = false;

            var csfitter = ContentAreaObject.gameObject.GetOrAddComponent<ContentSizeFitter>();
            csfitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
            csfitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

            gameObject.OnTransformChildrenChangedAsObservable().Subscribe(_ =>
            {
                var added = gameObject.GetChildrenObjects().Last();
                if (added != ScrollBarObject.gameObject)
                    gameObject.GetChildrenObjects().Last().transform.SetParent(ContentAreaObject.rectTransform);
            });
        }

        public EGVerticalLayoutScrollView SetChildAlinmentTypes(
            TextAnchor? childAlignment = null,
            bool? childControlWidth = null,
            bool? childControlHeight = null,
            bool? childScaleWidth = null,
            bool? childScaleHeight = null,
            bool? childForceExpandWidth = null,
            bool? childForceExpandHeight = null
        )
        {
            ContentAreaObject.SetChildAlignments(childAlignment, childControlWidth, childControlHeight,
                childScaleWidth, childScaleHeight,
                childForceExpandWidth, childForceExpandHeight);
            return this;
        }

        public EGVerticalLayoutScrollView SetPaddingAndSpacing(int? paddingLeft = null, int? paddingRight = null,
            int? paddingTop = null,
            int? paddingBottom = null, float? spacing = null)
        {
            ContentAreaObject.SetPaddingAndSpacing(paddingLeft, paddingRight, paddingTop, paddingBottom, spacing);
            return this;
        }

        public EGVerticalLayoutScrollView SetPaddingAndSpacing(int num)
        {
            ContentAreaObject.SetPaddingAndSpacing(num, num, num, num, num);
            return this;
        }

        public EGVerticalLayoutScrollView SetMovementType(MovementType? movementType = null, float? elasticity = null)
        {
            ScrollRectComponent.movementType = movementType ?? ScrollRectComponent.movementType;
            ScrollRectComponent.elasticity = elasticity ?? ScrollRectComponent.elasticity;
            return this;
        }

        public EGVerticalLayoutScrollView SetScrollBarVisibility(ScrollbarVisibility visibility, float? spacing = null)
        {
            ScrollRectComponent.verticalScrollbarVisibility = visibility;
            ScrollRectComponent.verticalScrollbarSpacing = spacing ?? ScrollRectComponent.verticalScrollbarSpacing;
            return this;
        }
    }
}