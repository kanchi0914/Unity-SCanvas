using System;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGButton : EGGameObject
    {
        /// <summary>
        /// Buttonコンポーネント
        /// </summary>
        public Button ButtonComponent { get; set; }

        /// <summary>
        /// Textコンポーネント
        /// </summary>
        public EGText TextObject { get; private set; }
        
        /// <summary>
        /// Buttonオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクトをラップするEGGameObject</param>
        /// <param name="text">ボタンのテキスト</param>
        public EGButton
        (
            EGGameObject parent,
            string text = ""
        ) : this
        (
            parent.gameObject,
            text
        ){}

        /// <summary>
        /// Buttonオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="text">ボタンのテキスト</param>
        /// <param name="name">オブジェクト名</param>
        public EGButton(
            GameObject parent = null,
            string name = "EGButton"
        ) : base(parent, name)
        {
            SetImageColor(Color.white).SetImage(UGUIDefaultResources.UISprite);
            ButtonComponent = gameObject.GetOrAddComponent<Button>();
            TextObject = new EGText(gameObject)
                    .SetRelativeSize(1, 1).SetFullStretchAnchor()as EGText;
        }

        /// <summary>
        /// ボタンコンポーネントから全てのイベントリスナーを削除し、新たにActionを追加する
        /// </summary>
        /// <param name="onClick">クリック時に呼ばれるAction</param>
        public EGButton SetOnOnClick(Action onClick)
        {
            ButtonComponent.onClick.RemoveAllListeners();
            AddOnClick(onClick);
            return this;
        }

        /// <summary>
        /// ボタンコンポーネントのイベントリスナーにActionを追加する
        /// </summary>
        /// <param name="onClick">クリック時に呼ばれるAction</param>
        public EGButton AddOnClick(Action onClick)
        {
            ButtonComponent.onClick.AddListener(() => onClick.Invoke());
            return this;
        }

        public EGButton SetText(string text)
        {
            TextObject.SetText(text);
            return this;
        }
    }
}