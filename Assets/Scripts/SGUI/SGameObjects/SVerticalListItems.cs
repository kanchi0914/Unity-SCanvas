using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts.Extensions;
using SGUI.Base;
using SGUI.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;

namespace SGUI.SGameObjects
{
    public class SVerticalListItems : SGameObject
    {
        private SGameObject parent;



        public List<SGameObject> childrenObjects = new List<SGameObject> ();

        private int paddingLeft = 0;
        private int paddingRight = 0;
        private int paddingTop = 0;
        private int paddingBottom = 0;
        private int spacing = 0;
        private int rowSize;

        public SVerticalListItems (
            SGameObject parent,
            string name = "SVerticalListItems",
            int rowSize = 10
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateVerticalLayoutView (parent.GameObject, name);
            })
        )
        {
            this.rowSize = rowSize;
        }

        public SVerticalListItems (
            SGameObject parent,
            float posRatioX,
            float posRatioY,
            float ratioX,
            float ratioY
        ) : base (parent, "SVerticalListItems",
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateVerticalLayoutView (parent.GameObject, "SVerticalListItems");
            })
        )
        {
            SetRectSizeByRatio (ratioX, ratioY);
            SetLocalPosByRatio (posRatioX, posRatioY);
        }

        public SVerticalListItems (
            SGameObject parent,
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
        }

        public SVerticalListItems SetVerticalListItems (
            List<SGameObject> sGameObjects,
            int rowSize = 10,
            float widthRatio = 1.0f,
            TextAnchor textAnchor = TextAnchor.UpperLeft
        )
        {
            this.childrenObjects = sGameObjects;
            SetListLayout (textAnchor);
            SetChildrenSize (1);
            sGameObjects.ForEach (
                s => { s.SetParentSGameObject (this); });
            return this;
        }

        public void SetChildrenSize (float widthRatio)
        {
            var margin = paddingTop + paddingBottom + (rowSize - 1) * spacing;
            foreach (var item in childrenObjects.Select ((Value, Index) => new { Value, Index }))
            {
                var child = item.Value;
                var layoutElement = child.GameObject.GetComponent<LayoutElement> ();
                if (item.Index == 0) child.SetLocalPos (paddingLeft, paddingTop);
                else child.SetLocalPos (paddingLeft, 0);
                if (layoutElement != null && layoutElement.minHeight != 0)
                {
                    child.RectSize = new Vector2(widthRatio * RectSize.x, layoutElement.minHeight);
                }
                else
                {
                    child.RectSize = new Vector2(
                        widthRatio * RectSize.x - paddingRight,
                        (this.RectSize.y - margin) / rowSize
                    );
                }
            }
        }


        public void AddChild (SGameObject sGameObject)
        {
            sGameObject.SetParentSGameObject (this);
            childrenObjects.Add (sGameObject);
            SetChildrenSize (1);
        }

        public void SetPadding (int left, int right, int top, int bottom)
        {
            var layout = gameObject.GetComponent<VerticalLayoutGroup> ();
            paddingLeft = layout.padding.left = left;
            paddingRight = layout.padding.right = right;
            paddingTop = layout.padding.top = top;
            paddingBottom = layout.padding.bottom = bottom;
        }

        public void SetSpacing (int spacing)
        {
            var layout = gameObject.GetComponent<VerticalLayoutGroup> ();
            layout.spacing = spacing;
            this.spacing = spacing;
        }

        private void SetListLayout (TextAnchor textAnchor)
        {
            var layout = GameObject.AddComponent<VerticalLayoutGroup> ();
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;
            layout.childControlHeight = false;
            layout.childControlWidth = false;
            layout.childAlignment = textAnchor;
        }







        #region  RequiredMethods

        public new SVerticalListItems SetBackGroundColor (ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor (colorType, alpha) as SVerticalListItems;
        }

        public new SVerticalListItems SetBackGroundColor (Color color)
        {
            return base.SetBackGroundColor (color) as SVerticalListItems;
        }

        public new SVerticalListItems SetParentSGameObject (SGameObject parent)
        {
            return base.SetParentSGameObject (parent) as SVerticalListItems;
        }

        public new SVerticalListItems SetRectSizeByRatio (float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio (ratioX, ratioY) as SVerticalListItems;
        }

        public new SVerticalListItems SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SVerticalListItems;
        }

        #endregion

    }
}