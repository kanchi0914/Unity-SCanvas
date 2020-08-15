// using System;
// using System.Collections.Generic;
// using EGUI.Base;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
//
// namespace EGUI.GameObjects
// {
//     class EGDropDownBackUp : EGGameObject
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
//         public float Spacing  => layoutComponent.spacing;
//
//         public EGDropDown(
//             EGGameObject parent,
//             float posRatioX = 0,
//             float posRatioY = 0,
//             float widthRatio = 1,
//             float heightRatio = 1,
//             string name = "",
//             int maxContentFieldSize = 200
//         ) : base(parent,
//             posRatioX,
//             posRatioY,
//             widthRatio,
//             heightRatio,
//             name,
//             () => UIFactory.CreateDropDown(parent.GameObject, name)
//         )
//         {
//             layoutComponent = gameObject.transform.FindDeep("Content").AddComponent<VerticalLayoutGroup>();
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
//             var trigger = gameObject.AddComponent<EventTrigger>();
//             EventTrigger.Entry entry = new EventTrigger.Entry();
//             entry.eventID = EventTriggerType.PointerClick;
//             entry.callback.AddListener(e => OnClick());
//             trigger.triggers.Add(entry);
//         }
//
//         public void Init()
//         {
//             var label = new EGUIObject(this, name: "Label");
//             var arrow = new EGUIObject(this, name: "Arrow");
//             var template = new EGUIObject(this, name: "Template");
//             var viewport = new EGUIObject(this, name: "Viewport");
//             var content = new EGUIObject(this, name: "Content");
//             var item = new EGUIObject(this, name: "Item Background");
//             var itemBackground = new EGUIObject(this, name: "Item Checkmark");
//             var itemLabel = new EGUIObject(this, name: "Item Label");
//             
//             
//             
//         }
//
//         public void OnClick()
//         {
//             SetSpacing();
//             SetPadding();
//             var content = this.GameObject.FindDeep("Content", false);
//             var rect = content.GetComponent<RectTransform>();
//             var contentFieldSize = itemSize * options.Count + ((options.Count - 1) * Spacing) + PaddingTop + PaddingBottom;
//             rect.sizeDelta = new Vector2(0, contentFieldSize);
//             foreach (Transform n in content.transform)
//             {
//                 var layout = n.gameObject.GetComponent<LayoutElement>();
//                 if (!layout) layout = n.gameObject.AddComponent<LayoutElement>();
//                 layout.minHeight = this.itemSize;
//                 layout.flexibleWidth = 1f;
//             }
//             var dpl = this.GameObject.FindDeep("Dropdown List").GetComponent<RectTransform>();
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
//         public EGDropDown SetContentAreaImage(Sprite image)
//         {
//             var templete = this.GameObject.transform.FindDeep("Template");
//             templete.GetComponent<Image>().sprite = image;
//             return this;
//         }
//
//         public EGDropDown SetTemplateItemImage(Sprite image)
//         {
//             var templete = this.GameObject.transform.FindDeep("Template");
//             var itemTemplate = templete.transform.FindDeep("Item Background");
//             itemTemplate.GetComponent<Image>().sprite = image;
//             return this;
//         }
//
//         public EGDropDown SetTemplateCheckmarkImage(Sprite image)
//         {
//             var templete = this.GameObject.transform.FindDeep("Template");
//             var checkmarkTemplate = templete.transform.FindDeep("Item Checkmark");
//             checkmarkTemplate.GetComponent<Image>().sprite = image;
//             return this;
//         }
//
//         public EGDropDown SetHandleImage(Sprite image)
//         {
//             var templete = this.GameObject.transform.FindDeep("Template");
//             var handle = templete.transform.FindDeep("Handle");
//             handle.GetComponent<Image>().sprite = image;
//             return this;
//         }
//
//         public EGDropDown SetScrollbarImage(Sprite image)
//         {
//             var templete = this.GameObject.transform.FindDeep("Template");
//             var scrollbar = templete.transform.FindDeep("Scrollbar");
//             scrollbar.GetComponent<Image>().sprite = image;
//             return this;
//         }
//
//         public EGDropDown SetTemplateTextConfig(
//             int fontSize, ColorType color, string fontFilePath = null)
//         {
//             var templete = this.GameObject.transform.FindDeep("Template");
//             var textTemplate = templete.transform.FindDeep("Item Label");
//             var text = textTemplate.GetComponent<Text>();
//             SetTextConfig(text, fontSize, color, fontFilePath);
//             return this;
//         }
//
//         public EGDropDown SetTopItemTextConfig(
//             int fontSize, ColorType color, string fontFilePath = null
//         )
//         {
//             var topText = GameObject.transform.Find("Label").GetComponent<Text>();
//             SetTextConfig(topText, fontSize, color, fontFilePath);
//             return this;
//         }
//
//         public EGDropDown SetTopItemImage(
//             string iamgeFilePath
//         )
//         {
//             var image = Resources.Load<Sprite>(iamgeFilePath) as Sprite;
//             this.GameObject.GetComponent<Image>().sprite = image;
//             return this;
//         }
//
//         public EGDropDown SetArrowImage(
//             string iamgeFilePath
//         )
//         {
//             var arrow = this.GameObject.transform.Find("Arrow");
//             var image = Resources.Load<Sprite>(iamgeFilePath) as Sprite;
//             arrow.GetComponent<Image>().sprite = image;
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
//             options.ForEach(option =>
//             {
//                 dropdownComponent.options.Add(new Dropdown.OptionData(option.text));
//             });
//             return this;
//         }
//
//         public EGDropDown SetItemSize(int width, int height)
//         {
//             this.RectSize = new Vector2(RectSize.x, RectSize.y);
//             this.GameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
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
//
//     }
// }