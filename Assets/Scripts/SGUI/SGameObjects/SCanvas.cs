using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.SCanvases;
using SGUI;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace SGUI.SGameObjects
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

        public new SCanvas SetBackGroundColor (ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor (colorType, alpha) as SCanvas;
        }

        public new SCanvas SetBackGroundColor (Color color)
        {
            return base.SetBackGroundColor (color) as SCanvas;
        }

        public new SCanvas SetParentSGameObject (SGameObject parent)
        {
            return base.SetParentSGameObject (parent) as SCanvas;
        }

        public new SCanvas SetRectSizeByRatio (float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio (ratioX, ratioY) as SCanvas;
        }

        public new SCanvas SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SCanvas;
        }

    }
}