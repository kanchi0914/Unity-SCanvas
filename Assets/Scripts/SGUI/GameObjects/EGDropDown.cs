// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Assets.Scripts.Extensions;
// using Assets.Scripts.SGUI.Base;
// using EGUI.Base;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
//
// namespace EGUI.GameObjects
// {
//     class EGDropDown : EGGameObject
//     {
//         private Dropdown dropdownComponent;
//
//         public EGText TopItemText { get; }
//         public EGText TempleteText { get; }
//
//         private List<(string text, Action action)> options = new List<(string text, Action action)>();
//
//         private int itemSize = 60;
//
//         private int maxContentFieldSize = 200;
//
//         private VerticalLayoutGroup layoutComponent;
//
//         public int PaddingLeft => layoutComponent.padding.left;
//         public int PaddingRight => layoutComponent.padding.right;
//         public int PaddingTop => layoutComponent.padding.top;
//         public int PaddingBottom => layoutComponent.padding.bottom;
//         public float Spacing => layoutComponent.spacing;
//
//         public EGVerticalLayoutScrollView TemplateObject;
//
//         public EGDropDown(
//             EGGameObject parent,
//             int maxContentFieldSize = 200,
//             string name = "EGDropDown"
//         ) : base(parent,name)
//         {
//             var label = new EGText(this, "", name: "Label")
//                 .SetMiddleCenterAnchor() as EGText;
//             var arrow = new EGImage(this, name: "Arrow")
//                 .SetMiddleCenterAnchor() as EGImage;
//             TemplateObject = new EGVerticalLayoutScrollView(this, name: "Template")
//                 .SetMiddleCenterAnchor() as EGVerticalLayoutScrollView;
//             var item = new EgToggle(TemplateObject.ContentArea, name: "Item")
//                 .SetFullStretchAnchor() as EgToggle;
//
//             item.ToggleComponent.isOn = true;
//
//             TemplateObject.ImageComponent.sprite = UGUIResources.UISprite;
//             TemplateObject.SetMovementType(ScrollRect.MovementType.Clamped);
//             TemplateObject.SetScrollbarVisibility(ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);
//             TemplateObject.EgScrollBar.SetLocalPos(0, 0);
//
//             label.TextComponent.alignment = TextAnchor.MiddleLeft;
//
//             arrow.ImageComponent.sprite = UGUIResources.Dropdown;
//
//             Image backgroundImage = this.gameObject.TryAddComponent<Image>();
//             backgroundImage.sprite = UGUIResources.UISprite;
//             backgroundImage.type = Image.Type.Sliced;
//
//             Dropdown dropdown = gameObject.TryAddComponent<Dropdown>();
//             dropdown.targetGraphic = backgroundImage;
//             dropdown.template = TemplateObject.GameObject.TryAddComponent<RectTransform>();
//             dropdown.captionText = label.TextComponent;
//             dropdown.itemText = item.Text.TextComponent;
//
//             label.RectTransform.anchorMin = Vector2.zero;
//             label.RectTransform.anchorMax = Vector2.one;
//             label.RectTransform.offsetMin = new Vector2(10, 6);
//             label.RectTransform.offsetMax = new Vector2(-25, -7);
//
//             arrow.SetMiddleRightAnchor()
//                 .SetLocalPos(-15, 0)
//                 .SetRectSize(30, 30);
//
//             TemplateObject.RectTransform.anchorMin = new Vector2(0, 0);
//             TemplateObject.RectTransform.anchorMax = new Vector2(1, 0);
//             TemplateObject.RectTransform.pivot = new Vector2(0.5f, 1);
//             TemplateObject.RectTransform.anchoredPosition = new Vector2(0, 2);
//             TemplateObject.RectTransform.sizeDelta = new Vector2(0, 250);
//
//             TemplateObject.SetActive(false);
//
//             layoutComponent = gameObject.transform.FindDeep("Content")
//                 .TryAddComponent<VerticalLayoutGroup>();
//
//             this.maxContentFieldSize = maxContentFieldSize;
//             dropdownComponent = gameObject.GetComponent<Dropdown>();
//             dropdownComponent.ClearOptions();
//             // dropdownComponent.onValueChanged.AddListener(e => OnValueChanged());
//
//             layoutComponent.childControlWidth = true;
//             layoutComponent.childControlHeight = true;
//             layoutComponent.childScaleHeight = false;
//             layoutComponent.childScaleWidth = false;
//             layoutComponent.childForceExpandHeight = false;
//             layoutComponent.childForceExpandWidth = false;
//
//             (float x, float y) temp = (RectSize.x, itemSize);
//             RectSize = new Vector2(temp.x, temp.y);
//
//             var trigger = gameObject.TryAddComponent<EventTrigger>();
//             EventTrigger.Entry entry = new EventTrigger.Entry();
//             entry.eventID = EventTriggerType.PointerClick;
//             entry.callback.AddListener(e => OnClick());
//             trigger.triggers.Add(entry);
//
//             AddOption("tetetetet", null);
//             AddOption("sddasdasd", null);
//             AddOption("sddasdasdsasasa", null);
//             label.SetText("");
//             OnClick();
//         }
//
//         public void OnClick()
//         {
//             // var aaaa = new EGUIObject(null);
//             // aaaa.Mono.StartCoroutine(aaaaa());
//             // aaaa.Dispose();
//             var content = TemplateObject.ContentArea;
//             var contentFieldSize =
//                 itemSize * options.Count + ((options.Count - 1) * Spacing) + PaddingTop + PaddingBottom;
//             content.SetRectSize(0, contentFieldSize);
//             foreach (Transform t in content.gameObject.transform)
//             {
//                 var layout = t.gameObject.TryAddComponent<LayoutElement>();
//                 layout.minHeight = itemSize;
//                 layout.flexibleWidth = 1f;
//             }
//
//             //動的に生成されるので毎回取得する必要がある
//             var dpl = gameObject.FindDeep("Dropdown List")?.TryAddComponent<RectTransform>();
//             if (contentFieldSize < maxContentFieldSize)
//             {
//                 if (dpl) dpl.sizeDelta = new Vector2(0, contentFieldSize);
//             }
//             else
//             {
//                 if (dpl) dpl.sizeDelta = new Vector2(0, maxContentFieldSize);
//             }
//         }
//
//         IEnumerator aaaaa()
//         {
//             yield return new WaitForSeconds(1f);
//             var content = TemplateObject.ContentArea;
//             var contentFieldSize =
//                 itemSize * options.Count + ((options.Count - 1) * Spacing) + PaddingTop + PaddingBottom;
//             content.SetRectSize(0, contentFieldSize);
//             foreach (Transform t in content.gameObject.transform)
//             {
//                 var layout = t.gameObject.TryAddComponent<LayoutElement>();
//                 layout.minHeight = itemSize;
//                 layout.flexibleWidth = 1f;
//             }
//
//             //動的に生成されるので毎回取得する必要がある
//             var dpl = this.gameObject.FindDeep("Dropdown List").GetComponent<RectTransform>();
//             if (contentFieldSize < maxContentFieldSize)
//             {
//                 if (dpl) dpl.sizeDelta = new Vector2(0, contentFieldSize);
//             }
//             else
//             {
//                 if (dpl) dpl.sizeDelta = new Vector2(0, maxContentFieldSize);
//             }
//
//             Debug.Log("aaaaaa");
//         }
//
//         /// <summary>
//         /// ドロップダウンにアイテムを追加　
//         /// </summary>
//         /// <param name="text"></param>
//         /// <param name="action"></param>
//         /// <returns></returns>
//         public EGDropDown AddOption(string text, Action action)
//         {
//             dropdownComponent.options.Add(new Dropdown.OptionData(text));
//             options.Add((text, action));
//             return this;
//         }
//
//         private void SetTextConfig(Text text, int fontSize, ColorType color, string fontFilePath = null)
//         {
//             text.fontSize = fontSize;
//             text.color = Utils.GetColor(color, 1f);
//             if (fontFilePath != null)
//             {
//                 var font = Resources.Load(fontFilePath) as Font;
//                 if (font) text.font = font;
//             }
//         }
//
//         public EGDropDown SetOptions(List<(string text, Action action)> options)
//         {
//             dropdownComponent.ClearOptions();
//             this.options = options;
//             options.ForEach(option => { dropdownComponent.options.Add(new Dropdown.OptionData(option.text)); });
//             return this;
//         }
//
//         public EGDropDown SetItemSize(int width, int height)
//         {
//             this.RectSize = new Vector2(RectSize.x, RectSize.y);
//             this.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
//             return this;
//         }
//
//         private void OnValueChanged()
//         {
//             var action = options[dropdownComponent.value].action;
//             if (action != null) action.Invoke();
//         }
//
//         public EGDropDown SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
//         {
//             layoutComponent.padding.left = left ?? PaddingLeft;
//             layoutComponent.padding.right = right ?? PaddingRight;
//             layoutComponent.padding.top = top ?? PaddingTop;
//             layoutComponent.padding.bottom = bottom ?? PaddingBottom;
//             return this;
//         }
//
//         public EGDropDown SetSpacing(float? spacing = null)
//         {
//             layoutComponent.spacing = spacing ?? Spacing;
//             return this;
//         }
//     }
// }