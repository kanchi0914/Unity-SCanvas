using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.UI;
using SGUI.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static SGUI.Base.Utils;

namespace SGUI.GameObjects
{
    public class SButton : SGameObject
    {
        public Button Button { get; set; }

        public SText SText { get; private set; }

        public SButton(SGameObject parent) : this(parent, "") { }

        public SButton(
            SGameObject parent = null,
            string text = "",
            string name = "SButton",
            ColorType color = ColorType.White
        ) : base(parent, name,
            new Func<GameObject>(() =>
           {
               return UIFactory.CreateButton(parent.GameObject, name, text, color);
           })
        )
        {
            SText = new SText(this, text: text).SetRectSizeByRatio(1, 1) as SText;
            SText.SetFullStretchAnchor();
            SText.SetLocalPos(0, 0);
            Button = gameObject.GetComponent<Button>();
        }

        public SButton SetOnOnClick(Action onClick)
        {
            Button.onClick.RemoveAllListeners();
            return AddOnClick(onClick);
        }

        public SButton AddOnClick(Action onClick)
        {
            Button.onClick.AddListener(() => onClick.Invoke());
            return this;
        }

        public SButton AddOnSelect(Action onSelect)
        {
            var trigger = gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener(e => onSelect());
            trigger.triggers.Add(entry);
            return this;
        }

        public new SButton SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            return base.SetBackGroundColor(colorType, alpha) as SButton;
        }

        public new SButton SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SButton;
        }

        public new SButton SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SButton;
        }

        public new SButton SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as SButton;
        }

        public new SButton SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as SButton;
        }

        public new SButton SetLocalPos(float width, float height)
        {
            return base.SetLocalPos(width, height) as SButton;
        }

        public new SButton SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SButton;
        }

        public new SButton SetAnchorType(AnchorType anchorType)
        {
            return base.SetAnchorType(anchorType) as SButton;
        }


    }
}