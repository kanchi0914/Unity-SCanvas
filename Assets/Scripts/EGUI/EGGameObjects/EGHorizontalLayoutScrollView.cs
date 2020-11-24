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
        /// DropDownオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isAutoSizingHeight">アイテムの高さを親に合わせるか</param>
        /// <param name="scrollBarHeight">ScrollBarオブジェクトの高さ</param>
        public EGHorizontalLayoutScrollView
        (
            EGGameObject parent,
            bool isAutoSizingHeight = true,
            int scrollBarHeight = 20
        ) : this
        (
            parent.gameObject,
            isAutoSizingHeight, scrollBarHeight
        ){}
        
        
        /// <summary>
        /// HorizontalLayoutGroupでアイテムを配置するScrollViewオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isAutoSizingHeight">アイテムの高さを親に合わせるか</param>
        /// <param name="scrollBarHeight">ScrollBarオブジェクトの高さ</param>
        /// <param name="name">オブジェクト名</param>
        public EGHorizontalLayoutScrollView(
            GameObject parent = null,
            bool isAutoSizingHeight = true,
            int scrollBarHeight = 20,
            string name = "EGHorizontalLayoutScrollView"
        ) : base(parent, name)
        {
            SetMiddleCenterAnchor()
                .SetLocalPos(0, 0)
                .SetRectSize(defaultWidth, defaultHeight)
                .SetImageColor(Color.white, 0.4f);
            var viewport = new EGGameObject(gameObject, name: "Viewport")
                .SetFullStretchAnchor()
                .SetPivot(0,1)
                .SetRectSizeByRatio(1, 1)
                .SetLocalPos(0, 0);

            ContentAreaObject = new EgHorizontalLayoutView(viewport.gameObject,
                isAutoSizingHeight: isAutoSizingHeight, name: "Content");
            ContentAreaObject
                .SetRectSize(0, defaultHeight)
                .SetFullStretchAnchor()
                .SetPivot(0, 1)
                .SetLocalPos(0, 0);
            
            ScrollBarObject = new EGScrollBar(gameObject, name: "Scrollbar Horizontal")
                .SetDirection(Scrollbar.Direction.LeftToRight)
                .SetHorizontalStretchWithBottomPivotAnchor()
                .SetRectSize(defaultWidth, scrollBarHeight)
                .SetLocalPos(0, 0) as EGScrollBar;;

            ScrollRectComponent = gameObject.AddComponent<ScrollRect>();
            ScrollRectComponent.content = ContentAreaObject.gameObject.GetRectTransform();
            ScrollRectComponent.viewport = viewport.rectTransform;
            ScrollRectComponent.horizontalScrollbar = ScrollBarObject.gameObject.GetComponent<Scrollbar>();
            ScrollRectComponent.horizontalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.verticalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            ScrollRectComponent.horizontalScrollbarSpacing = -3;
            ScrollRectComponent.verticalScrollbarSpacing = -3;
            ScrollRectComponent.horizontal = true;
            ScrollRectComponent.vertical = false;

            var mask = viewport.gameObject.AddComponent<Mask>();
            viewport.gameObject.SetImageSprite(UGUIResources.Mask);
            mask.showMaskGraphic = false;

            var csfitter = ContentAreaObject.gameObject.GetOrAddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
            csfitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

            gameObject.OnTransformChildrenChangedAsObservable().Subscribe(_ =>
            {
                var added = gameObject.GetChildrenObjects().Last();
                if (added != ScrollBarObject.gameObject)
                    gameObject.GetChildrenObjects().Last().transform.SetParent(ContentAreaObject.rectTransform);
            });
        }
        
        /// <summary>
        /// アイテムが配置される
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public EGHorizontalLayoutScrollView SetPaddingAndSpacing(int num)
        {
            ContentAreaObject.SetPaddingAndSpacing(num, num, num, num, num);
            return this;
        }
        
        public EGHorizontalLayoutScrollView SetChildAlinmentTypes(
            TextAnchor? childAlignment = null,
            bool? childControlWidth = null,
            bool? childControlHeight = null,
            bool? childScaleWidth = null,
            bool? childScaleHeight = null,
            bool? childForceExpandWidth = null,
            bool? childForceExpandHeight = null
        )
        {
            ContentAreaObject.SetChildAliments(childAlignment, childControlWidth, childControlHeight,
                childScaleWidth, childScaleHeight,
                childForceExpandWidth, childForceExpandHeight);
            return this;
        }
        
        public EGHorizontalLayoutScrollView SetPaddingAndSpacing(int? paddingLeft = null, int? paddingRight = null,
            int? paddingTop = null,
            int? paddingBottom = null, float? spacing = null)
        {
            ContentAreaObject.SetPaddingAndSpacing(paddingLeft, paddingRight, paddingTop, paddingBottom, spacing);
            return this;
        }

        public EGHorizontalLayoutScrollView SetMovementType(MovementType? movementType = null, float? elasticity = null)
        {
            ScrollRectComponent.movementType = movementType ?? ScrollRectComponent.movementType;
            ScrollRectComponent.elasticity = elasticity ?? ScrollRectComponent.elasticity;
            return this;
        }

        public EGHorizontalLayoutScrollView SetScrollBarVisibility(ScrollbarVisibility visibility, float? spacing = null)
        {
            ScrollRectComponent.verticalScrollbarVisibility = visibility;
            ScrollRectComponent.verticalScrollbarSpacing = spacing ?? ScrollRectComponent.verticalScrollbarSpacing;
            return this;
        }
    }
}