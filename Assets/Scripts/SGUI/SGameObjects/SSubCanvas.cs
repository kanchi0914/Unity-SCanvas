using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using SGUI.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGUI.SGameObjects
{
    public class SSubCanvas : SGameObject
    {
        // private SGameObject parent;

        public SSubCanvas (
            SGameObject parent,
            string name
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateBaseRect (parent.GameObject, name);
            })
        ) { }

        public SSubCanvas (
            SGameObject parent,
            float posRatioX,
            float posRatioY,
            float ratioX,
            float ratioY
        ) : base (parent, "SSubCanvas",
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateBaseRect (parent.GameObject, "SSubCanvas");
            })
        )
        {
            SetRectSizeByRatio (ratioX, ratioY);
            SetLocalPosByRatio (posRatioX, posRatioY);
        }

        public SSubCanvas (
            SGameObject parent,
            string name,
            float posRatioX,
            float posRatioY,
            float ratioX,
            float ratioY
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateBaseRect (parent.GameObject, name);
            })
        )
        {
            SetRectSizeByRatio (ratioX, ratioY);
            SetLocalPosByRatio (posRatioX, posRatioY);
        }

                #region  RequiredMethods

        public new SSubCanvas SetBackGroundColor (ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor (colorType, alpha) as SSubCanvas;
        }

        public new SSubCanvas SetBackGroundColor (Color color)
        {
            return base.SetBackGroundColor (color) as SSubCanvas;
        }

        public new SSubCanvas SetParentSGameObject (SGameObject parent)
        {
            return base.SetParentSGameObject (parent) as SSubCanvas;
        }

        public new SSubCanvas SetRectSizeByRatio (float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio (ratioX, ratioY) as SSubCanvas;
        }

        public new SSubCanvas SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SSubCanvas;
        }

        #endregion

        // public string name { get; private set; } = "SubCanvas";

        // public float posRatioX { get; private set; } = 0.0f;
        // public float posRatioY { get; private set; } = 0.0f;
        // public float widthRatio { get; private set; } = 0.2f;
        // public float heightRatio { get; private set; } = 0.2f;

        // // public float LocalPosX

        // public int width { get; private set; } = 100;

        // public int height { get; private set; } = 100;

        // public void ClearComponents ()
        // {
        //     foreach (Transform child in gameObject.transform)
        //     {
        //         GameObject.Destroy (child.gameObject);
        //     }
        //     var layout0 = gameObject.GetComponent<GridLayoutGroup> ();
        //     //GameObject.DestroyImmediate(layout0, true); 
        //     Destroy (layout0);
        // }

        // IEnumerator Destroy (Component component)
        // {
        //     yield return new WaitForEndOfFrame ();
        //     Destroy (component);
        // }

        // private void SetListLayout (TextAnchor textAnchor)
        // {
        //     var layout = GameObject.AddComponent<VerticalLayoutGroup> ();
        //     layout.childForceExpandHeight = false;
        //     layout.childForceExpandWidth = false;
        //     layout.childControlHeight = false;
        //     layout.childControlWidth = false;
        //     layout.childAlignment = textAnchor;
        // }

        // private void SetGridLayout (int columnSize, int rowSize)
        // {
        //     GridLayoutGroup layout = gameObject.GetComponent<GridLayoutGroup> ();
        //     if (layout == null)
        //     {
        //         layout = gameObject.AddComponent<GridLayoutGroup> ();
        //     }
        //     if (columnSize > 0)
        //     {
        //         layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        //         layout.constraintCount = columnSize;
        //         layout.cellSize = new Vector2 (
        //             RectSize.x / columnSize, RectSize.y / rowSize);
        //     }
        // }

        // public void SetText (string _text)
        // {
        //     var textObj = UIFactory.CreateText (this.GameObject, _text);
        // }

        // public void SetButton (string _text)
        // {
        //     UIFactory.CreateButton (this.GameObject, _text);
        // }

        // public void SetVerticalListItems<T> (
        //     List<T> sGameObjects,
        //     int rowSize = 10,
        //     float widthXratio = 0.5f,
        //     TextAnchor textAnchor = TextAnchor.UpperLeft
        // ) where T : SGameObject
        // {
        //     SetListLayout (textAnchor);
        //     sGameObjects.ForEach (
        //         s =>
        //         {
        //             s.SetParentSGameObject (this);
        //             var layoutElement = s.GameObject.GetComponent<LayoutElement> ();
        //             if (layoutElement != null && layoutElement.minHeight != 0)
        //             {
        //                 s.RectSize = (widthXratio * this.RectSize.x, layoutElement.minHeight);
        //             }
        //             else
        //             {
        //                 s.RectSize = (widthXratio * this.RectSize.x, this.RectSize.y / rowSize);
        //             }
        //         });
        // }

        // public void SetGridListItems<T> (
        //     List<T> sGameObjects,
        //     int columnSize, int rowSize
        // ) where T : SGameObject
        // {
        //     SetGridLayout (columnSize, rowSize);
        //     sGameObjects.ForEach (
        //         s =>
        //         {
        //             s.SetParentSGameObject (this);
        //         });
        // }

        // public void SetGridButtons(
        //     List<SButton> sButtons,
        //     int columnSize, int rowSize
        //     )
        // {
        //     SetGridLayout(columnSize, rowSize);
        //     sButtons.ForEach(sb => sb.Button.transform.SetParent(GameObject.transform, false));
        // }

    }
}