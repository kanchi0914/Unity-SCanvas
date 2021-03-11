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
        
        private int defaultWidth = 200;
        private int defaultheight = 40;
        private int maxContentFieldSize = 200;
        private List<(string text, Action action)> options = new List<(string text, Action action)>();
        private VerticalLayoutGroup layoutComponent;
        private EGToggle item;
        
        /// <summary>
        /// Dropdownコンポーネント
        /// </summary>
        public Dropdown DropdownComponent { get; private set; }

        /// <summary>
        /// DropDownオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクトをラップするEGGameObject</param>
        /// <param name="maxContentFieldSize">選択肢表示エリアの最大高さ</param>
        /// <param name="name">オブジェクトの名前</param>
        public EGVerticalLayoutScrollView TemplateObject;
        
        /// <summary>
        /// DropDownオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="maxContentFieldSize">選択肢表示エリアの最大高さ</param>
        public EGDropDown
        (
            EGGameObject parent = null,
            int maxContentFieldSize = 200
        ) : this
        (
            parent?.gameObject,
            maxContentFieldSize
        ){}

        /// <summary>
        /// DropDownオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="maxContentFieldSize">選択肢表示エリアの最大高さ</param>
        /// <param name="name">オブジェクトの名前</param>
        public EGDropDown(
            GameObject parent,
            int maxContentFieldSize = 200,
            string name = "DropDown"
        ) : base(parent, name)
        {
            SetSize(defaultWidth, defaultheight)
                .SetImage(UGUIDefaultResources.UISprite);

            var label = new EGText(gameObject, "Label")
                .SetAnchorType(AnchorType.MiddleCenter) as EGText;
            label.SetParagraph(alignment: TextAnchor.MiddleLeft);

            var arrow = new EGGameObject(gameObject, name: "Arrow")
                .SetAnchorType(AnchorType.MiddleCenter)
                .SetImage(UGUIDefaultResources.Dropdown);

            TemplateObject = new EGVerticalLayoutScrollView(gameObject, isAutoSizingWidth: true, name: "Template")
                .SetChildAlinmentTypes(
                    childControlHeight: true, childForceExpandHeight: true)
                .SetMovementType(ScrollRect.MovementType.Clamped)
                .SetScrollBarVisibility(ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport)
                .SetImage(UGUIDefaultResources.UISprite)
                .SetImageColor(Color.white)
                .SetPosition(0, 0) as EGVerticalLayoutScrollView;

            item = new EGToggle(TemplateObject.ContentAreaObject.gameObject, name: "Item")
                .SetImageColor(Color.clear)
                .SetAnchorType(AnchorType.FullStretch) as EGToggle;
            item.gameObject.GetOrAddComponent<LayoutElement>().minHeight = defaultheight;
            item.IsOn = true;
            item.BoxImageObject.SetImageColor(Color.clear);

            DropdownComponent = gameObject.GetOrAddComponent<Dropdown>();
            DropdownComponent.targetGraphic = gameObject.GetComponent<Image>();
            DropdownComponent.template = TemplateObject.gameObject.GetOrAddComponent<RectTransform>();
            DropdownComponent.captionText = label.TextComponent;
            DropdownComponent.itemText = item.LabelTextObject.TextComponent;
            DropdownComponent.onValueChanged.AddListener(_ => OnValueChanged());
            DropdownComponent.ClearOptions();

            label.SetSize(defaultWidth - 30, defaultheight - 10)
                .SetAnchorType(AnchorType.FullStretch)
                .SetPivot(0, 0.5f)
                .SetPosition(20, 0);

            arrow.SetAnchorType(AnchorType.MiddleRight)
                .SetPosition(-15, 0)
                .SetSize(30, 30);

            TemplateObject
                .SetAnchorType(AnchorType.HorizontalStretchWithBottomPivot)
                .SetSize(200, 250)
                .SetPosition(0, 0)
                .SetPivot(0.5f, 1)
                .SetActive(false);

            this.maxContentFieldSize = maxContentFieldSize;

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
            // 動的に生成されるので毎回取得する必要がある
            var dpl = gameObject.FindDeep("Dropdown List")?.GetOrAddComponent<RectTransform>();
            if (contentFieldSize < maxContentFieldSize)
            {
                if (dpl)
                    dpl.SetSize(rectTransform.GetApparentRectSize().x, contentFieldSize)
                        .SetAnchoredPos(0, 0);
            }
            else
            {
                if (dpl)
                    dpl.SetSize(rectTransform.GetApparentRectSize().x, maxContentFieldSize)
                        .SetAnchoredPos(0, 0);
            }
        }

        /// <summary>
        /// ドロップダウンにアイテムを追加する
        /// </summary>
        /// <param name="text">アイテムのテキスト</param>
        /// <param name="action">選択されたときに呼ばれるAction</param>
        /// <returns></returns>
        public EGDropDown AddOption(string text, Action action)
        {
            DropdownComponent.options.Add(new Dropdown.OptionData(text));
            options.Add((text, action));
            return this;
        }

        /// <summary>
        /// ドロップダウンにアイテムをまとめて追加する
        /// </summary>
        /// <param name="options">アイテムのテキストと選択時のアクションのタプルのリスト</param>
        /// <returns></returns>
        public EGDropDown SetOptions(List<(string text, Action action)> options)
        {
            DropdownComponent.ClearOptions();
            this.options = options;
            options.ForEach(option => { DropdownComponent.options.Add(new Dropdown.OptionData(option.text)); });
            return this;
        }

        private void OnValueChanged()
        {
            var action = options[DropdownComponent.value].action;
            action?.Invoke();
        }
    }
}