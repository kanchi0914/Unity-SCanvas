// using System;
// using Assets.Scripts.SGUI.Base;
// using UnityEngine;
// using UnityEngine.UI;
// using UniRx;
// using Assets.Scripts.Extensions;
// using EGUI.Base;
//
// namespace EGUI.GameObjects
// {
//     public class EgToggle : EGImage
//     {
//         public Toggle ToggleComponent { get; private set; }
//
//         public EGImage BoxImageObject { get; private set; }
//
//         public EGImage CheckImageObject { get; private set; }
//         
//         public EGText Text { get; private set; }
//
//         public Color EnabledTextColor { get; private set; } = Color.black;
//         public Color DisabledTextColor { get; private set; } = Color.black;
//         
//         public Color EnabledBackGroundImageColor { get; private set; } = Color.white;
//         
//         public Color DisabledBackGroundImageColor { get; private set; } = Color.gray;
//
//         private bool isWithCheckBox;
//
//         public EgToggle(EGGameObject parent) : this(parent, name:"SToggle") { }
//
//         public EgToggle(
//             EGGameObject parent,
//             string imageFilePath = null,
//             string name = "EgToggle",
//             bool isOn = true,
//             bool isGrouped = false,
//             bool isWithCheckBox = true
//         ) : base(
//             parent,
//             null,
//             name
//         )
//         {
//             SetRectSize(200, 40);
//             
//             ToggleComponent = gameObject.AddComponent<Toggle>();
//             ToggleComponent.isOn = isOn;
//             BoxImageObject = new EGImage(this, name: "toggleBackGround");
//             BoxImageObject.SetColor(Color.gray);
//             Text = new EGText(this, name: "toggleLabel");
//             
//             var spacing = 10;
//             var checkBoxImageSize = RectSize.y - spacing * 2;
//
//             BoxImageObject.SetMiddleLeftAnchor()
//                 .SetLocalPos(spacing, 0);
//             BoxImageObject.SetRectSize(checkBoxImageSize, checkBoxImageSize);
//             var boxAspectFitter = BoxImageObject.GameObject.TryAddComponent<AspectRatioFitter>();
//             boxAspectFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
//             boxAspectFitter.aspectRatio = 1; 
//             BoxImageObject.SetVerticalStretchWithLeftPivotAnchor();
//
//             CheckImageObject = new EGImage(BoxImageObject, name: "box_gray_name")
//                 .SetImageSource(UGUIResources.Checkmark)
//                 .SetRectSizeByRatio(1, 1)
//                 .SetLocalPos(0, 0)
//                 .SetFullStretchAnchor() as EGImage;
//
//             Text.SetMiddleLeftAnchor();
//             if (isWithCheckBox)
//             {
//                 Text.SetLocalPos(checkBoxImageSize + spacing * 2, 0);
//                 float textWidth = (200 - (spacing * 3 + checkBoxImageSize));
//                 Text.SetRectSize(textWidth, RectSize.y);
//             }
//             else
//             {
//                 Text.SetLocalPos( spacing, 0);
//                 float textWidth = (200 - (spacing * 2));
//                 Text.SetRectSize(textWidth, RectSize.y);
//             }
//             Text.SetFullStretchAnchor();
//             
//             BoxImageObject.SetActive(isWithCheckBox);
//             BoxImageObject.SetColor(ColorType.Gray);
//
//             ToggleComponent.targetGraphic = BoxImageObject.ImageComponent;
//             ToggleComponent.graphic = CheckImageObject.ImageComponent;
//
//             if (isGrouped && parent != null)
//             {
//                 var group = parent.gameObject.TryAddComponent<ToggleGroup>();
//                 group.allowSwitchOff = false;
//                 ToggleComponent.group = group;
//             }
//
//             this.isWithCheckBox = isWithCheckBox;
//             ToggleComponent.onValueChanged.AddListener(e => OnSelected());
//         }
//
//         // /// <summary>
//         // /// SetRectSizeしたとき変更できるように
//         // /// </summary>
//         // private void UpdateSize()
//         // {
//         //     var spacing = ApparentRectSize.y * 0.1f;
//         //     if (!isWithBoxImage)
//         //     {
//         //         Text.SetLocalPos(spacing * 1, spacing);
//         //         Text.SetRectSize((int)(ApparentRectSize.x - (spacing * 2)), (int)(0.8f * ApparentRectSize.y));
//         //     }
//         //     else
//         //     {
//         //         var imageSize = 0.8f * ApparentRectSize.y;
//         //         // BoxImageObject.SetAnchorType(Utils.AnchorType.TopLeft);
//         //         // BoxImageObject.SetLocalPos(spacing, spacing);
//         //         // BoxImageObject.SetRectSize((int)imageSize, (int)imageSize);
//         //         Text.SetLocalPos(spacing * 2 + imageSize, spacing);
//         //         Text.SetRectSize((int)(ApparentRectSize.x - (spacing * 3 + imageSize)), (int)imageSize);
//         //     }
//         // }
//
//         public EgToggle SetOnValueChanged(Action action, bool isOn = true)
//         {
//             ToggleComponent.onValueChanged.RemoveAllListeners();
//             return AddOnValueChanged(action, isOn);
//         }
//
//         public EgToggle SetEnabledTextColor(Color color)
//         {
//             EnabledTextColor = color;
//             return this;
//         }
//         
//         public EgToggle SetDisabledTextColor(Color color)
//         {
//             DisabledTextColor = color;
//             return this;
//         }
//         
//         public EGImage SetEnabledBackGroundImageColor(Color color)
//         {
//             EnabledBackGroundImageColor = color;
//             return this;
//         }
//         
//         public EGImage SetDisabledBackGroundImageColor(Color color)
//         {
//             DisabledBackGroundImageColor = color;
//             return this;
//         }
//
//         public EgToggle AddOnValueChanged(Action action, bool isOn = true)
//         {
//             ToggleComponent.onValueChanged.AddListener(e =>
//             {
//                 if (isOn)
//                 {
//                     if (ToggleComponent.isOn) action.Invoke();
//                 }
//                 else
//                 {
//                     if (!ToggleComponent.isOn) action.Invoke();
//                 }
//
//             });
//             return this;
//         }
//         
//         private void OnSelected()
//         {
//             if (ToggleComponent.isOn)
//             {
//                 Text.SetTextColor(EnabledTextColor);
//                 SetColor(EnabledBackGroundImageColor);
//             }
//             else
//             {
//                 Text.SetTextColor(DisabledTextColor);
//                 SetColor(DisabledBackGroundImageColor);
//             }
//         }
//
//     }
//
// }