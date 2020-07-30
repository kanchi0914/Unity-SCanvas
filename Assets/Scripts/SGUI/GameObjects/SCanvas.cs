using System;
using System.Collections.Generic;

using SGUI;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;

namespace SGUI.GameObjects
{
    public class SCanvas : SGameObject
    {

        public SCanvas (string name = "SCanvas", bool isStatic = false) 
        : base (
            null,
            name, 
            new Func<GameObject>(() => {
                return UIFactory.CreateCanvas(name);
            }))
        {
            if (isStatic)
            {
                gameObject.GetComponent<Canvas>().sortingOrder
                = 1000;
            }
            else 
            {
                CanvasStack.Push (this);
            }
        }

        public new SCanvas SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            return base.SetBackGroundColor(colorType, alpha) as SCanvas;
        }

        public new SCanvas SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SCanvas;
        }

        public new SCanvas SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SCanvas;
        }

        public new SCanvas SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as SCanvas;
        }

        public new SCanvas SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as SCanvas;
        }

        public new SCanvas SetLocalPos(float width, float height)
        {
            return base.SetLocalPos(width, height) as SCanvas;
        }

        public new SCanvas SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SCanvas;
        }

        public new SCanvas SetAnchorType(AnchorType anchorType)
        {
            return base.SetAnchorType(anchorType) as SCanvas;
        }

    }
}