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

        private SGameObject parent;
        //public SButton(SGameObject parent, string text, 
        //    Delegate onClick, Action onSelect = null) : this(parent, text)
        //{
        //    if (onClick != null)
        //    {
        //        AddOnClick(onClick);
        //    }
        //    if (onSelect != null)
        //    {
        //        AddOnSelect(onSelect);
        //    }
        //}

        public SButton(
            SGameObject parent,
            string text,
            float posXratio = 0.0f,
            float posYratio = 0.0f,
            float ratioX = 1.0f,
            float ratioY = 1.0f
            )
        {
            InitGameObject(parent, text);
            this.parent = parent;
            RectSize = (parent.RectSize.x * ratioX, parent.RectSize.y * ratioY);
            SetParent(parent);
            SetLocalPos(posXratio, -(posYratio)); 
        }

        public override void InitGameObject(params object[] args){
            gameObject = UIFactory.CreateButton(
                (args[0] as SGameObject).GameObject, args[1] as string
                );
            Button = gameObject.GetComponent<Button>();
        }

        public SButton SetRectSize(float ratioX, float ratioY)
        {
            RectSize = (parent.RectSize.x * ratioX, parent.RectSize.y * ratioY);
            return this;
        }

        public SButton SetLocalPos(float posXratio, float posYratio)
        {
            gameObject.transform.AddLocalPosX(posXratio * parent.RectSize.x);
            gameObject.transform.AddLocalPosY(-(posYratio * parent.RectSize.y));
            return this;
        }

        public SButton AddOnClick(Delegate onClick)
        {
            Button.onClick.AddListener(() => onClick.DynamicInvoke());
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


        //public void SetOnClick(Delegate onClick)
        //{
        //    Button = gameObject.GetComponent<Button>();
        //    Button.onClick.AddListener(() => onClick.DynamicInvoke());
        //}

        //public void SetOnSelect(Action action)
        //{
        //    var trigger = gameObject.AddComponent<EventTrigger>();
        //    var entry = new EventTrigger.Entry();
        //    entry.eventID = EventTriggerType.Select;
        //    entry.callback.AddListener(e => action());
        //    trigger.triggers.Add(entry);
        //}

    }
}
