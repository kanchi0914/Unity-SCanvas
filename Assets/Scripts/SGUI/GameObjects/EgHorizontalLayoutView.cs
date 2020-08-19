using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static EGUI.Base.Utils;

namespace EGUI.GameObjects
{
    public class EgHorizontalLayoutView : EGGameObject
    {
        public HorizontalLayoutGroup LayoutComponent { get; private set; }

        public int PaddingLeft => LayoutComponent.padding.left;
        public int PaddingRight => LayoutComponent.padding.right;
        public int PaddingTop => LayoutComponent.padding.top;
        public int PaddingBottom => LayoutComponent.padding.bottom;
        public float Spacing  => LayoutComponent.spacing;

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
            LayoutComponent = gameObject.TryAddComponent<HorizontalLayoutGroup>();
            LayoutComponent.childScaleHeight = false;
            LayoutComponent.childScaleWidth = false;
            LayoutComponent.childControlWidth = isAutoSizingWidth;
            LayoutComponent.childControlHeight = isAutoSizingHeight;
            LayoutComponent.childForceExpandHeight = isAutoSizingHeight;
            LayoutComponent.childForceExpandWidth = isAutoAlignment || isAutoSizingWidth;
        }

        public EgHorizontalLayoutView SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
        {
            LayoutComponent.padding.left = left ?? PaddingLeft;
            LayoutComponent.padding.right = right ?? PaddingRight;
            LayoutComponent.padding.top = top ?? PaddingTop;
            LayoutComponent.padding.bottom = bottom ?? PaddingBottom;
            return this;
        }

        public EgHorizontalLayoutView SetSpacing(float spacing)
        {
            LayoutComponent.spacing = spacing;
            return this;
        }
    }
}