// using System;
// using Assets.Scripts.Extensions;
// using Assets.Scripts.SGUI.Base;
// using EGUI.Base;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
//
// namespace EGUI.GameObjects
// {
//     public class EGText : EGGameObject
//     {
//         public Text TextComponent { get; private set; }
//         
//         public Color DefaultTextColor { get; set; } = Color.black;
//
//         public EGText
//         (
//             EGGameObject parent,
//             string text = "",
//             bool isAutoSizing = false,
//             int fontSize = -1,
//             Color? color = null,
//             string name = "EGText"
//         ) : base
//         (
//             parent,
//             "EGText"
//         )
//         {
//             TextComponent = gameObject.TryAddComponent<Text>();
//             TextComponent.text = text;
//             TextComponent.fontSize = (fontSize > 0) ? fontSize : Utils.DefaultFontSize;
//             TextComponent.alignment = TextAnchor.MiddleCenter;
//             TextComponent.font = UGUIResources.Font;
//             TextComponent.color = color ?? Color.black;
//             TextComponent.resizeTextForBestFit = isAutoSizing;
//         }
//
//         public EGText SetTextConfig(
//             int? fontSize = null, 
//             Color? color = null, 
//             float? alpha = null, 
//             Font font = null, 
//             TextAnchor? alignment = null)
//         {
//             TextComponent.fontSize = fontSize ?? TextComponent.fontSize;
//             SetTextColor(color ?? TextComponent.color, alpha);
//             TextComponent.alignment = alignment ?? TextComponent.alignment;
//             TextComponent.font = font ?? TextComponent.font;
//             return this;
//         }
//
//         public EGText SetFontByFilePath(string fontFilePath)
//         {
//             var font = Resources.Load<Font>(fontFilePath);
//             TextComponent.font = font ?? TextComponent.font;
//             return this;
//         }
//         
//         public EGText SetFontStyle(FontStyle fontStyle)
//         {
//             TextComponent.fontStyle = fontStyle;
//             return this;
//         }
//         
//         public EGText SetTextColor(Color color, float? alpha = null)
//         {
//             TextComponent.color = new Color(color.r, color.g, color.b, alpha ?? color.a);
//             DefaultTextColor = TextComponent.color;
//             return this;
//         }
//
//         /// <summary>
//         /// Set text.
//         /// </summary>
//         /// <param name="_text"></param>
//         /// <returns></returns>
//         public EGText SetText(string _text)
//         {
//             var text = gameObject.GetComponent<Text>();
//             text.text = _text;
//             return this;
//         }
//
//         /// <summary>
//         /// Set text alignment.
//         /// </summary>
//         /// <param name="textAnchor"></param>
//         /// <returns></returns>
//         public EGText SetTextAlignment(TextAnchor textAnchor)
//         {
//             var text = gameObject.GetComponent<Text>();
//             text.alignment = textAnchor;
//             return this;
//         }
//         
//         // selectable
//
//         public EGText SetPointerEnteredTextColor(Color color)
//         {
//             AddEvent(EventTriggerType.PointerEnter, () =>  TextComponent.color = color);
//             AddEvent(EventTriggerType.PointerExit, () => TextComponent.color = DefaultTextColor);
//             return this;
//         }
//         
//         public EGText AddOnClick(Action action)
//         {
//             var entry = new EventTrigger.Entry();
//             entry.eventID = EventTriggerType.PointerClick;
//             entry.callback.AddListener(e => action.Invoke());
//             var trigger = gameObject.TryAddComponent<EventTrigger>();
//             trigger.triggers.Add(entry);
//             return this;
//         }
//
//         public EGText AddEvent(EventTriggerType type, Action action)
//         {
//             var entry = new EventTrigger.Entry();
//             entry.eventID = type;
//             entry.callback.AddListener(e => action.Invoke());
//             var trigger = gameObject.TryAddComponent<EventTrigger>();
//             trigger.triggers.Add(entry);
//             return this;
//         }
//     }
// }