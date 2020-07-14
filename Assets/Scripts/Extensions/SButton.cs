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
        // public Delegate OnClick { get; private set; }

        public SButton(GameObject parent, string text, 
            Delegate onClick, Action onSelect = null)
        {
            InitGameObject(parent, text);
            SetOnClick(onClick);
            if (onSelect != null)
            {
                SetOnSelect(onSelect);
            }
        }

        public override void InitGameObject(params object[] args){
            gameObject = UIFactory.CreateButton(args[0] as GameObject, args[1] as string);
        }

        public SButton(string text)
        {
            Button = UICreator.CreateButton(defaultLabel: text);
        }

        public void SetOnClick(Delegate onClick)
        {
            // OnClick = onClick;
            Button = gameObject.GetComponent<Button>();
            Button.onClick.AddListener(() => onClick.DynamicInvoke());
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
