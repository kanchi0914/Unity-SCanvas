using EGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGText : EGGameObject
    {
        public Text TextComponent { get; private set; }

        public EGText
        (
            EGGameObject parent,
            string text = "",
            bool isAutoSizing = false,
            int fontSize = 24,
            ColorType colorType = ColorType.Black,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "EGText"
        ) : base
        (
            parent,
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            "EGText",
            () => UIFactory.CreateText(parent.GameObject, "EGText", text, fontSize, colorType)
        )
        {
            TextComponent = gameObject.GetComponent<Text>();
            TextComponent.resizeTextForBestFit = isAutoSizing;
        }

        public EGText SetTextConfig(
            int fontSize, ColorType color, string fontName = null)
        {
            var text = gameObject.GetComponent<Text>();
            text.fontSize = fontSize;
            text.color = Utils.GetColor(color, 1f);
            if (fontName == null) return this;
            var font = Resources.Load(fontName) as Font;
            text.font = font;
            return this;
        }

        public EGText SetTextConfig(
            int fontSize, ColorType color, Font font)
        {
            var text = gameObject.GetComponent<Text>();
            text.fontSize = fontSize;
            text.color = Utils.GetColor(color, 1f);
            text.font = font;
            return this;
        }

        public EGText SetFont(string fontName = null)
        {
            if (fontName != null)
            {
                var font = Resources.Load(fontName) as Font;
                TextComponent.font = font;
            }
            return this;
        }

        public EGText SetTextColor(ColorType color, float alpha)
        {
            TextComponent.color = Utils.GetColor(color, alpha);
            return this;
        }

        public EGText SetTextColor(Color color)
        {
            TextComponent.color = color;
            return this;
        }

        public EGText SetFontSize(int fontSize)
        {
            TextComponent.fontSize = fontSize;
            return this;
        }

        public EGText SetFontStyle(FontStyle fontStyle)
        {
            TextComponent.fontStyle = FontStyle.Italic;
            return this;
        }

        /// <summary>
        /// Set text.
        /// </summary>
        /// <param name="_text"></param>
        /// <returns></returns>
        public EGText SetText(string _text)
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
        public EGText SetTextAlignment(TextAnchor textAnchor)
        {
            var text = gameObject.GetComponent<Text>();
            text.alignment = textAnchor;
            return this;
        }
    }
}