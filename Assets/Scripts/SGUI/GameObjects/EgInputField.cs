using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using Assets.Scripts.SGUI.SGameObjects.ComponentScripts;
using EGUI.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    class EgInputField : EgImage
    {

        public InputField InputField { get { return inputField; } }

        private InputField inputField;

        public string Text { get { return InputField.text; } }

        EGText placeHolder;
        EGText text;

        public EgInputField(
            EGGameObject parent,
            string name
        ) : base(parent, name)
        {
            Init();
        }

        private void Init()
        {
            SetColor(ColorType.White, 1);
            SetImageSource(UGUIResources.InputField);


            placeHolder = new EGText(this, "PlaceHolder");
            placeHolder.SetAnchorType(Utils.AnchorType.FullStretch);
            placeHolder.SetColor(ColorType.Gray, 0.5f);
            placeHolder.SetText("Enter Text...");
            placeHolder.SetTextColor(ColorType.Gray, 0.6f);
            placeHolder.SetFontStyle(FontStyle.Italic);

            var spacing = 0.1f * RectSize.y;
            placeHolder.SetOffset(spacing, spacing, -spacing, -spacing);
            text = new EGText(this, "Text");
            text.SetFullStretchAnchor();
            text.TextComponent.supportRichText = false;

            inputField = gameObject.AddComponent<InputField>();
            inputField.targetGraphic = Image;
            inputField.textComponent = text.TextComponent;
            inputField.placeholder = placeHolder.TextComponent;

            SetOnSelect(() => Debug.Log("majikijtieee..."));
            SetOnDeselect(() => Debug.Log("それはだめｗｗｗ"));
        }

        public EgInputField AddOnValueChanged(Action action)
        {
            inputField.onValueChanged.AddListener(e => action.Invoke());
            return this;
        }

        public EgInputField AddOnEndEdit(Action action)
        {
            inputField.onEndEdit.AddListener(e => action.Invoke());
            return this;
        }

        public EgInputField SetOnSelect(Action action)
        {
            var onSelectEvent = gameObject.TryAddComponent<OnSelectEventWrapper>();
            onSelectEvent.OnSelectEvent += (g => action.Invoke());
            return this;
        }

        public EgInputField SetOnDeselect(Action action)
        {
            var onDeselectEvent = gameObject.TryAddComponent<OnDeselectWrapper>();
            onDeselectEvent.OnDeselectEvent += (g => action.Invoke());
            return this;
        }

        public EgInputField SetLineType(
            InputField.LineType lineType = InputField.LineType.SingleLine
            )
        {
            inputField.lineType = lineType;
            return this;
        }

        public EgInputField SetCharactorLimit(
            int characterLimit
            )
        {
            inputField.characterLimit = characterLimit;
            return this;
        }

        public EgInputField SetContentType(
            InputField.ContentType contentType
            )
        {
            inputField.contentType = contentType;
            return this;
        }

        //public SInputField SetLineLimit(
        //    int lineLimit
        //    )
        //{
        //    inputField.contentType = lineLimit;
        //    return this;
        //}

    }
}