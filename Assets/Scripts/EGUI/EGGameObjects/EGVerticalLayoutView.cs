
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGVerticalLayoutView : EGGameObject
    {
        /// <summary>
        /// VerticalLayoutGroupコンポーネントへの参照
        /// </summary>
        public VerticalLayoutGroup LayoutComponent;

        /// <summary>
        /// VerticalLayoutGroupコンポーネントを持つオブジェクトを生成し、参照を保持するクラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isAutoSizingWidth">各アイテムの幅を親に合わせて自動調整するか</param>
        /// <param name="isAutoSizingHeight">各アイテムの高さを自動調整するか</param>
        /// <param name="isAutoAlignment">アイテムの並ぶ間隔を自動で調整するか</param>
        /// <param name="name"></param>
        public EGVerticalLayoutView
        (
            GameObject parent,
            bool isAutoSizingWidth = false,
            bool isAutoSizingHeight = false,
            bool isAutoAlignment = false,
            string name = "EGVerticalLayoutView"
        ) : base
        (
            parent,
            name
        )
        {
            LayoutComponent = gameObject.GetOrAddComponent<VerticalLayoutGroup>();
            SetChildAlignments(null, false, false, isAutoSizingWidth,
                isAutoSizingHeight, isAutoAlignment || isAutoSizingHeight,
                isAutoSizingWidth);
        }

        public EGVerticalLayoutView SetChildAlignments(
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
        /// VerticalLayoutGroupコンポーネントのpadding, spacingを設定する
        /// </summary>
        /// <param name="paddingLeft">padding.left</param>
        /// <param name="paddingRight">padding.right</param>
        /// <param name="paddingTop">padding.top</param>
        /// <param name="paddingBottom">padding.bottom</param>
        /// /// <param name="spacing">spacing</param>
        public EGVerticalLayoutView SetPaddingAndSpacing(int? paddingLeft = null, int? paddingRight = null, int? paddingTop = null,
            int? paddingBottom = null, float? spacing = null)
        {
            LayoutComponent.padding.left = paddingLeft ?? LayoutComponent.padding.left;
            LayoutComponent.padding.right = paddingRight ?? LayoutComponent.padding.right;
            LayoutComponent.padding.top = paddingTop ?? LayoutComponent.padding.top;
            LayoutComponent.padding.bottom = paddingBottom ?? LayoutComponent.padding.bottom;
            LayoutComponent.spacing = spacing ?? LayoutComponent.spacing;
            return this;
        }

        /// <summary>
        /// VerticalLayoutGroupコンポーネントのpadding, spacingにまとめて同じ値を設定する
        /// </summary>
        /// <param name="left">設定する値</param>
        public EGVerticalLayoutView SetPaddingAndSpacing(int num)
        {
            SetPaddingAndSpacing(num, num, num, num, num);
            return this;
        }
    }
}