using System;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using Assets.Scripts.SGUI.SGameObjects.ComponentScripts;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    class EgInputField : EGGameObject
    {
        public InputField InputField { get; private set; }

        EGText placeHolder;
        EGText text;

        public EgInputField(
            GameObject parent,
            string name = "EgInputField"
        ) : base(parent, name)
        {
            Init();
        }

        private void Init()
        {
            gameObject
                .SetImageColor(Color.white)
                .SetImageSprite(UGUIResources.InputField);
            
            placeHolder = new EGText(this.gameObject, "Enter Text...", "PlaceHolder");
            placeHolder.gameObject
                .SetFullStretchAnchor()
                .SetImageColor(Color.gray, 0.5f);

            placeHolder.SetTextConfig(color: Color.gray, colorAlpha: 0.6f, fontStyle: FontStyle.Italic);
            placeHolder.gameObject
                .SetRectSize(gameObject.GetRectSize().x - 10, gameObject.GetRectSize().y - 10)
                .SetLocalPos(0, 0);
            
            text = new EGText(gameObject, "","Text");
            text.gameObject.SetRectSize(gameObject.GetRectSize().x - 10, gameObject.GetRectSize().y - 10);
            text.SetTextConfig(alignment: TextAnchor.UpperLeft);
            text.gameObject.SetFullStretchAnchor().SetLocalPos(0, 0);
            text.TextComponent.supportRichText = false;

            InputField = gameObject.AddComponent<InputField>();
            InputField.targetGraphic = gameObject.GetComponent<Image>();
            InputField.textComponent = text.TextComponent;
            InputField.placeholder = placeHolder.TextComponent;
            InputField.lineType = InputField.LineType.MultiLineNewline;
        }

        public EgInputField AddOnValueChanged(Action action)
        {
            InputField.onValueChanged.AddListener(e => action.Invoke());
            return this;
        }

        public EgInputField AddOnEndEdit(Action action)
        {
            InputField.onEndEdit.AddListener(e => action.Invoke());
            return this;
        }

        public EgInputField SetOnSelect(Action action)
        {
            var onSelectEvent = gameObject.GetOrAddComponent<OnSelectEventWrapper>();
            onSelectEvent.OnSelectEvent += (g => action.Invoke());
            return this;
        }

        public EgInputField SetOnDeselect(Action action)
        {
            var onDeselectEvent = gameObject.GetOrAddComponent<OnDeselectWrapper>();
            onDeselectEvent.OnDeselectEvent += (g => action.Invoke());
            return this;
        }

        public EgInputField SetConfig(
            int? characterLimit = null,
            InputField.LineType? lineType = null,
            InputField.ContentType? contentType = null
            )
        {
            InputField.characterLimit = characterLimit ?? InputField.characterLimit;
            InputField.lineType = lineType ?? InputField.lineType;
            InputField.contentType = contentType ?? InputField.contentType;
            return this;
        }

    }
}