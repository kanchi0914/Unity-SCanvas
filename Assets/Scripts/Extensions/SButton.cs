using HC.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Extensions
{
    public class SButton : SGameObject
    {
        public Button Button { get; set; }
        public Delegate Func { get; private set; }

        public SButton(GameObject parent, string text, 
            Delegate func, Action onSelect = null)
        {
            gameObject = UIFactory.CreateButton(parent, text);
            Func = func;
            Button = gameObject.GetComponent<Button>();
            Button.onClick.AddListener(() => func.DynamicInvoke());
            if (onSelect != null)
            {
                SetOnSelect(onSelect);
            }
        }

        public SButton(string text)
        {
            Button = UICreator.CreateButton(defaultLabel: text);
        }

        public void SetOnSelect(Action action)
        {
            var trigger = gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener(e => action());
            trigger.triggers.Add(entry);
        }

    }
}
