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
        /// <param name="parent">親オブジェクト</param>
        /// <param name="text">ボタンのテキスト</param>
        /// <param name="name">オブジェクト名</param>
        public EGButton(
            GameObject parent,
            string text,
            string name = "EGButton"
        ) : base(parent, name)
        {
            SetImageColor(Color.white).SetImageSprite(UGUIResources.UISprite);
            ButtonComponent = gameObject.GetOrAddComponent<Button>();
            TextObject = new EGText(gameObject, text)
                    .SetRectSizeByRatio(1, 1).SetFullStretchAnchor()as EGText;
        }

        /// <summary>
        /// ボタンコンポーネントから全てのイベントリスナーを削除し、新たにActionを追加する
        /// </summary>
        /// <param name="onClick">クリック時に呼ばれるAction</param>
        public void SetOnOnClick(Action onClick)
        {
            ButtonComponent.onClick.RemoveAllListeners();
            AddOnClick(onClick);
        }

        /// <summary>
        /// ボタンコンポーネントのイベントリスナーにActionを追加する
        /// </summary>
        /// <param name="onClick">クリック時に呼ばれるAction</param>
        public void AddOnClick(Action onClick)
        {
            ButtonComponent.onClick.AddListener(() => onClick.Invoke());
        }
    }
}