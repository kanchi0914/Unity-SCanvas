using System;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGButton : EGGameObject
    {
        public Button ButtonComponent { get; set; }
        public GameObject TextObject { get; private set; }
        
        public EGButton(
            GameObject parent,
            string name = "EGButton"
        ) : base(
            parent,
            name
            )
        {
            ButtonComponent = gameObject.TryAddComponent<Button>();
            TextObject = new EGGameObject(gameObject, "Text")
                .gameObject.TryAddComponent<Text>().gameObject;
            TextObject.SetRectSizeByRatio(1,1).SetFullStretchAnchor();
        }

        /// <summary>
        /// ボタンコンポーネントから全てのイベントリスナーを削除し、新たにActionを追加します。
        /// </summary>
        /// <param name="onClick"></param>
        /// <returns></returns>
        public EGButton SetOnOnClick(Action onClick)
        {
            ButtonComponent.onClick.RemoveAllListeners();
            return AddOnClick(onClick);
        }

        /// <summary>
        /// ボタンコンポーネントのイベントリスナーにActionを追加します。
        /// </summary>
        /// <param name="onClick"></param>
        /// <returns></returns>
        public EGButton AddOnClick(Action onClick)
        {
            ButtonComponent.onClick.AddListener(() => onClick.Invoke());
            return this;
        }

        // public EGButton AddOnSelect(Action onSelect)
        // {
        //     var trigger = gameObject.AddComponent<EventTrigger>();
        //     var entry = new EventTrigger.Entry();
        //     entry.eventID = EventTriggerType.Select;
        //     entry.callback.AddListener(e => onSelect());
        //     trigger.triggers.Add(entry);
        //     return this;
        // }
        //
    }
}