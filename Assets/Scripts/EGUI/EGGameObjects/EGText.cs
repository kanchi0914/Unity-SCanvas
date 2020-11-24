using System;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using EGUI.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace EGUI.GameObjects
{
    public class EGText : EGGameObject
    {
        public struct TextPreset
        {
            public Font Font;
            public FontStyle? FontStyle;
            public int? FontSize;
            public int? LineSpacing;
            public bool? SupportRichText;
            public TextAnchor? Alignment;
            public bool? AlignByGeometry;
            public HorizontalWrapMode? HorizontalOverflow;
            public VerticalWrapMode? VerticalOverflow;
            public bool? ResizeTextForBestFit;
            public int? ResizeTextMinSize;
            public int? ResizeTextMaxSize;
            public Color? Color;

            public TextPreset(
                Font font = null, FontStyle? fontStyle = null, int? fontSize = null,
                int? lineSpacing = null, bool? supportRichText = null,
                TextAnchor? alignment = null, bool? alignByGeometry = null,
                HorizontalWrapMode? horizontalOverflow = null,
                VerticalWrapMode? verticalOverflow = null,
                bool? resizeTextForBestFit = null, int? resizeTextMinSize = null,
                int? resizeTextMaxSize = null, Color? color = null
                )
            {
                Font = font;
                FontStyle = fontStyle;
                FontSize = fontSize;
                LineSpacing = lineSpacing;
                SupportRichText = supportRichText;
                Alignment = alignment;
                AlignByGeometry = alignByGeometry;
                HorizontalOverflow = horizontalOverflow;
                VerticalOverflow = verticalOverflow;
                ResizeTextForBestFit = resizeTextForBestFit;
                ResizeTextMinSize = resizeTextMinSize;
                ResizeTextMaxSize = resizeTextMaxSize;
                Color = color;
            }
        }
        
        /// <summary>
        /// Textコンポーネント
        /// </summary>
        public Text TextComponent { get; private set; }

        /// <summary>
        /// デフォルトの色
        /// </summary>
        public Color DefaultTextColor { get; set; } = Utils.DefaultTextColor;

        /// <summary>
        /// TextComponent.raycastTarget
        /// </summary>
        public bool IsRaycastTarget
        {
            get => TextComponent.raycastTarget;
            set => TextComponent.raycastTarget = value;
        }

        /// <summary>
        /// TextComponent.maskable
        /// </summary>
        public bool IsMaskable
        {
            get => TextComponent.maskable;
            set => TextComponent.maskable = value;
        }

        /// <summary>
        /// Textオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="text">テキスト</param>
        public EGText
        (
            EGGameObject parent,
            string text = "",
            bool isAutoSizing = false
        ) : this
        (
            parent.gameObject,
            text, isAutoSizing
        )
        {
        }

        /// <summary>
        /// Textオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="text">テキスト</param>
        /// <param name="name">オブジェクト名</param>
        public EGText
        (
            GameObject parent = null,
            string text = "",
            bool isAutoSizing = false,
            string name = "EGText"
        ) : base
        (
            parent,
            "EGText"
        )
        {
            TextComponent = gameObject.GetOrAddComponent<Text>();
            SetText(text)
                .SetCharacter(fontSize: Utils.DefaultFontSize, font: UGUIResources.Font)
                .SetParagraph(alignment: TextAnchor.MiddleCenter, resizeTextForBestFit: isAutoSizing)
                .SetColor(Utils.DefaultTextColor);
        }

        /// <summary>
        /// テキストを設定
        /// </summary>
        /// <param name="text">テキスト</param>
        /// <returns></returns>
        public EGText SetText(string text)
        {
            gameObject.GetOrAddComponent<Text>().text = text;
            return this;
        }

        /// <summary>
        /// テキストの色と透明度を設定
        /// </summary>
        /// <param name="color"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public EGText SetColor(Color color, float? alpha = null)
        {
            TextComponent.color = new Color(color.r, color.g, color.b, alpha ?? color.a);
            DefaultTextColor = TextComponent.color;
            return this;
        }

        /// <summary>
        /// カーソルが乗ったときのテキストの色を設定
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public EGText SetPointerEnteredTextColor(Color color)
        {
            AddEvent(EventTriggerType.PointerEnter, () => TextComponent.color = color);
            AddEvent(EventTriggerType.PointerExit, () => TextComponent.color = DefaultTextColor);
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

        /// <summary>
        /// TextコンポーネントのCharacter項目以下を設定
        /// </summary>
        /// <param name="font"></param>
        /// <param name="fontStyle"></param>
        /// <param name="fontSize"></param>
        /// <param name="lineSpacing"></param>
        /// <param name="supportRichText"></param>
        /// <returns></returns>
        public EGText SetCharacter(
            Font font = null, FontStyle? fontStyle = null, int? fontSize = null,
            int? lineSpacing = null, bool? supportRichText = null)
        {
            TextComponent.font = font ?? TextComponent.font;
            TextComponent.fontStyle = fontStyle ?? TextComponent.fontStyle;
            TextComponent.fontSize = fontSize ?? TextComponent.fontSize;
            TextComponent.lineSpacing = lineSpacing ?? TextComponent.lineSpacing;
            TextComponent.supportRichText = supportRichText ?? TextComponent.supportRichText;
            return this;
        }

        /// <summary>
        /// TextコンポーネントのParagraph項目以下を設定
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="alignByGeometry"></param>
        /// <param name="horizontalOverflow"></param>
        /// <param name="verticalOverflow"></param>
        /// <param name="resizeTextForBestFit"></param>
        /// <returns></returns>
        public EGText SetParagraph(
            TextAnchor? alignment = null, bool? alignByGeometry = null,
            HorizontalWrapMode? horizontalOverflow = null,
            VerticalWrapMode? verticalOverflow = null,
            bool? resizeTextForBestFit = null,
            int? resizeTextMinSize = null,
            int? resizeTextMaxSize = null
            )
        {
            TextComponent.alignment = alignment ?? TextComponent.alignment;
            TextComponent.alignByGeometry = alignByGeometry ?? TextComponent.alignByGeometry;
            TextComponent.horizontalOverflow = horizontalOverflow ?? TextComponent.horizontalOverflow;
            TextComponent.verticalOverflow = verticalOverflow ?? TextComponent.verticalOverflow;
            TextComponent.resizeTextForBestFit = resizeTextForBestFit ?? TextComponent.resizeTextForBestFit;
            TextComponent.resizeTextMinSize = resizeTextMinSize ?? TextComponent.resizeTextMinSize;
            TextComponent.resizeTextMaxSize = resizeTextMaxSize ?? TextComponent.resizeTextMaxSize;
            return this;
        }

        public EGText SetTextPreset(TextPreset textPreset)
        {
            SetCharacter(textPreset.Font, textPreset.FontStyle, textPreset.FontSize,
                textPreset.LineSpacing, textPreset.SupportRichText);
            SetParagraph(textPreset.Alignment, textPreset.AlignByGeometry, textPreset.HorizontalOverflow,
                textPreset.VerticalOverflow, textPreset.ResizeTextForBestFit, textPreset.ResizeTextMinSize, 
                textPreset.ResizeTextMaxSize);
            if (textPreset.Color != null) SetColor((Color)textPreset.Color);
            return this;
        }
    }
}