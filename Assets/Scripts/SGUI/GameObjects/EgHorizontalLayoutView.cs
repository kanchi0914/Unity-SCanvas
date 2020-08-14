using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static EGUI.Base.Utils;

namespace EGUI.GameObjects
{
    public class EgHorizontalLayoutView : EGGameObject, ILayoutObject
    {
        public HorizontalLayoutGroup LayoutComponent { get; private set; }

        public int PaddingLeft => LayoutComponent.padding.left;
        public int PaddingRight => LayoutComponent.padding.right;
        public int PaddingTop => LayoutComponent.padding.top;
        public int PaddingBottom => LayoutComponent.padding.bottom;
        public float Spacing  => LayoutComponent.spacing;
        //
        // public float ConstantItemWidth { get; private set; }
        // public float ConstantItemHeight { get; private set; }

        public EgHorizontalLayoutView(
            EGGameObject parent,
            bool isAutoSizingWidth = false,
            bool isAutoSizingHeight = false,
            bool isAutoAlignment = false,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "SHorizontalLayoutView"
        ) : base(parent, 
            posRatioX, 
            posRatioY, 
            widthRatio, 
            heightRatio,
            name,
            () => UIFactory.CreateHotizontalLayoutView(parent.GameObject, name)
        )
        {
            LayoutComponent = gameObject.GetComponent<HorizontalLayoutGroup>();
            LayoutComponent.childControlWidth = isAutoSizingWidth;
            LayoutComponent.childControlHeight = isAutoSizingHeight;
            LayoutComponent.childForceExpandHeight = isAutoSizingHeight;
            LayoutComponent.childForceExpandWidth = isAutoAlignment || isAutoSizingWidth;
        }

        /// <summary>
        /// 子オブジェクトを追加し、レイアウト配下に置く
        /// </summary>
        /// <param name="egGameObject"></param>
        public void AddItem(EGGameObject egGameObject)
        {
            gameObject.transform.SetParent(gameObject.transform, false);
            egGameObject.ParentEgGameObject = this;
        }
        
        public EgHorizontalLayoutView SetAutoSizingHeight(bool isActive = true)
        {
            LayoutComponent.childControlHeight = isActive;
            return this;
        }

        public EgHorizontalLayoutView SetAutoSizingWidth(bool isActive = true)
        {
            LayoutComponent.childControlWidth = isActive;
            LayoutComponent.childForceExpandWidth = isActive;
            return this;
        }

        public EgHorizontalLayoutView SetAutoHorizontalAlignment(bool isActive = true)
        {
            LayoutComponent.childForceExpandWidth = isActive;
            return this;
        }

        public EgHorizontalLayoutView SetChildAlignment(TextAnchor textAnchor)
        {
            LayoutComponent.childAlignment = textAnchor;
            return this;
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