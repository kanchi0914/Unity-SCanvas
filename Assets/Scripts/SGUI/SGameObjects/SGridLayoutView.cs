using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace SGUI.SGameObjects
{
    public class SGridLayoutView : SGameObject
    {

        public SGridLayoutView (
            SGameObject parent,
            int rowCount,
            int columnCount
        ) : this (parent, "SGridLayoutView", rowCount, columnCount, 0, 0, 1, 1) { }

        public SGridLayoutView (
            SGameObject parent,
            string name,
            int rowCount,
            int columnCount,
            float posRatioX,
            float posRatioY,
            float ratioX,
            float ratioY
        ) : base (parent, "SGridLayoutView",
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateGridLayoutView (parent.GameObject, "SVerticalListItems");
            })
        )
        {
            SetRectSizeByRatio (ratioX, ratioY);
            SetLocalPosByRatio (posRatioX, posRatioY);
        }

        private void SetGridLayout (int columnSize, int rowSize)
        {
            GridLayoutGroup layout = gameObject.GetComponent<GridLayoutGroup> ();
            if (layout == null)
            {
                layout = gameObject.AddComponent<GridLayoutGroup> ();
            }
            if (columnSize > 0)
            {
                layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                layout.constraintCount = columnSize;
                layout.cellSize = new Vector2 (
                    RectSize.x / columnSize, RectSize.y / rowSize);
            }
        }

        public void SetGridListItems (
            List<SGameObject> sGameObjects,
            int columnSize, int rowSize
        )
        {
            SetGridLayout (columnSize, rowSize);
            sGameObjects.ForEach (
                s =>
                {
                    s.SetParentSGameObject (this);
                });
        }

        public SGridLayoutView SetVerticalListItems (
            List<SGameObject> sGameObjects,
            int rowSize = 10,
            float widthRatio = 1.0f,
            TextAnchor textAnchor = TextAnchor.UpperLeft
        )
        {
            SetListLayout (textAnchor);
            sGameObjects.ForEach (
                s =>
                {
                    // s.SetParentSGameObject (this);
                    var layoutElement = s.GameObject.GetComponent<LayoutElement> ();
                    if (layoutElement != null && layoutElement.minHeight != 0)
                    {
                        s.RectSize = new Vector2 (widthRatio * RectSize.x, layoutElement.minHeight);
                    }
                    else
                    {
                        s.RectSize = new Vector2 (widthRatio * RectSize.x, this.RectSize.y / rowSize);
                    }
                });
            return this as SGridLayoutView;
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

        public new SGridLayoutView SetBackGroundColor (ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor (colorType, alpha) as SGridLayoutView;
        }

        public new SGridLayoutView SetBackGroundColor (Color color)
        {
            return base.SetBackGroundColor (color) as SGridLayoutView;
        }

        public new SGridLayoutView SetParentSGameObject (SGameObject parent)
        {
            return base.SetParentSGameObject (parent) as SGridLayoutView;
        }

        public new SGridLayoutView SetRectSizeByRatio (float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio (ratioX, ratioY) as SGridLayoutView;
        }

        public new SGridLayoutView SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SGridLayoutView;
        }

        #endregion

    }
}