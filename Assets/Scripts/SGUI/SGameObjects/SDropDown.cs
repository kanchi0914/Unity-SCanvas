using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGUI.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SGUI.SGameObjects
{
    class SDropDown : SGameObject
    {

        private Dropdown dropdown;

        private List < (string text, Action action) > options = new List < (string text, Action action) > ();

        private int itemSize = 60;

        private int maxContentFieldSize = 200;

        private int paddingLeft = 0;
        private int paddingRight = 0;

        private int paddingTop = 0;
        private int paddingBottom = 0;

        private int spacing = 0;

        public SDropDown (
            SGameObject parent,
            string name,
            int itemSize,
            int maxContentFieldSize
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateDropDown (parent.GameObject, name);
            })
        )
        {
            this.itemSize = itemSize;
            this.maxContentFieldSize = maxContentFieldSize;
            dropdown = this.GameObject.GetComponent<Dropdown> ();
            dropdown.ClearOptions ();
            dropdown.onValueChanged.AddListener (e => OnValueChanged ());
            var layout = this.GameObject.transform.FindDeep ("Content").AddComponent<VerticalLayoutGroup> ();

            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childScaleHeight = false;
            layout.childScaleWidth = false;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;

            (float x, float y) temp = (RectSize.x, itemSize);
            this.RectSize = (temp.x, temp.y);
            //this.GameObject.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (RectSize.x, itemSize);

            var trigger = gameObject.AddComponent<EventTrigger> ();
            EventTrigger.Entry entry = new EventTrigger.Entry ();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener (e => OnClick ());
            trigger.triggers.Add (entry);
        }

        public void OnClick ()
        {
            SetSpacing ();
            SetPadding ();
            var content = this.GameObject.FindDeep ("Content", false);
            var rect = content.GetComponent<RectTransform> ();
            var contentFieldSize = itemSize * options.Count + ((options.Count - 1) * spacing) + paddingTop + paddingBottom;
            rect.sizeDelta = new Vector2 (0, contentFieldSize);
            foreach (Transform n in content.transform)
            {
                var layout = n.gameObject.GetComponent<LayoutElement> ();
                if (!layout) layout = n.gameObject.AddComponent<LayoutElement> ();
                layout.minHeight = this.itemSize;
                layout.flexibleWidth = 1f;
            }
            var dpl = this.GameObject.FindDeep ("Dropdown List").GetComponent<RectTransform> ();
            if (contentFieldSize < maxContentFieldSize)
            {
                if (dpl) dpl.sizeDelta = new Vector2 (0, contentFieldSize);
            }
            else
            {
                if (dpl) dpl.sizeDelta = new Vector2 (0, maxContentFieldSize);
            }
        }

        public void AddOption ((string text, Action action) option)
        {
            dropdown.options.Add (new Dropdown.OptionData (option.text));
            options.Add (option);
        }

        public void SetOptions (List < (string text, Action action) > options)
        {
            dropdown.ClearOptions ();
            this.options = options;
            options.ForEach (option =>
            {
                dropdown.options.Add (new Dropdown.OptionData (option.text));
            });
        }

        public void SetItemSize (int width, int height)
        {
            this.RectSize = (RectSize.x, RectSize.y);
            this.GameObject.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (width, height);
        }

        public void OnValueChanged ()
        {
            Debug.Log (dropdown.value);
            options[dropdown.value].action.Invoke ();
        }

        public void SetPadding (int left, int right, int top, int bottom)
        {
            paddingLeft = left;
            paddingRight = right;
            paddingTop = top;
            paddingBottom = bottom;
        }

        public void SetSpacing (int spacing)
        {
            this.spacing = spacing;
        }

        private void SetPadding ()
        {
            var layout = gameObject.GetComponentInChildren<VerticalLayoutGroup> ();
            layout.padding.left = paddingLeft;
            layout.padding.right = paddingRight;
            layout.padding.top = paddingTop;
            layout.padding.bottom = paddingBottom;
        }

        private void SetSpacing ()
        {
            var layout = gameObject.GetComponentInChildren<VerticalLayoutGroup> ();
            layout.spacing = this.spacing;
        }

    }
}