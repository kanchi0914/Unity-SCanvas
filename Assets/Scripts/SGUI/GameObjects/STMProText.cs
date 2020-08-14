﻿// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using EGUI.Base;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// namespace EGUI.GameObjects
// {
//     public class STMProText : EGGameObject
//     {
//
//         public TextMeshProUGUI TextComponent { get; private set; }
//
//         public STMProText(
//             EGGameObject parent,
//             string name = "SText",
//             string text = "",
//             int fontSize = 24,
//             ColorType colorType = ColorType.Black
//         ) : base(parent, name,
//             new Func<GameObject>(() =>
//            {
//                return UIFactory.CreateTMProText(parent.GameObject, name);
//            })
//         )
//         {
//             TextComponent = gameObject.GetComponent<TextMeshProUGUI>();
//         }
//
//         public STMProText SetTextConfig(
//             int fontSize, ColorType color, string fontName = null)
//         {
//             var text = gameObject.GetComponent<Text>();
//             text.fontSize = fontSize;
//             text.color = Utils.GetColor(color, 1f);
//             if (fontName != null)
//             {
//                 var font = Resources.Load(fontName) as Font;
//                 text.font = font;
//             }
//             return this;
//         }
//
//         public STMProText SetFont(string fontName = null)
//         {
//             if (fontName != null)
//             {
//                 var font = Resources.Load(fontName) as TMP_FontAsset;
//                 TextComponent.font = font;
//             }
//             return this;
//         }
//
//         public STMProText SetColor(ColorType color, float alpha)
//         {
//             TextComponent.color = Utils.GetColor(color, alpha);
//             return this;
//         }
//
//         public STMProText SetFontSize(int fontSize)
//         {
//             TextComponent.fontSize = fontSize;
//             return this;
//         }
//
//         public STMProText SetFontStyle(FontStyles fontStyle)
//         {
//             TextComponent.fontStyle = fontStyle;
//             return this;
//         }
//
//         /// <summary>
//         /// Set text
//         /// </summary>
//         /// <param name="_text"></param>
//         /// <returns></returns>
//         public STMProText SetText(string _text)
//         {
//             var text = gameObject.GetComponent<Text>();
//             text.text = _text;
//             return this;
//         }
//
//         /// <summary>
//         /// Set text alignment
//         /// </summary>
//         /// <param name="textAnchor"></param>
//         /// <returns></returns>
//         public STMProText SetAlignMent(TextAnchor textAnchor)
//         {
//             var text = gameObject.GetComponent<Text>();
//             text.alignment = textAnchor;
//             return this;
//         }
//
//
//         #region  RequiredMethods
//
//         public new EGText SetBackGroundColor(ColorType colorType, float alpha)
//         {
//             return base.SetBackGroundColor(colorType, alpha) as EGText;
//         }
//
//         public new EGText SetBackGroundColor(Color color)
//         {
//             return base.SetBackGroundColor(color) as EGText;
//         }
//
//         public new EGText SetParentSGameObject(EGGameObject parent)
//         {
//             return base.SetParentSGameObject(parent) as EGText;
//         }
//
//         public new EGText SetRectSizeByRatio(float ratioX, float ratioY)
//         {
//             return base.SetRectSizeByRatio(ratioX, ratioY) as EGText;
//         }
//
//         public new EGText SetLocalPosByRatio(float posXratio, float posYratio)
//         {
//             return base.SetLocalPosByRatio(posXratio, posYratio) as EGText;
//         }
//
//         #endregion
//
//     }
//
// }