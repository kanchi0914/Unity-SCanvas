using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace SGUI.SGameObjects
{
    public class SText : SGameObject
    {
        public SText (
            SGameObject parent,
            string name = "SText",
            string text = "",
            int fontSize = 24,
            ColorType colorType = ColorType.Black
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateText (parent.GameObject, name, text, fontSize, colorType);
            })
        ) { }

        public SText SetTextConfig (
            int fontSize, ColorType color, string fontName = null)
        {
            var text = gameObject.GetComponent<Text> ();
            text.fontSize = fontSize;
            text.color = Utils.GetColor (color, 1f);
            if (fontName != null) {
                var font = Resources.Load(fontName) as Font;
                text.font = font;
            }
            return this;
        }

        public SText SetText (string _text)
        {
            var text = gameObject.GetComponent<Text> ();
            text.text = _text;
            return this;
        }


        public SText SetAlignMent(TextAnchor textAnchor){
            var text = gameObject.GetComponent<Text> ();
            text.alignment = textAnchor;
            return this;
        }

    }

}