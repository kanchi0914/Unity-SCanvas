using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using EGUI.Base;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    class EGDropDown : EGGameObject
    {
        private Dropdown dropdownComponent;
        private int defaultWidth = 200;
        private int defaultheight = 40;
        private List<(string text, Action action)> options = new List<(string text, Action action)>();
        private int maxContentFieldSize = 200;
        private VerticalLayoutGroup layoutComponent;
        private EgToggle item;
        
        /// <summary>
        /// ドロップダウン展開時に表示される選択肢一覧表示エリアのオブジェクト
        /// </summary>
        public EGVerticalLayoutScrollView TemplateObject;

        /// <summary>
        /// ドロップダウンを生成
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="maxContentFieldSize">選択肢表示エリアの最大高さ</param>
        /// <param name="name">オブジェクトの名前</param>
        public EGDropDown(
            GameObject parent,
            int maxContentFieldSize = 200,
            string name = "EGDropDown"
        ) : base(parent, name)
        {
            gameObject.SetRectSize(defaultWidth, defaultheight);
            gameObject.SetImageSprite(UGUIResources.UISprite);

            var label = new EGText(gameObject, "", name: "Label");
            label.gameObject.SetMiddleCenterAnchor();
            label.TextComponent.alignment = TextAnchor.MiddleLeft;

            var arrow = new EGGameObject(gameObject, name: "Arrow");
            arrow.gameObject.SetMiddleCenterAnchor();
            arrow.gameObject.SetImageSprite(UGUIResources.Dropdown);

            TemplateObject = new EGVerticalLayoutScrollView(gameObject, isAutoSizingWidth: true, name: "Template");
            TemplateObject.gameObject.SetImageSprite(UGUIResources.UISprite);
            TemplateObject.gameObject.SetImageSprite(UGUIResources.UISprite);
            TemplateObject.ContentAreaObject.LayoutComponent.childControlHeight = true;
            TemplateObject.ContentAreaObject.LayoutComponent.childForceExpandHeight = true;
            TemplateObject.ScrollRectComponent.movementType = ScrollRect.MovementType.Clamped;
            TemplateObject.ScrollRectComponent.verticalScrollbarVisibility =
                ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            TemplateObject.ScrollBarObject.gameObject.SetLocalPos(0, 0);
            
            item = new EgToggle(TemplateObject.ContentAreaObject.gameObject, name: "Item");
            item.gameObject.SetFullStretchAnchor();
            item.gameObject.GetOrAddComponent<LayoutElement>().minHeight = defaultheight;
            item.ToggleComponent.isOn = true;

            Dropdown dropdown = gameObject.GetOrAddComponent<Dropdown>();
            dropdown.targetGraphic = gameObject.GetComponent<Image>();
            dropdown.template = TemplateObject.gameObject.GetOrAddComponent<RectTransform>();
            dropdown.captionText = label.TextComponent;
            dropdown.itemText = item.Text.TextComponent;
            dropdown.onValueChanged.AddListener(_ => OnValueChanged());

            label.gameObject
                .SetRectSize(defaultWidth - 30, defaultheight - 10)
                .SetFullStretchAnchor()
                .SetPivot(0, 0.5f)
                .SetLocalPos(20, 0);

            arrow.gameObject
                .SetMiddleRightAnchor()
                .SetLocalPos(-15, 0)
                .SetRectSize(30, 30);

            TemplateObject.gameObject
                .SetHorizontalStretchWithBottomPivotAnchor()
                .SetRectSize(200, 250)
                .SetLocalPos(0, 0)
                .SetPivot(0.5f, 1);

            TemplateObject.gameObject.SetActive(false);

            this.maxContentFieldSize = maxContentFieldSize;
            dropdownComponent = gameObject.GetComponent<Dropdown>();
            dropdownComponent.ClearOptions();

            var trigger = gameObject.GetOrAddComponent<EventTrigger>();
            gameObject.ObserveEveryValueChanged(g => g.GetRectTransform().sizeDelta)
                .Subscribe(_ => item.gameObject.GetOrAddComponent<LayoutElement>().minHeight
                    = rectTransform.GetApparentRectSize().y);

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener(e => OnClick());
            trigger.triggers.Add(entry);
            label.SetText("");
        }

        private void OnClick()
        {
            var contentFieldSize = rectTransform.GetApparentRectSize().y
                * options.Count + ((options.Count - 1) * TemplateObject.ContentAreaObject.LayoutComponent.spacing)
                                + TemplateObject.ContentAreaObject.LayoutComponent.padding.top
                                + TemplateObject.ContentAreaObject.LayoutComponent.padding.bottom;
            //動的に生成されるので毎回取得する必要がある
            var dpl = gameObject.FindDeep("Dropdown List")?.GetOrAddComponent<RectTransform>();
            if (contentFieldSize < maxContentFieldSize)
            {
                if (dpl)
                    dpl.gameObject.SetRectSize(rectTransform.GetApparentRectSize().x, contentFieldSize)
                        .SetLocalPos(0, 0);
            }
            else
            {
                if (dpl)
                    dpl.gameObject.SetRectSize(rectTransform.GetApparentRectSize().x, maxContentFieldSize)
                        .SetLocalPos(0, 0);
            }
        }

        /// <summary>
        /// ドロップダウンに選択肢を追加する
        /// </summary>
        /// <param name="text">選択肢のテキスト</param>
        /// <param name="action">選択されたときに呼ばれるAction</param>
        /// <returns></returns>
        public EGDropDown AddOption(string text, Action action)
        {
            dropdownComponent.options.Add(new Dropdown.OptionData(text));
            options.Add((text, action));
            return this;
        }

        /// <summary>
        /// ドロップダウンに選択肢をまとめて追加すえう
        /// </summary>
        /// <param name="options">選択肢のテキストと選択時のアクションのタプルのリスト</param>
        /// <returns></returns>
        public EGDropDown SetOptions(List<(string text, Action action)> options)
        {
            dropdownComponent.ClearOptions();
            this.options = options;
            options.ForEach(option => { dropdownComponent.options.Add(new Dropdown.OptionData(option.text)); });
            return this;
        }

        private void OnValueChanged()
        {
            var action = options[dropdownComponent.value].action;
            if (action != null) action.Invoke();
        }
    }
}