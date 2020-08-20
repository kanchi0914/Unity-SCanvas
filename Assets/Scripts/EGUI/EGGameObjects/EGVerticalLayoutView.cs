
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
            LayoutComponent.childScaleHeight = false;
            LayoutComponent.childScaleWidth = false;
            LayoutComponent.childControlWidth = isAutoSizingWidth;
            LayoutComponent.childControlHeight = isAutoSizingHeight;
            LayoutComponent.childForceExpandHeight = isAutoAlignment || isAutoSizingHeight;
            LayoutComponent.childForceExpandWidth = isAutoSizingWidth;
        }
        
        /// <summary>
        /// VerticalLayoutGroupコンポーネントのpadding, spacingを設定する
        /// </summary>
        /// <param name="left">padding.left</param>
        /// <param name="right">padding.right</param>
        /// <param name="top">padding.top</param>
        /// <param name="bottom">padding.bottom</param>
        /// /// <param name="spacing">spacing</param>
        public void SetPaddingAndSpacing(int? left = null, int? right = null, int? top = null, int? bottom = null,
            float? spacing = null)
        {
            LayoutComponent.padding.left = left ?? LayoutComponent.padding.left;
            LayoutComponent.padding.right = right ?? LayoutComponent.padding.right;
            LayoutComponent.padding.top = top ?? LayoutComponent.padding.top;
            LayoutComponent.padding.bottom = bottom ?? LayoutComponent.padding.bottom;
            LayoutComponent.spacing = spacing ?? LayoutComponent.spacing;
        }

        /// <summary>
        /// VerticalLayoutGroupコンポーネントのpadding, spacingにまとめて同じ値を設定する
        /// </summary>
        /// <param name="left">設定する値</param>
        public void SetPaddingAndSpacing(int num)
        {
            SetPaddingAndSpacing(num, num, num, num, num);
        }
    }
}