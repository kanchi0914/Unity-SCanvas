using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts.Extensions;
using SGUI.Base;
using SGUI.Base;
using SGUI.SGameObjects.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;

namespace SGUI.SGameObjects
{
    public class SHorizontalLayoutView : SGameObject, ILayoutObject
    {
        private SGameObject parent;
        public List<SGameObject> childrenObjects = new List<SGameObject> ();

        private int paddingLeft = 0;
        private int paddingRight = 0;
        private int paddingTop = 0;
        private int paddingBottom = 0;
        private int spacing = 0;
        private int columnSize;

        public SHorizontalLayoutView (
            SGameObject parent,
            int columnSize = 10
        ) : this (parent, columnSize, "SHorizontalLayoutView", 0, 0, 1, 1) { }

        public SHorizontalLayoutView (
            SGameObject parent,
            int columnSize,
            string name,
            float posRatioX,
            float posRatioY,
            float ratioX,
            float ratioY
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateHotizontalLayoutView (parent.GameObject, name);
            })
        )
        {
            SetRectSizeByRatio (ratioX, ratioY);
            SetLocalPosByRatio (posRatioX, posRatioY);
            this.columnSize = columnSize;
        }

        private void SetChildAlignment (TextAnchor textAnchor)
        {
            var layout = GameObject.TryAddComponent<HorizontalLayoutGroup> ();
            layout.childAlignment = textAnchor;
        }

        private void SetChildrenSize ()
        {
            var margin = paddingLeft + paddingRight + (columnSize - 1) * spacing;
            foreach (var item in childrenObjects.Select ((Value, Index) => new { Value, Index }))
            {
                var child = item.Value;
                var width = (this.RectSize.x - margin) / columnSize;
                if (width < 1) width = 1;
                child.RectSize = new Vector2 (
                    width, 1
                );
            }
        }

        public void AddItem (SGameObject sGameObject)
        {
            gameObject.transform.SetParent (gameObject.transform, false);
            sGameObject.ParentSGameObject = this;
            childrenObjects.Add (sGameObject);
            SetChildrenSize ();
        }

        public SHorizontalLayoutView SetPadding (int left, int right, int top, int bottom)
        {
            var layout = gameObject.GetComponent<HorizontalLayoutGroup> ();
            paddingLeft = layout.padding.left = left;
            paddingRight = layout.padding.right = right;
            paddingTop = layout.padding.top = top;
            paddingBottom = layout.padding.bottom = bottom;
            return this;
        }

        public SHorizontalLayoutView SetSpacing (int spacing)
        {
            var layout = gameObject.GetComponent<HorizontalLayoutGroup> ();
            layout.spacing = spacing;
            this.spacing = spacing;
            return this;
        }

        #region  RequiredMethods

        public new SHorizontalLayoutView SetBackGroundColor (ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor (colorType, alpha) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetBackGroundColor (Color color)
        {
            return base.SetBackGroundColor (color) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetParentSGameObject (SGameObject parent)
        {
            return base.SetParentSGameObject (parent) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetRectSizeByRatio (float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio (ratioX, ratioY) as SHorizontalLayoutView;
        }

        public new SHorizontalLayoutView SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SHorizontalLayoutView;
        }

        #endregion

    }
}