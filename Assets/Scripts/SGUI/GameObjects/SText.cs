﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace SGUI.GameObjects
{
    public class SText : SGameObject
    {

        public Text TextComponent { get; private set; }

        public SText(
            SGameObject parent,
            string text = "",
            bool isAutoSizing = false,
            int fontSize = 24,
            ColorType colorType = ColorType.Black
        ) : base(parent, "SText",
            new Func<GameObject>(() =>
           {
               return UIFactory.CreateText(parent.GameObject, "SText", text, fontSize, colorType);
           })
        )
        {
            this.TextComponent = gameObject.GetComponent<Text>();
            TextComponent.resizeTextForBestFit = isAutoSizing;
        }

        public SText SetTextConfig(
            int fontSize, ColorType color, string fontName = null)
        {
            var text = gameObject.GetComponent<Text>();
            text.fontSize = fontSize;
            text.color = Utils.GetColor(color, 1f);
            if (fontName != null)
            {
                var font = Resources.Load(fontName) as Font;
                text.font = font;
            }
            return this;
        }

        public SText SetTextConfig(
            int fontSize, ColorType color, Font font)
        {
            var text = gameObject.GetComponent<Text>();
            text.fontSize = fontSize;
            text.color = Utils.GetColor(color, 1f);
            text.font = font;
            return this;
        }

        public SText SetFont(string fontName = null)
        {
            if (fontName != null)
            {
                var font = Resources.Load(fontName) as Font;
                TextComponent.font = font;
            }
            return this;
        }

        public SText SetTextColor(ColorType color, float alpha)
        {
            TextComponent.color = Utils.GetColor(color, alpha);
            return this;
        }

        public SText SetTextColor(Color color)
        {
            TextComponent.color = color;
            return this;
        }

        public SText SetFontSize(int fontSize)
        {
            TextComponent.fontSize = fontSize;
            return this;
        }

        public SText SetFontStyle(FontStyle fontStyle)
        {
            TextComponent.fontStyle = FontStyle.Italic;
            return this;
        }

        /// <summary>
        /// Set text.
        /// </summary>
        /// <param name="_text"></param>
        /// <returns></returns>
        public SText SetText(string _text)
        {
            var text = gameObject.GetComponent<Text>();
            text.text = _text;
            return this;
        }

        /// <summary>
        /// Set text alignment.
        /// </summary>
        /// <param name="textAnchor"></param>
        /// <returns></returns>
        public SText SetTextAnchor(TextAnchor textAnchor)
        {
            var text = gameObject.GetComponent<Text>();
            text.alignment = textAnchor;
            return this;
        }


        #region  RequiredMethods

        public new SText SetBackGroundColor(ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor(colorType, alpha) as SText;
        }

        public new SText SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SText;
        }

        public new SText SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SText;
        }

        public new SText SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as SText;
        }

        public new SText SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SText;
        }

        #endregion

    }

}