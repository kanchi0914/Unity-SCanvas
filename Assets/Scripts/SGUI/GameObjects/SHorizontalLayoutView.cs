using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts.Extensions;
using SGUI.Base;
using SGUI.Base;
using SGUI.GameObjects.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;

namespace SGUI.GameObjects
{
    public class SHorizontalLayoutView : SGameObject, ILayoutObject
    {
        private SGameObject parent;

        private HorizontalLayoutGroup layout;

        public List<SGameObject> childrenObjects = new List<SGameObject>();

        private int paddingLeft = 0;
        private int paddingRight = 0;
        private int paddingTop = 0;
        private int paddingBottom = 0;
        private int spacing = 0;
        private int columnSize;
        bool isAutoSizingWidth = false;
        bool isAutoAlignMent = false;


        public SHorizontalLayoutView(
            SGameObject parent,
            bool isAutoSizingWidth = true,
            bool isAutoSizingHeight = true,
            bool isAutoAlignment = false
        ) : this(parent, "SHorizontalLayoutView", 0, 0, 1, 1, isAutoSizingWidth, isAutoSizingHeight, isAutoAlignment) { }

        public SHorizontalLayoutView(
            SGameObject parent,
            string name,
            float posRatioX,
            float posRatioY,
            float ratioX,
            float ratioY,
            bool isAutoSizingWidth = false,
            bool isAutoSizingHeight = false,
            bool isAutoAlignment = false
        ) : base(parent, name,
            new Func<GameObject>(() =>
           {
               return UIFactory.CreateHotizontalLayoutView(parent.GameObject, name);
           })
        )
        {
            SetRectSizeByRatio(ratioX, ratioY);
            SetLocalPosByRatio(posRatioX, posRatioY);
            layout = gameObject.GetComponent<HorizontalLayoutGroup>();

            layout.childControlWidth = isAutoSizingWidth;
            layout.childControlHeight = isAutoSizingHeight;
            layout.childForceExpandHeight = isAutoSizingHeight;
            layout.childForceExpandWidth = (isAutoAlignment || isAutoSizingWidth);
        }

        public void AddItem(SGameObject sGameObject)
        {
            gameObject.transform.SetParent(gameObject.transform, false);
            sGameObject.ParentSGameObject = this;
            childrenObjects.Add(sGameObject);
        }


        public SHorizontalLayoutView SetAutoSizingHeight(bool isActive = true)
        {
            layout.childControlHeight = isActive;
            return this;
        }

        public SHorizontalLayoutView SetAutoSizingWidth(bool isActive = true)
        {
            layout.childControlWidth = isActive;
            layout.childForceExpandWidth = isActive;
            return this;
        }

        public SHorizontalLayoutView SetAutoHorizontalAlinment(bool isActive = true)
        {
            layout.childForceExpandWidth = isActive;
            return this;
        }

        public SHorizontalLayoutView SetChildAlignment(TextAnchor textAnchor)
        {
            layout.childAlignment = textAnchor;
            return this;
        }

        public SHorizontalLayoutView SetPadding(int left, int right, int top, int bottom)
        {
            var layout = gameObject.GetComponent<HorizontalLayoutGroup>();
            paddingLeft = layout.padding.left = left;
            paddingRight = layout.padding.right = right;
            paddingTop = layout.padding.top = top;
            paddingBottom = layout.padding.bottom = bottom;
            return this;
        }

        public SHorizontalLayoutView SetSpacing(int spacing)
        {
            var layout = gameObject.GetComponent<HorizontalLayoutGroup>();
            layout.spacing = spacing;
            this.spacing = spacing;
            return this;
        }

        #region  RequiredMethods

        public new SHorizontalLayoutView SetBackGroundColor(ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor(colorType, alpha) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetLocalPos(float width, float height)
        {
            return base.SetLocalPos(width, height) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetAnchorType(AnchorType anchorType)
        {
            return base.SetAnchorType(anchorType) as SHorizontalLayoutView;
        }

        #endregion

    }
}