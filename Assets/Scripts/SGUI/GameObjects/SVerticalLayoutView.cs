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
    public class SVerticalLayoutView : SGameObject, ILayoutObject
    {
        private SGameObject parent;
        public List<SGameObject> childrenObjects = new List<SGameObject> ();

        private int paddingLeft = 0;
        private int paddingRight = 0;
        private int paddingTop = 0;
        private int paddingBottom = 0;
        private int spacing = 0;
        private int rowSize = 10;

        public SVerticalLayoutView (
            SGameObject parent,
            int rowSize = 10
        ) : this (parent, rowSize, "SVerticalListView", 0, 0, 1, 1) { }

        public SVerticalLayoutView (
            SGameObject parent,
            int rowSize,
            string name,
            float posRatioX,
            float posRatioY,
            float ratioX,
            float ratioY
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateVerticalLayoutView (parent.GameObject, name);
            })
        )
        {
            SetRectSizeByRatio (ratioX, ratioY);
            SetLocalPosByRatio (posRatioX, posRatioY);
            this.rowSize = rowSize;
        }

        private void SetChildAlignment (TextAnchor textAnchor)
        {
            var layout = GameObject.TryAddComponent<VerticalLayoutGroup> ();
            layout.childAlignment = textAnchor;
        }

        private void SetChildrenSize ()
        {
            var margin = paddingTop + paddingBottom + (rowSize - 1) * spacing;
            foreach (var item in childrenObjects.Select ((Value, Index) => new { Value, Index }))
            {
                var child = item.Value;
                var height = (this.RectSize.y - margin) / rowSize;
                if (height < 0) height = 1;
                child.RectSize = new Vector2 (
                    1, height
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

        public SVerticalLayoutView SetPadding (int left, int right, int top, int bottom)
        {
            var layout = gameObject.GetComponent<VerticalLayoutGroup> ();
            paddingLeft = layout.padding.left = left;
            paddingRight = layout.padding.right = right;
            paddingTop = layout.padding.top = top;
            paddingBottom = layout.padding.bottom = bottom;
            return this;
        }

        public SVerticalLayoutView SetSpacing (int spacing)
        {
            var layout = gameObject.GetComponent<VerticalLayoutGroup> ();
            layout.spacing = spacing;
            this.spacing = spacing;
            return this;
        }

        #region  RequiredMethods

        public new SVerticalLayoutView SetBackGroundColor (ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor (colorType, alpha) as SVerticalLayoutView;
        }

        public new SVerticalLayoutView SetBackGroundColor (Color color)
        {
            return base.SetBackGroundColor (color) as SVerticalLayoutView;
        }

        public new SVerticalLayoutView SetParentSGameObject (SGameObject parent)
        {
            return base.SetParentSGameObject (parent) as SVerticalLayoutView;
        }

        public new SVerticalLayoutView SetRectSizeByRatio (float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio (ratioX, ratioY) as SVerticalLayoutView;
        }

        public new SVerticalLayoutView SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SVerticalLayoutView;
        }

        #endregion

    }
}