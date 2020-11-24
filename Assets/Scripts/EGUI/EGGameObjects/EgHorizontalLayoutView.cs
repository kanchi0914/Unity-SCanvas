using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EgHorizontalLayoutView : EGGameObject
    {
        /// <summary>
        /// HorizontalLayoutGroupコンポーネントへの参照
        /// </summary>
        public HorizontalLayoutGroup LayoutComponent { get; private set; }
        
        /// <summary>
        /// EgHorizontalLayoutViewオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isAutoSizingWidth">各アイテムの幅を自動調整するか</param>
        /// <param name="isAutoSizingHeight">各アイテムの高さを親に合わせて自動調整するか</param>
        /// <param name="isAutoAlignment">各アイテムの間隔を自動で調整するか</param>
        public EgHorizontalLayoutView
        (
            EGGameObject parent = null,
            bool isAutoSizingWidth = false,
            bool isAutoSizingHeight = false,
            bool isAutoAlignment = false
        ) : this
        (
            parent.gameObject,
            isAutoSizingWidth, isAutoSizingHeight, isAutoAlignment
        ){}

        /// <summary>
        /// EgHorizontalLayoutViewオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isAutoSizingWidth">各アイテムの幅を自動調整するか</param>
        /// <param name="isAutoSizingHeight">各アイテムの高さを親に合わせて自動調整するか</param>
        /// <param name="isAutoAlignment">各アイテムの間隔を自動で調整するか</param>
        /// <param name="name">オブジェクト名</param>
        public EgHorizontalLayoutView(
            GameObject parent,
            bool isAutoSizingWidth = false,
            bool isAutoSizingHeight = false,
            bool isAutoAlignment = false,
            string name = "SHorizontalLayoutView"
        ) : base(
            parent,
            name
        )
        {
            LayoutComponent = gameObject.GetOrAddComponent<HorizontalLayoutGroup>();
            SetChildAliments(null, isAutoSizingWidth, isAutoSizingHeight, false, false,
                isAutoAlignment || isAutoSizingWidth, isAutoSizingHeight);
        }

        /// <summary>
        /// 子オブジェクトの配置方法を設定する
        /// </summary>
        /// <param name="childAlignment"></param>
        /// <param name="childControlWidth"></param>
        /// <param name="childControlHeight"></param>
        /// <param name="childScaleWidth"></param>
        /// <param name="childScaleHeight"></param>
        /// <param name="childForceExpandWidth"></param>
        /// <param name="childForceExpandHeight"></param>
        /// <returns></returns>
        public EgHorizontalLayoutView SetChildAliments(
            TextAnchor? childAlignment = null,
            bool? childControlWidth = null,
            bool? childControlHeight = null,
            bool? childScaleWidth = null,
            bool? childScaleHeight = null,
            bool? childForceExpandWidth = null,
            bool? childForceExpandHeight = null
        )
        {
            LayoutComponent.childAlignment = childAlignment ?? LayoutComponent.childAlignment;
            LayoutComponent.childControlWidth = childControlWidth ?? LayoutComponent.childControlWidth;
            LayoutComponent.childControlHeight = childControlHeight ?? LayoutComponent.childControlHeight;
            LayoutComponent.childScaleWidth = childScaleWidth ?? LayoutComponent.childScaleWidth;
            LayoutComponent.childScaleHeight = childScaleHeight ?? LayoutComponent.childScaleHeight;
            LayoutComponent.childForceExpandWidth = childForceExpandWidth ?? LayoutComponent.childForceExpandWidth;
            LayoutComponent.childForceExpandHeight = childForceExpandHeight ?? LayoutComponent.childForceExpandHeight;
            return this;
        }

        /// <summary>
        /// padding, spacingを設定する
        /// </summary>
        /// <param name="left">padding.left</param>
        /// <param name="right">padding.right</param>
        /// <param name="top">padding.top</param>
        /// <param name="bottom">padding.bottom</param>
        /// /// <param name="spacing">spacing</param>
        public EgHorizontalLayoutView SetPaddingAndSpacing(int? left = null, int? right = null, int? top = null,
            int? bottom = null,
            float? spacing = null)
        {
            LayoutComponent.padding.left = left ?? LayoutComponent.padding.left;
            LayoutComponent.padding.right = right ?? LayoutComponent.padding.right;
            LayoutComponent.padding.top = top ?? LayoutComponent.padding.top;
            LayoutComponent.padding.bottom = bottom ?? LayoutComponent.padding.bottom;
            LayoutComponent.spacing = spacing ?? LayoutComponent.spacing;
            return this;
        }

        /// <summary>
        /// padding, spacingにまとめて同じ値を設定する
        /// </summary>
        /// <param name="left">設定する値</param>
        public EgHorizontalLayoutView SetPaddingAndSpacing(int num)
        {
            SetPaddingAndSpacing(num, num, num, num, num);
            return this;
        }
    }
}