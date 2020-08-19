
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EgVerticalLayoutView : EGGameObject
    {
        public VerticalLayoutGroup LayoutComponent;
        public int PaddingLeft => LayoutComponent.padding.left;
        public int PaddingRight => LayoutComponent.padding.right;
        public int PaddingTop => LayoutComponent.padding.top;
        public int PaddingBottom => LayoutComponent.padding.bottom;
        public float Spacing  => LayoutComponent.spacing;

        public EgVerticalLayoutView
        (
            GameObject parent,
            bool isAutoSizingWidth = false,
            bool isAutoSizingHeight = false,
            bool isAutoAlignment = false,
            string name = "SVerticalLayoutView"
        ) : base
        (
            parent,
            name
        )
        {
            LayoutComponent = gameObject.TryAddComponent<VerticalLayoutGroup>();
            LayoutComponent.childScaleHeight = false;
            LayoutComponent.childScaleWidth = false;
            LayoutComponent.childControlWidth = isAutoSizingWidth;
            LayoutComponent.childControlHeight = isAutoSizingHeight;
            LayoutComponent.childForceExpandHeight = isAutoAlignment || isAutoSizingHeight;
            LayoutComponent.childForceExpandWidth = isAutoSizingWidth;
        }
        
        // /// <summary>
        // /// 子オブジェクトを追加し、レイアウト配下に置く
        // /// </summary>
        // /// <param name="egGameObject"></param>
        // public void AddItem(EGGameObject egGameObject)
        // {
        //     egGameObject.GameObject.transform.SetParent(gameObject.transform, false);
        //     egGameObject.ParentEgGameObject = this;
        // }

        public EgVerticalLayoutView SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
        {
            LayoutComponent.padding.left = left ?? PaddingLeft;
            LayoutComponent.padding.right = right ?? PaddingRight;
            LayoutComponent.padding.top = top ?? PaddingTop;
            LayoutComponent.padding.bottom = bottom ?? PaddingBottom;
            return this;
        }
        
        public EgVerticalLayoutView SetPaddingAll(int padding)
        {
            SetPadding(padding, padding, padding, padding);
            return this;
        }

        public EgVerticalLayoutView SetSpacing(float spacing)
        {
            LayoutComponent.spacing = spacing;
            return this;
        }
    }
}