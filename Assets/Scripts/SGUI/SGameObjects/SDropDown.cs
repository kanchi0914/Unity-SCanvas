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
            this.RectSize = new Vector2(temp.x, temp.y);

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

        public SDropDown AddOption ((string text, Action action) option)
        {
            dropdown.options.Add (new Dropdown.OptionData (option.text));
            options.Add (option);
            return this;
        }

        public SDropDown SetContentAreaImage (string iamgeFilePath)
        {
            var templete = this.GameObject.transform.FindDeep ("Template");
            var image = Resources.Load<Sprite> (iamgeFilePath) as Sprite;
            templete.GetComponent<Image> ().sprite = image;
            return this;
        }

        public SDropDown SetTemplateItemImage (string iamgeFilePath)
        {
            var templete = this.GameObject.transform.FindDeep ("Template");
            var itemTemplate = templete.transform.FindDeep ("Item Background");
            var image = Resources.Load<Sprite> (iamgeFilePath) as Sprite;
            itemTemplate.GetComponent<Image> ().sprite = image;
            return this;
        }

        public SDropDown SetTemplateCheckmarkImage (string iamgeFilePath)
        {
            var templete = this.GameObject.transform.FindDeep ("Template");
            var checkmarkTemplate = templete.transform.FindDeep ("Item Checkmark");
            var image = Resources.Load<Sprite> (iamgeFilePath) as Sprite;
            checkmarkTemplate.GetComponent<Image> ().sprite = image;
            return this;
        }

        public SDropDown SetHandleImage (string iamgeFilePath)
        {
            var templete = this.GameObject.transform.FindDeep ("Template");
            var handle = templete.transform.FindDeep ("Handle");
            var image = Resources.Load<Sprite> (iamgeFilePath) as Sprite;
            handle.GetComponent<Image> ().sprite = image;
            return this;
        }

        public SDropDown SetScrollbarImage (string iamgeFilePath)
        {
            var templete = this.GameObject.transform.FindDeep ("Template");
            var scrollbar = templete.transform.FindDeep ("Scrollbar");
            var image = Resources.Load<Sprite> (iamgeFilePath) as Sprite;
            scrollbar.GetComponent<Image> ().sprite = image;
            return this;
        }

        public SDropDown SetTemplateTextConfig (
            int fontSize, ColorType color, string fontFilePath = null)
        {
            var templete = this.GameObject.transform.FindDeep ("Template");
            var textTemplate = templete.transform.FindDeep ("Item Label");
            var text = textTemplate.GetComponent<Text> ();
            SetTextConfig (text, fontSize, color, fontFilePath);
            return this;
        }

        public SDropDown SetTopItemTextConfig (
            int fontSize, ColorType color, string fontFilePath = null
        )
        {
            var topText = GameObject.transform.Find ("Label").GetComponent<Text> ();
            SetTextConfig (topText, fontSize, color, fontFilePath);
            return this;
        }

        public SDropDown SetTopItemImage (
            string iamgeFilePath
        )
        {
            var image = Resources.Load<Sprite> (iamgeFilePath) as Sprite;
            this.GameObject.GetComponent<Image> ().sprite = image;
            return this;
        }

        public SDropDown SetArrowImage (
            string iamgeFilePath
        )
        {
            var arrow = this.GameObject.transform.Find ("Arrow");
            var image = Resources.Load<Sprite> (iamgeFilePath) as Sprite;
            arrow.GetComponent<Image> ().sprite = image;
            return this;
        }

        private void SetTextConfig (Text text, int fontSize, ColorType color, string fontFilePath = null)
        {
            text.fontSize = fontSize;
            text.color = Utils.GetColor (color, 1f);
            if (fontFilePath != null)
            {
                var font = Resources.Load (fontFilePath) as Font;
                if (font) text.font = font;
            }
        }

        public SDropDown SetOptions (List < (string text, Action action) > options)
        {
            dropdown.ClearOptions ();
            this.options = options;
            options.ForEach (option =>
            {
                dropdown.options.Add (new Dropdown.OptionData (option.text));
            });
            return this;
        }

        public SDropDown SetItemSize (int width, int height)
        {
            this.RectSize = new Vector2(RectSize.x, RectSize.y);
            this.GameObject.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (width, height);
            return this;
        }

        private void OnValueChanged ()
        {
            options[dropdown.value].action.Invoke ();
        }

        public SDropDown SetPadding (int left, int right, int top, int bottom)
        {
            paddingLeft = left;
            paddingRight = right;
            paddingTop = top;
            paddingBottom = bottom;
            SetPadding();
            return this;
        }

        public SDropDown SetSpacing (int spacing)
        {
            this.spacing = spacing;
            SetSpacing();
            return this;
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