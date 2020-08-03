using EGUI.Base;
using EGUI.GameObjects.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static EGUI.Base.Utils;

namespace EGUI.GameObjects
{
    public class EgHorizontalLayoutView : EGGameObject, ILayoutObject
    {
        private readonly HorizontalLayoutGroup layoutComponent;

        public int PaddingLeft => layoutComponent.padding.left;
        public int PaddingRight => layoutComponent.padding.right;
        public int PaddingTop => layoutComponent.padding.top;
        public int PaddingBottom => layoutComponent.padding.bottom;
        public float Spacing  => layoutComponent.spacing;

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
            layoutComponent = gameObject.GetComponent<HorizontalLayoutGroup>();
            layoutComponent.childControlWidth = isAutoSizingWidth;
            layoutComponent.childControlHeight = isAutoSizingHeight;
            layoutComponent.childForceExpandHeight = isAutoSizingHeight;
            layoutComponent.childForceExpandWidth = (isAutoAlignment || isAutoSizingWidth);
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
            layoutComponent.childControlHeight = isActive;
            return this;
        }

        public EgHorizontalLayoutView SetAutoSizingWidth(bool isActive = true)
        {
            layoutComponent.childControlWidth = isActive;
            layoutComponent.childForceExpandWidth = isActive;
            return this;
        }

        public EgHorizontalLayoutView SetAutoHorizontalAlignment(bool isActive = true)
        {
            layoutComponent.childForceExpandWidth = isActive;
            return this;
        }

        public EgHorizontalLayoutView SetChildAlignment(TextAnchor textAnchor)
        {
            layoutComponent.childAlignment = textAnchor;
            return this;
        }

        public EgHorizontalLayoutView SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
        {
            layoutComponent.padding.left = left ?? PaddingLeft;
            layoutComponent.padding.right = right ?? PaddingRight;
            layoutComponent.padding.top = top ?? PaddingTop;
            layoutComponent.padding.bottom = bottom ?? PaddingBottom;
            return this;
        }

        public EgHorizontalLayoutView SetSpacing(float spacing)
        {
            layoutComponent.spacing = spacing;
            return this;
        }
    }
}