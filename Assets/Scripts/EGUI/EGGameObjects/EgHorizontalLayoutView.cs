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
        /// HorizontalLayoutGroupコンポーネントを持つオブジェクトを生成し、参照を保持するクラス
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
            LayoutComponent.childScaleHeight = false;
            LayoutComponent.childScaleWidth = false;
            LayoutComponent.childControlWidth = isAutoSizingWidth;
            LayoutComponent.childControlHeight = isAutoSizingHeight;
            LayoutComponent.childForceExpandHeight = isAutoSizingHeight;
            LayoutComponent.childForceExpandWidth = isAutoAlignment || isAutoSizingWidth;
        }

        /// <summary>
        /// HorizontalLayoutGrouコンポーネントのpadding, spacingを設定する
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
        /// HorizontalLayoutGrouコンポーネントのpadding, spacingにまとめて同じ値を設定する
        /// </summary>
        /// <param name="left">設定する値</param>
        public void SetPaddingAndSpacing(int num)
        {
            SetPaddingAndSpacing(num, num, num, num, num);
        }
    }
}