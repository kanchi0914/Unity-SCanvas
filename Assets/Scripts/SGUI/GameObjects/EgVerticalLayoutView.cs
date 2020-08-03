
using EGUI.Base;
using EGUI.GameObjects.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EgVerticalLayoutView : EGGameObject, ILayoutObject
    {
        private readonly VerticalLayoutGroup layoutComponent;
        public int PaddingLeft => layoutComponent.padding.left;
        public int PaddingRight => layoutComponent.padding.right;
        public int PaddingTop => layoutComponent.padding.top;
        public int PaddingBottom => layoutComponent.padding.bottom;
        public float Spacing  => layoutComponent.spacing;

        public EgVerticalLayoutView
        (
            EGGameObject parent,
            bool isAutoSizingWidth = false,
            bool isAutoSizingHeight = false,
            bool isAutoAlignment = false,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "SVerticalLayoutView"
        ) : base
        (
            parent,
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            name,
            () => UIFactory.CreateVerticalLayoutView(parent.GameObject, name)
        )
        {
            layoutComponent = gameObject.GetComponent<VerticalLayoutGroup>();
            layoutComponent.childControlWidth = isAutoSizingWidth;
            layoutComponent.childControlHeight = isAutoSizingHeight;
            layoutComponent.childForceExpandHeight = isAutoAlignment || isAutoSizingHeight;
            layoutComponent.childForceExpandWidth = isAutoSizingWidth;
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
        
        public EgVerticalLayoutView SetAutoSizingHeight(bool isActive = true)
        {
            layoutComponent.childControlHeight = isActive;
            layoutComponent.childForceExpandHeight = isActive;
            return this;
        }

        public EgVerticalLayoutView SetAutoSizingWidth(bool isActive = true)
        {
            layoutComponent.childControlWidth = isActive;
            return this;
        }

        public EgVerticalLayoutView SetAutoVerticalAlignment(bool isActive = true)
        {
            layoutComponent.childForceExpandHeight = isActive;
            return this;
        }

        public EgVerticalLayoutView SetChildAlignment(TextAnchor textAnchor)
        {
            layoutComponent.childAlignment = textAnchor;
            return this;
        }

        public EgVerticalLayoutView SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
        {
            layoutComponent.padding.left = left ?? PaddingLeft;
            layoutComponent.padding.right = right ?? PaddingRight;
            layoutComponent.padding.top = top ?? PaddingTop;
            layoutComponent.padding.bottom = bottom ?? PaddingBottom;
            return this;
        }

        public EgVerticalLayoutView SetSpacing(float spacing)
        {
            layoutComponent.spacing = spacing;
            return this;
        }
    }
}