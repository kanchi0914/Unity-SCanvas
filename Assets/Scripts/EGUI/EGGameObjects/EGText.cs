using System;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using EGUI.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGText : EGGameObject
    {
        public Text TextComponent { get; private set; }

        public Color DefaultTextColor { get; set; } = Color.black;

        public EGText
        (
            GameObject parent,
            string text,
            string name = "EGText"
        ) : base
        (
            parent,
            "EGText"
        )
        {
            TextComponent = gameObject.GetOrAddComponent<Text>();
            SetText(text);
            SetTextConfig(
                fontSize: Utils.DefaultFontSize, alignment: TextAnchor.MiddleCenter,
                font: UGUIResources.Font, color: Color.black);
        }

        /// <summary>
        /// Set text.
        /// </summary>
        /// <param name="_text"></param>
        /// <returns></returns>
        public EGText SetText(string text)
        {
            gameObject.GetComponent<Text>().text = text;
            return this;
        }

        public EGText SetTextConfig(
            int? fontSize = null,
            bool? isAutoSizing = null,
            Color? color = null,
            float? colorAlpha = null,
            Font font = null,
            TextAnchor? alignment = null,
            FontStyle? fontStyle = null)
        {
            TextComponent.fontSize = fontSize ?? TextComponent.fontSize;
            SetTextColor(color ?? TextComponent.color, colorAlpha);
            TextComponent.alignment = alignment ?? TextComponent.alignment;
            TextComponent.font = font ?? TextComponent.font;
            TextComponent.resizeTextForBestFit = isAutoSizing ?? TextComponent.resizeTextForBestFit;
            TextComponent.fontStyle = fontStyle ?? TextComponent.fontStyle;
            return this;
        }

        public EGText SetTextColor(Color color, float? alpha = null)
        {
            TextComponent.color = new Color(color.r, color.g, color.b, alpha ?? color.a);
            DefaultTextColor = TextComponent.color;
            return this;
        }


        public EGText SetPointerEnteredTextColor(Color color)
        {
            AddEvent(EventTriggerType.PointerEnter, () => TextComponent.color = color);
            AddEvent(EventTriggerType.PointerExit, () => TextComponent.color = DefaultTextColor);
            return this;
        }

        public EGText AddOnClick(Action action)
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener(e => action.Invoke());
            var trigger = gameObject.GetOrAddComponent<EventTrigger>();
            trigger.triggers.Add(entry);
            return this;
        }

        public EGText AddEvent(EventTriggerType type, Action action)
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(e => action.Invoke());
            var trigger = gameObject.GetOrAddComponent<EventTrigger>();
            trigger.triggers.Add(entry);
            return this;
        }
    }
}