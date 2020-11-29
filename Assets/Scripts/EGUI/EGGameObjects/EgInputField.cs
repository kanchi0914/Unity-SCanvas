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
        /// <summary>
        /// InputFieldコンポーネント
        /// </summary>
        public InputField InputFieldComponent { get; private set; }

        /// <summary>
        /// テキスト未入力時に表示されるTextオブジェクト
        /// </summary>
        public EGText PlaceHolderTextObject;

        /// <summary>
        /// Textオブジェクト
        /// </summary>
        public EGText Textobject;

        /// <summary>
        /// InputFieldオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        public EgInputField
        (
            EGGameObject parent
        ) : this
        (
            parent.gameObject
        )
        {
        }

        /// <summary>
        /// InputFieldオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="name">オブジェクト名</param>
        public EgInputField(
            GameObject parent = null,
            string name = "EgInputField"
        ) : base(parent, name)
        {
            SetImageColor(Color.white)
                .SetImage(UGUIResources.InputField);

            PlaceHolderTextObject = new EGText(this.gameObject, "Enter Text...", false, "PlaceHolder")
                .SetFullStretchAnchor()
                .SetImageColor(Color.gray, 0.5f) as EGText;

            PlaceHolderTextObject
                .SetCharacter(fontStyle: FontStyle.Italic)
                .SetColor(Color.gray, 0.6f)
                .SetSize(gameObject.GetRectSize().x - 10, gameObject.GetRectSize().y - 10)
                .SetPosition(0, 0);

            Textobject = new EGText(gameObject, "", false, "Text")
                .SetParagraph(alignment: TextAnchor.UpperLeft)
                .SetSize(RectSize.x - 10, RectSize.y - 10)
                .SetFullStretchAnchor().SetPosition(0, 0) as EGText;

            Textobject.TextComponent.supportRichText = false;

            InputFieldComponent = gameObject.AddComponent<InputField>();
            InputFieldComponent.targetGraphic = gameObject.GetComponent<Image>();
            InputFieldComponent.textComponent = Textobject.TextComponent;
            InputFieldComponent.placeholder = PlaceHolderTextObject.TextComponent;
            InputFieldComponent.lineType = InputField.LineType.MultiLineNewline;
        }

        /// <summary>
        /// テキスト変更時に呼ばれるActionを追加する
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public EgInputField AddOnValueChanged(Action action)
        {
            InputFieldComponent.onValueChanged.AddListener(e => action.Invoke());
            return this;
        }

        // /// <summary>
        // /// テキスト変更時に呼ばれるActionを追加する
        // /// </summary>
        // /// <param name="action"></param>
        // /// <returns></returns>
        // public EgInputField AddOnEndEdit(Action action)
        // {
        //     InputFieldComponent.onEndEdit.AddListener(e => action.Invoke());
        //     return this;
        // }

        /// <summary>
        /// 入力欄にカーソルを合わせたときに呼ばれるActionを設定する
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public EgInputField SetOnSelect(Action action)
        {
            var onSelectEvent = gameObject.GetOrAddComponent<OnSelectEventWrapper>();
            onSelectEvent.OnSelectEvent += (g => action.Invoke());
            return this;
        }

        /// <summary>
        /// 入力欄からカーソルを外したときに呼ばれるActionを設定する
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public EgInputField SetOnDeselect(Action action)
        {
            var onDeselectEvent = gameObject.GetOrAddComponent<OnDeselectWrapper>();
            onDeselectEvent.OnDeselectEvent += (g => action.Invoke());
            return this;
        }

        /// <summary>
        /// InputFieldComponent.characterLimitを設定する
        /// </summary>
        /// <param name="characterLimit"></param>
        /// <returns></returns>
        public EgInputField SetCharacterLimit(
            int characterLimit
        )
        {
            InputFieldComponent.characterLimit = characterLimit;
            return this;
        }

        /// <summary>
        /// InputFieldComponent.lineType、
        /// InputFieldComponent.contentTypeを設定する
        /// </summary>
        /// <param name="lineType"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public EgInputField SetContentType(
            InputField.LineType? lineType = null,
            InputField.ContentType? contentType = null
        )
        {
            InputFieldComponent.lineType = lineType ?? InputFieldComponent.lineType;
            InputFieldComponent.contentType = contentType ?? InputFieldComponent.contentType;
            return this;
        }
    }
}