using System;
using EGUI.Base;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGButton : EGGameObject
    {
        public Button Button { get; set; }
        public EGText EgText { get; private set; }
        
        public EGButton(
            EGGameObject parent,
            string text = "",
            ColorType color = ColorType.White,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "EGButton"
        ) : base(parent,
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            name,
            () => UIFactory.CreateButton(parent.GameObject, name, text, color)
            )
        {
            EgText = new EGText(this, text: text);
            EgText.SetFullStretchAnchor();
            Button = gameObject.GetComponent<Button>();
        }

        /// <summary>
        /// ボタンコンポーネントから全てのイベントリスナーを削除し、追加する
        /// </summary>
        /// <param name="onClick"></param>
        /// <returns></returns>
        public EGButton SetOnOnClick(Action onClick)
        {
            Button.onClick.RemoveAllListeners();
            return AddOnClick(onClick);
        }

        /// <summary>
        /// ボタンコンポーネントにイベントリスナーを追加する
        /// </summary>
        /// <param name="onClick"></param>
        /// <returns></returns>
        public EGButton AddOnClick(Action onClick)
        {
            Button.onClick.AddListener(() => onClick.Invoke());
            return this;
        }

        public EGButton AddOnSelect(Action onSelect)
        {
            var trigger = gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener(e => onSelect());
            trigger.triggers.Add(entry);
            return this;
        }
        
    }
}