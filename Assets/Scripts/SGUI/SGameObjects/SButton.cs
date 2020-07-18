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

namespace SGUI.SGameObjects
{
    public class SButton : SGameObject
    {
        public Button Button { get; set; }

        public SText SText { get; private set; }

        public SButton (
            SGameObject parent = null,
            string name = "SButton",
            string text = "",
            ColorType color = ColorType.White
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                if (parent == null) return UIFactory.CreateButton (null, name, text, color);
                return UIFactory.CreateButton (parent.GameObject, name, text, color);
            })
        ) {
            SText = new SText(this, text:text);
            Button = gameObject.GetComponent<Button>();
        }

        public SButton AddOnClick (Delegate onClick)
        {
            Button.onClick.AddListener (() => onClick.DynamicInvoke ());
            return this;
        }

        public SButton AddOnSelect (Action onSelect)
        {
            var trigger = gameObject.AddComponent<EventTrigger> ();
            var entry = new EventTrigger.Entry ();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener (e => onSelect ());
            trigger.triggers.Add (entry);
            return this;
        }

        public new SButton SetRectSizeByRatio (float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio (ratioX, ratioY) as SButton;
        }

        public new SButton SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SButton;
        }

    }
}