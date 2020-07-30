using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using SGUI.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;

namespace SGUI.GameObjects
{
    public class SSubCanvas : SGameObject
    {

        public SSubCanvas (
            SGameObject parent
        ) : this(parent, 0, 0, 1, 1) { }

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


        #region  RequiredMethods
        public new SSubCanvas SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            return base.SetBackGroundColor(colorType, alpha) as SSubCanvas;
        }

        public new SSubCanvas SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SSubCanvas;
        }

        public new SSubCanvas SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SSubCanvas;
        }

        public new SSubCanvas SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as SSubCanvas;
        }

        public new SSubCanvas SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as SSubCanvas;
        }

        public new SSubCanvas SetLocalPos(float width, float height)
        {
            return base.SetLocalPos(width, height) as SSubCanvas;
        }

        public new SSubCanvas SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SSubCanvas;
        }

        public new SSubCanvas SetAnchorType(AnchorType anchorType)
        {
            return base.SetAnchorType(anchorType) as SSubCanvas;
        }
        #endregion

    }
}